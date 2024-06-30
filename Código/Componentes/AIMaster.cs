using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;
using System;

public class AIMaster : MonoBehaviour
{
    [SerializeField] private GameObject IAEnemigo;
    [SerializeField] private GameObject IAEnemigoGrande;
    [SerializeField] private Transform enemySpawnpoint;
    public MapGeneration map;
    public PointComponent puntos;
    public ShipComponent player;
    public TimeComponent time;
    public List<GameObject> enemies;
    public List<GameObject> allies;
    public List<AIActorComponent> ais;

    void Start()
    {
        StartCoroutine("Comprar");
        allies.Add(player.gameObject);
    }

    void Update()
    {
        List<AIActorComponent> tmp = ais;
        foreach (AIActorComponent AI in tmp)
        {
            if(AI.GetComponent<ShipComponent>().GetVida() <= 0){Delete(AI);}
            else if((AI.toIsland ||AI.InDestination()) && AI.GetComponent<ShipComponent>().GetEquipo() == -1 && CloseToPlayer(AI.gameObject.transform.position)){
                AI.toIsland = false;
                AI.SetDestination(GetPoint(player.gameObject.transform.position, 30f));
            }
            else if(AI.InDestination()){
                AI.toIsland = true;
                AI.SetDestination(GetPoint( GetClosest(AI.transform.position, AI.GetComponent<ShipComponent>().GetEquipo()), 20f));
            }
        }
    }

    public Vector3 GetClosest(Vector3 origin, int e){
        List<Vector3> i = new List<Vector3>();
        if(e == 1){
            foreach(GameObject ia in enemies){
                if ((ia.gameObject.transform.position - origin).magnitude < 50f){
                    i.Add(ia.transform.position);
                }
            }
        }
        else{
            foreach(GameObject ia in allies){
                if ((ia.gameObject.transform.position - origin).magnitude < 50f){
                    i.Add(ia.transform.position);
                }
            }
        }

        foreach(IslandComponent island in map.islasGeneradas){
            if(island.equipoConquistado != e && (island.gameObject.transform.position - origin).magnitude < 50f){i.Add(island.gameObject.transform.position);}
        }

        i.Sort((obj1, obj2) => (obj1 - origin).magnitude.CompareTo((obj2 - origin).magnitude));
        int r = Random.Range(0,Math.Min(2, i.Count));
        if(r<0){r = 0;}
        return i[r];
    }

    public void Delete(AIActorComponent a){
        ais.Remove(a);
        enemies.Remove(a.gameObject);
        Destroy(a.gameObject);
    }

    public bool CloseToPlayer(Vector3 pos){
        return (pos - player.gameObject.transform.position).magnitude < 40;
    }

    public Vector3 GetPoint(Vector3 center, float distance){
        float angle = Random.Range(0f, 2f * Mathf.PI);

        // Calculamos las coordenadas del punto a una distancia `distance` del centro `center`
        float offsetX = Mathf.Cos(angle) * distance;
        float offsetY = Mathf.Sin(angle) * distance;

        // Creamos un nuevo Vector3 para representar el punto alrededor del centro
        Vector3 point = new Vector3(center.x + offsetX, center.y + offsetY, 0);

        return point;
    }

    IEnumerator Comprar(){
        while(true){
            while(enemies.Count < 6 && puntos.monedasIA >= 10){
                BuildEnemyShip();
                puntos.monedasIA -= 10;
            }

            while(puntos.monedasIA >= 10 && enemies.Count < 10){
                float random = Random.Range(1,10);
                if(random%2 == 0 && puntos.monedasIA >= 10){BuildEnemyShip();
                puntos.monedasIA -= 10;}
                else if (puntos.monedasIA >=20){BuildBigEnemyShip();
                puntos.monedasIA -= 20;}
            }
            yield return new WaitForSeconds(15);
        }
    }

    void BuildBigEnemyShip(){       
        GameObject IA = Instantiate(IAEnemigoGrande, enemySpawnpoint.position, enemySpawnpoint.rotation);
        IA.GetComponent<ShipComponent>().SetEquipo(-1);
        IA.GetComponent<AIActorComponent>().player = player;
        IA.GetComponent<ShipComponent>().time = time;
        ais.Add(IA.GetComponent<AIActorComponent>());
        AddAI(IA.GetComponent<AIActorComponent>());
    }

    void BuildEnemyShip(){       
        GameObject IA = Instantiate(IAEnemigo, enemySpawnpoint.position, enemySpawnpoint.rotation);
        IA.GetComponent<ShipComponent>().SetEquipo(-1);
        IA.GetComponent<AIActorComponent>().player = player;
        IA.GetComponent<ShipComponent>().time = time;
        ais.Add(IA.GetComponent<AIActorComponent>());
        AddAI(IA.GetComponent<AIActorComponent>());
    }

    public void AddAI(AIActorComponent ai){ais.Add(ai); allies.Add(ai.gameObject);}

    public bool canAddAllies(){return allies.Count <= 10;}
}