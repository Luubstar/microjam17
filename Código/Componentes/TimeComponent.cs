using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TimeComponent : MonoBehaviour
{
    [SerializeField] public int minutos;
    [SerializeField] public int segundos;
    public UIComponent UI;
    private PointComponent points;

    void Start(){
        points = gameObject.GetComponent<PointComponent>();
        StartCoroutine("CountDown");
    }

    public int GetSegundos(){
        return minutos*60 + segundos;
    }
    
    public override string ToString(){
        if (segundos >= 10){return minutos + ":" + segundos;}
        return minutos + ":0" + segundos;
    }

    IEnumerator CountDown(){
        while (GetSegundos() > 0){
            segundos -= 1;
            if (segundos < 0 && minutos > 0){segundos = 59; minutos-=1;}
            
            yield return new WaitForSeconds(1f);
        }
        points.CuentaIslas();
        if (points.Ganas() >= 0.5f){UI.Win(true);}
        else{UI.Win(false);} 
        Time.timeScale = 0f;
    }

}
