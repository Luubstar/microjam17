using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class IslandComponent : MonoBehaviour
{
    [SerializeField] private GameObject bandera;
    [SerializeField] private GameObject[] posiciones;
    [SerializeField] float velocidadConquista;
    [SerializeField] int equipoConquistado = ShipComponent.NEUTRAL;
    int direccionConquista;
    public float puntosDeConquista;

    void Start(){
        StartCoroutine("Conquista");
    }

    void Update(){
        float t = Mathf.Clamp01(Math.Abs(puntosDeConquista) / 100f);
        bandera.transform.position = Vector3.Lerp(posiciones[1].transform.position, posiciones[0].transform.position, 1-t);
        if(puntosDeConquista > 0){bandera.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 1f);}
        else if(puntosDeConquista < 0){bandera.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f);}
    }

    IEnumerator Conquista(){
        while(true){
            yield return new WaitForSeconds(0.01f);
            puntosDeConquista += velocidadConquista * direccionConquista;
            if(puntosDeConquista >= 100){puntosDeConquista = 100; equipoConquistado = ShipComponent.BLUETEAM;}
            else if(puntosDeConquista <= -100){puntosDeConquista = -100; equipoConquistado = ShipComponent.REDTEAM;}
        }
    }

    public int GetEquipo(){return equipoConquistado;}

    void OnTriggerStay2D(Collider2D col){
        if(GetEquipo() == ShipComponent.BLUETEAM && col.GetComponent<PlayerComponent>() != null){
            col.GetComponent<PlayerComponent>().GetUI().TurnButtons(true);
        }
    }

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
        if(col.GetComponent<PlayerComponent>() != null){
            col.GetComponent<PlayerComponent>().GetUI().TurnButtons(false);
        }
    }


}
