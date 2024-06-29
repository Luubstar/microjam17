using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointComponent : MonoBehaviour
{
    private int monedasJugador;
    private int monedasIA = 10;
    private int cuentaIslas;
    [SerializeField] MapGeneration map;
    [SerializeField] ShipComponent player;

    void Start(){
        StartCoroutine("CoinGenerator");
    }

    IEnumerator CoinGenerator(){
        while(true){
            yield return new WaitForSeconds(10);
            int puntosBlue = 0;
            int puntosRed = 0;
            cuentaIslas = 0;
            
            List<IslandComponent> islas = map.GetIslasGeneradas();
            foreach(IslandComponent i in islas){
                if (i.GetEquipo() == ShipComponent.BLUETEAM){puntosBlue += 1; cuentaIslas+=1;}
                else if (i.GetEquipo() == ShipComponent.REDTEAM){puntosRed += 1; cuentaIslas-=1;}
            }

            monedasIA += 10 * puntosRed;
            monedasJugador += 10 * puntosBlue;
        }
    }

    public bool Ganas(){return cuentaIslas > 0;}

    public int GetMonedasJugador(){return monedasJugador;}
    public int GetMonedasIA(){return monedasIA;}

    //TODO: Resto de compras
    public void GenerarAliado(){
        if(monedasJugador >= 5){
            monedasJugador -= 5;
            Debug.Log("Añadir Aliado");
        }
    }
    public void RepararJugador(){
        if(monedasJugador >= 10){
            monedasJugador -= 10;
            player.SetVida(player.GetVida() + 4);
            if(player.GetVida() > 12){player.SetVida(12);}
        }
    }
    public void MejorarVelocidad(){
        if(monedasJugador >= 10){
            monedasJugador -= 10;
            Debug.Log("Mejorar Velocidad");
        }
    }
    public void MejorarDaño(){
        if(monedasJugador >= 15){
            monedasJugador -= 15;
            player.MejorarDaño();
        }
    }
    public void MejorarCañones(){
        if(monedasJugador >= 20){
            monedasJugador -= 20;
            Debug.Log("Mejorar Cañones");
        }
    }
}
