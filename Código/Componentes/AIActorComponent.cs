using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AIActorComponent : MonoBehaviour
{
    public ShipComponent player;
    public UnityEngine.AI.NavMeshAgent agent;
    public float rotationSpeed;
    public bool toIsland;
    GameObject objective;
    public LayerMask entityLayer;
    ShipComponent ship;
    void Start()
    {
        ship = gameObject.GetComponent<ShipComponent>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
        StartCoroutine("FindObjective");
    }

    void Update()
    {
        if (agent.hasPath && agent.isOnNavMesh)
        {
           Vector3 direction = agent.steeringTarget - transform.position;
            direction.z = 0; // Asegúrate de que la dirección sea 2D

            // Si la dirección no es cero, rota hacia ella
            if (direction != Vector3.zero)
            {
                // Calcula la rotación deseada hacia el objetivo
                Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);

                // Rota suavemente hacia la rotación deseada
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
        if(objective != null){
            Vector3 aimPos = objective.transform.position + (Vector3) objective.gameObject.GetComponent<Rigidbody2D>().velocity * 1.5f + (Vector3) gameObject.GetComponent<Rigidbody2D>().velocity * 1.5f;
            if(ship.GunsAimed(aimPos)){
                ship.Shoot();
            }
            else{
                ship.Aim(aimPos);
            }
        }
    }

    IEnumerator FindObjective(){
        while(true){
            if(objective == null  || (objective.transform.position - this.gameObject.transform.position).magnitude > 40f){
                Collider2D[] colliders = Physics2D.OverlapCircleAll(this.gameObject.transform.position, 40, entityLayer);
                // Iterar sobre los colliders encontrados
                ShipComponent closest = null;
                foreach (Collider2D col in colliders)
                {
                    if(col.gameObject.GetComponent<ShipComponent>() != null){
                        ShipComponent s = col.gameObject.GetComponent<ShipComponent>();
                        if(s.GetEquipo() != ship.GetEquipo()){
                            if(closest == null || closest.gameObject.transform.position.magnitude > s.gameObject.transform.position.magnitude){
                                closest = s;
                            }
                        }
                    }
                }
                if(closest != null){objective = closest.gameObject;}
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void SetDestination(Vector3 target){agent.SetDestination(target);}

    public bool InDestination(){
        if(!agent.isOnNavMesh){return false;}
        float dist=agent.remainingDistance;
        return agent.pathStatus==UnityEngine.AI.NavMeshPathStatus.PathComplete && agent.remainingDistance<=10;
        }
}
