using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShipComponent : MonoBehaviour
{
    public static int BLUETEAM = 1;
    public static int REDTEAM = -1;
    public static int NEUTRAL = 0;
    public bool alive = true;
    [SerializeField] public Transform spawnpoint;
    [SerializeField] private SpriteRenderer casco;
    [SerializeField] private SpriteRenderer[] animacionHundirse;
    [SerializeField] private SpriteRenderer icono;
    [SerializeField] private int vida;
    [SerializeField] private int equipo;
    [SerializeField] private float velocidadDeRotacionDeTorreta;
    [SerializeField] public float velocidadAvance;
    [SerializeField] public float velocidadGiro;
    [SerializeField] private float fuerzaDeDisparo;
    [SerializeField] private int dañobalas;
    [SerializeField] private int tiempoRecarga;
    [SerializeField] public GunComponent[] gunComponents;
    [SerializeField] public TimeComponent time;

    void Start(){
        for(int i = 0; i < gunComponents.Length; i++){
            gunComponents[i].SetShip(this);
            gunComponents[i].SetTime(time);
        }

        if(equipo == BLUETEAM){casco.color = new Color(0f,0f,1f); icono.color = new Color(0f,0f,1f);}
        else if(equipo == REDTEAM){casco.color = new Color(1f,0f,0f);icono.color = new Color(1f,0f,0f);}
    }

    void Update(){
        if(vida <= 0 && alive){
            alive = false;
            StartCoroutine("Hundirse");
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
    public int GetVida(){return vida;}
    public void SetVida(int v){vida = v;}
    public void SetEquipo(int v){equipo = v;}
    public void MejorarDaño(){dañobalas++;}
    public void Dañar(int daño){vida -= daño;}

    public void Aim(Vector3 p){
        for(int i = 0; i < GetGunComponents().Length; i++){
            GunComponent gun = GetGunComponents()[i];
            gun.Aim(p);
        }
    }
    public void Shoot(){
        for(int i = 0; i < GetGunComponents().Length; i++){
            GunComponent gun = GetGunComponents()[i];
            gun.Shoot();
        }
    }

    public bool GunsAimed(Vector3 p){
        for(int i = 0; i < GetGunComponents().Length; i++){
                GunComponent gun = GetGunComponents()[i];
                if(!gun.isAimed(p)){return false;}
            }
        return true;
    }

    IEnumerator Hundirse(){ 
        float v = 0f;
        while(v < 100f){
            float t = Mathf.Clamp01(Math.Abs(v) / 100f);
            foreach(SpriteRenderer r in animacionHundirse){
                Color c = r.color;
                c.a = 1-t;
                r.color = c;
            }
            v += 2;
            yield return new WaitForSeconds(0.1f);
        }
        if(gameObject.GetComponent<PlayerComponent>() != null){
            gameObject.GetComponent<PlayerComponent>().Respawn();
            alive = true;
            vida = 12;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            foreach(SpriteRenderer r in animacionHundirse){
                Color c = r.color;
                c.a = 1;
                r.color = c;
            }
        }
    }
}
