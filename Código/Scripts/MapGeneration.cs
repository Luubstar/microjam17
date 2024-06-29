using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    [SerializeField] private GameObject[] islas;
    [SerializeField] private GameObject[] rocas;
    [SerializeField] private Transform[] coordsRocas;
    [SerializeField] private Transform[] primeraZona;
    [SerializeField] private Transform[] segundaZona;
    private List<IslandComponent> islasGeneradas = new List<IslandComponent>();
    void Start(){
        Generate();
    }

    public List<IslandComponent> GetIslasGeneradas(){return islasGeneradas;}

    void Generate(){
        SpawnIslas(primeraZona);
        SpawnIslas(segundaZona);
        SpawnPiedras();
    }

    void SpawnPiedras(){
        int max = 15;
        while(max > 0){
            float x = Random.Range(coordsRocas[0].position.x, coordsRocas[1].position.x);
            float y = Random.Range(coordsRocas[0].position.y, coordsRocas[1].position.y);

            float ry = Random.Range(-180f, 180f);
            GameObject roca = Instantiate(rocas[Random.Range(0, rocas.Length-1)], new Vector3(x,y,2), new Quaternion(0,0,ry,0));
            roca.transform.SetParent(this.gameObject.transform);
            //TODO: Evitar que estén muy juntas y sobre islas?
            max--;
        }

    }

    void SpawnIslas(Transform[] pos){
        List<Transform> posiciones = new List<Transform>();
        int cant = Random.Range(2, pos.Length);

        for(int i = 0; i < cant; i++){
            int j = Random.Range(0, pos.Length-1);
            if (!posiciones.Contains(pos[j])){
                posiciones.Add(pos[j]);
                float y = Random.Range(-180f, 180f);
                GameObject isla = Instantiate(islas[Random.Range(0, islas.Length-1)], new Vector3(pos[j].position.x, pos[j].position.y,1), new Quaternion(0,0,y,0));
                isla.transform.SetParent(this.gameObject.transform);
                islasGeneradas.Add(isla.GetComponent<IslandComponent>());
            }
        }
    }
}
