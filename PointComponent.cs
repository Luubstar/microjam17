using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointComponent : MonoBehaviour
{
    private int monedasJugador;
    private int monedasIA;
    private int cuentaIslas;
    [SerializeField] MapGeneration map;

    void Start(){
        StartCoroutine("CoinGenerator");
    }

    IEnumerator CoinGenerator(){
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

    public bool Ganas(){return cuentaIslas > 0;}

    public int GetMonedasJugador(){return monedasJugador;}
    public int GetMonedasIA(){return monedasIA;}
}
