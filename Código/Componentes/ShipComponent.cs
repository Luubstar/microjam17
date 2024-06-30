using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShipComponent : MonoBehaviour
{
    public static Color BLUE = new Color(61f/255, 118f/255, 200f/255);
    public static Color RED = new Color(250f/255, 69f/255, 62f/255);

    public static int BLUETEAM = 1;
    public static int REDTEAM = -1;
    public static int NEUTRAL = 0;
    public bool alive = true;
    public int vidaMax;
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

    public GameObject Humo1;
    public GameObject Humo2;
    public GameObject Humo3;

    void Start(){
        for(int i = 0; i < gunComponents.Length; i++){
            gunComponents[i].SetShip(this);
            gunComponents[i].SetTime(time);
        }

        if(equipo == BLUETEAM){casco.color = BLUE; icono.color = BLUE;}
        else if(equipo == REDTEAM){casco.color = RED;icono.color = RED;}
    }

    void Update(){
        if(vida <= 0 && alive){
            alive = false;
            StartCoroutine("Hundirse");
        }

        if(vida > vidaMax - vidaMax/3){
            Humo1.SetActive(false);
            Humo3.SetActive(false);
            Humo2.SetActive(false);
        }
        else if(vida <= vidaMax - (2*vidaMax)/3){
            Humo1.SetActive(true);
            Humo3.SetActive(true);
            Humo2.SetActive(true);
        }
        else if(vida <= vidaMax - vidaMax/3){
            Humo1.SetActive(true);
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

    public bool canShoot(){
        for(int i = 0; i < GetGunComponents().Length; i++){
                GunComponent gun = GetGunComponents()[i];
                if(!gun.canShoot()){return false;}
            }
        return true;
    }

    IEnumerator Hundirse(){ 
        if(gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>() != null){
            gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
        }
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
            vida = vidaMax;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            foreach(SpriteRenderer r in animacionHundirse){
                Color c = r.color;
                c.a = 1;
                r.color = c;
            }
        }
        else{
            Debug.Log("Hundido");
            gameObject.GetComponent<AIActorComponent>().master.Delete(gameObject.GetComponent<AIActorComponent>());
        }
    }
}
