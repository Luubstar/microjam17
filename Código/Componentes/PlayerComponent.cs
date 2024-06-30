using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(MoveComponent))]
[RequireComponent(typeof(ShipComponent))]
public class PlayerComponent : MonoBehaviour
{
    private MoveComponent moveComponent;
    private ShipComponent ship;
    public AIMaster ai;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float MaximoEspacioCamara;
    [SerializeField] private float MinimoEspacioCamara;
    [SerializeField] private UIComponent UI;
    [SerializeField] private GameObject IAAliado;
    bool playing = true;
    bool onHelp = false;

    public GameObject puntero;
    
    public void GenerarAliado(){
        GameObject IA = Instantiate(IAAliado, ship.spawnpoint.position, ship.spawnpoint.rotation);
        IA.GetComponent<ShipComponent>().SetEquipo(ship.GetEquipo());
        IA.GetComponent<AIActorComponent>().player = ship;
        ai.AddAI(IA.GetComponent<AIActorComponent>());
    }

    public void MejorarVelocidad(){
        ship.velocidadAvance += 0.2f;
        ship.velocidadGiro += 0.01f;
    }

    public void MejorarCañones(){
        foreach(GunComponent g in ship.gunComponents){g.UpdateLevel();}
    }

    void Start()
    {
        moveComponent = gameObject.GetComponent<MoveComponent>();
        ship = gameObject.GetComponent<ShipComponent>();
        Respawn();
        ai.playing = true;
        Pause();
        Pause();
    }

    void Update(){

        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 2;
        puntero.transform.position = mouse;

        if(!ship.canShoot()){puntero.GetComponent<SpriteRenderer>().color = new Color(1f,0f,0f);}
        else if(!ship.GunsAimed(mouse)){puntero.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f);}
        else {puntero.GetComponent<SpriteRenderer>().color = new Color(0f,1f,0f);}


        if(Input.GetKeyDown("escape") && !onHelp){Pause();}
        if(Input.GetKeyDown(KeyCode.F1) && (onHelp || playing)){Help();}
        if (ship.alive && playing){
            if(Math.Abs(Input.GetAxis("Horizontal")) > 0 || Math.Abs(Input.GetAxis("Vertical")) > 0){
                moveComponent.Move(Input.GetAxis("Vertical"),Input.GetAxis("Horizontal"), Input.GetAxis("Vertical") < 0);}

            if(Input.GetAxis("Mouse ScrollWheel") < 0 && mainCamera.orthographicSize < MaximoEspacioCamara){mainCamera.orthographicSize += 0.25f;}
            if(Input.GetAxis("Mouse ScrollWheel") > 0 && mainCamera.orthographicSize > MinimoEspacioCamara){mainCamera.orthographicSize -= 0.25f;}

            if(Input.GetKeyDown("1")){UI.ClickButton(1);}
            else if(Input.GetKeyDown("2")){UI.ClickButton(2);}
            else if(Input.GetKeyDown("3")){UI.ClickButton(3);}
            else if(Input.GetKeyDown("4")){UI.ClickButton(4);}
            else if(Input.GetKeyDown("5")){UI.ClickButton(5);}


            if(!ship.GunsAimed(mouse)){ship.Aim(mouse);}
            if(ship.GunsAimed(mouse) && Input.GetMouseButtonDown(0)){ship.Shoot();}
        }
    }

    public UIComponent GetUI(){return UI;} 

    public void Respawn(){
        transform.position = ship.spawnpoint.position;
        transform.rotation = ship.spawnpoint.rotation;
    }

    public void Pause(){
        playing = !playing; UI.Pause(!playing); ai.playing = playing;
        if(playing){Time.timeScale = 1f;}
        else{Time.timeScale = 0f;}
    }
    public void Help(){
        onHelp = !onHelp;
        playing = !playing; ai.playing = playing;
        UI.Help(!playing);
        if(playing){Time.timeScale = 1f;}
        else{Time.timeScale = 0f;}
    }

}
