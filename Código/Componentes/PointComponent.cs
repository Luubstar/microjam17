using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointComponent : MonoBehaviour
{
    [SerializeField] private int monedasJugador;
    public int monedasIA = 10;
    private float allyIslands;
    private float enemyIslands;
    public bool puedeMejorarVelocidad = true;
    public bool puedeMejorarDaño = true;
    public bool puedeMejorarCañones = true;
    public bool puedeCurarse = false;
    [SerializeField] MapGeneration map;
    [SerializeField] PlayerComponent player;
    public AIMaster aimaster;

    int mejorasVelocidad;
    int mejorasBalas;
    int mejorasCañones;
    int puntosBlue = 0;
    int puntosRed = 0;
    public AudioSource sourceCompra;
    void Start(){
        StartCoroutine("CoinGenerator");
        CuentaIslas();
    }

    void Update(){
        puedeCurarse = player.GetComponent<ShipComponent>().GetVida() < 12;
    }

    IEnumerator CoinGenerator(){
        while(true){
            yield return new WaitForSeconds(10);
            puntosBlue = 0;
            puntosRed = 0;
            
            CuentaIslas();

            monedasIA += 10 * puntosRed;
            monedasJugador += 10 * puntosBlue;
        }
    }

    public void CuentaIslas(){
        allyIslands = 0;
        enemyIslands = 0;
        List<IslandComponent> islas = map.GetIslasGeneradas();
        foreach(IslandComponent i in islas){
            if (i.GetEquipo() == ShipComponent.BLUETEAM){puntosBlue += 1; allyIslands+=1;}
            else if (i.GetEquipo() == ShipComponent.REDTEAM){puntosRed += 1; enemyIslands+=1;}
        }
    }

    public float Ganas(){return allyIslands/(enemyIslands+allyIslands);}

    public int GetMonedasJugador(){return monedasJugador;}
    public int GetMonedasIA(){return monedasIA;}

    public void GenerarAliado(){
        sourceCompra.Play();
        if(monedasJugador >= 5 && aimaster.canAddAllies()){
            monedasJugador -= 5;
            player.GenerarAliado();
        }
    }
    public void RepararJugador(){
        sourceCompra.Play();
        if(monedasJugador >= 10 && puedeCurarse){
            monedasJugador -= 10;
            player.GetComponent<ShipComponent>().SetVida(player.GetComponent<ShipComponent>().GetVida() + 4);
            if(player.GetComponent<ShipComponent>().GetVida() > 12){player.GetComponent<ShipComponent>().SetVida(12);}
        }
    }
    public void MejorarVelocidad(){
        sourceCompra.Play();
        if(monedasJugador >= 10 && puedeMejorarVelocidad){
            mejorasVelocidad++;
            monedasJugador -= 10;
            player.MejorarVelocidad();
            puedeMejorarVelocidad = mejorasVelocidad < 3;
        }
    }
    public void MejorarDaño(){
        sourceCompra.Play();
        if(monedasJugador >= 15 && puedeMejorarDaño){
            mejorasBalas++;
            monedasJugador -= 15;
            player.GetComponent<ShipComponent>().MejorarDaño();
            puedeMejorarVelocidad = mejorasVelocidad < 3;
        }
    }
    public void MejorarCañones(){
        sourceCompra.Play();
        if(monedasJugador >= 20 && puedeMejorarCañones){
            mejorasCañones++;
            monedasJugador -= 20;
            player.MejorarCañones();
            puedeMejorarVelocidad = mejorasCañones < 3;
        }
    }
}
