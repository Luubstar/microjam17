using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipComponent : MonoBehaviour
{
    public static int BLUETEAM = 1;
    public static int REDTEAM = -1;
    public static int NEUTRAL = 0;
    [SerializeField] private int vida;
    [SerializeField] private int equipo;
    [SerializeField] private float velocidadDeRotacionDeTorreta;
    [SerializeField] private float velocidadAvance;
    [SerializeField] private float velocidadGiro;
    [SerializeField] private float fuerzaDeDisparo;
    [SerializeField] private int dañobalas;
    [SerializeField] private int tiempoRecarga;
    [SerializeField] private GunComponent[] gunComponents;
    [SerializeField] private TimeComponent time;
    
    void Start(){
        for(int i = 0; i < gunComponents.Length; i++){
            gunComponents[i].SetShip(this);
            gunComponents[i].SetTime(time);
            }
    }

    public GunComponent[] GetGunComponents(){return gunComponents;} 
    public float GetVelocidadDeRotacionDeTorreta(){return velocidadDeRotacionDeTorreta;}
    public float GetVelocidadAvance(){return velocidadAvance;}
    public float GetVelocidadGiro(){return velocidadGiro;}
    public float GetFuerzaDeDisparo(){return fuerzaDeDisparo;}
    public int GetTiempoRecarga(){return tiempoRecarga;}
    public int GetEquipo(){return equipo;}
    public int GetDañoBalas(){return dañobalas;}
    public void Dañar(int daño){
        vida -= daño;
        if(vida < 0){Debug.Log("Fin de la partida");}
    }
}
