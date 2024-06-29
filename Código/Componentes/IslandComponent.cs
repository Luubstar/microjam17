using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class IslandComponent : MonoBehaviour
{
    [SerializeField] float velocidadConquista;
    [SerializeField] int equipoConquistado = ShipComponent.NEUTRAL;
    int direccionConquista;
    float puntosDeConquista;

    void Start(){
        StartCoroutine("Conquista");
    }

    IEnumerator Conquista(){
        while(true){
            yield return new WaitForSeconds(0.1f);
            puntosDeConquista += velocidadConquista * direccionConquista;
            if(puntosDeConquista >= 100){puntosDeConquista = 100; equipoConquistado = ShipComponent.BLUETEAM;}
            else if(puntosDeConquista <= -100){puntosDeConquista = -100; equipoConquistado = ShipComponent.REDTEAM;}
        }
    }

    public int GetEquipo(){return equipoConquistado;}

    void OnTriggerEnter2D(Collider2D col){
        if(col.GetComponent<ShipComponent>() != null){
            ShipComponent ship = col.GetComponent<ShipComponent>();
            direccionConquista += ship.GetEquipo();
        }
    }

    void OnTriggerExit2D(Collider2D col){
        if(col.GetComponent<ShipComponent>() != null){
            ShipComponent ship = col.GetComponent<ShipComponent>();
            direccionConquista -= ship.GetEquipo();
        }
    }


}
