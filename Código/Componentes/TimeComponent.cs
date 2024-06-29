using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TimeComponent : MonoBehaviour
{
    [SerializeField] private int minutos;
    [SerializeField] private int segundos;

    void Start(){
        StartCoroutine("CountDown");
    }

    public int GetSegundos(){
        return minutos*60 + segundos;
    }

    IEnumerator CountDown(){
        while (GetSegundos() > 0){
            segundos -= 1;
            if (segundos < 0 && minutos > 0){segundos = 59; minutos-=1;}
            else{} //TODO: FIN DE LA PARTIDA
            yield return new WaitForSeconds(1f);
        }
    }

}
