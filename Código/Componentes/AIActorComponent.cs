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
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
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
    }

    public void SetDestination(Vector3 target){agent.SetDestination(target);}

    public bool InDestination(){
        if(!agent.isOnNavMesh){return false;}
        float dist=agent.remainingDistance;
        return agent.pathStatus==UnityEngine.AI.NavMeshPathStatus.PathComplete && agent.remainingDistance<=10;
        }
}
