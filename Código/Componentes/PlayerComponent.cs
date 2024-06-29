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
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float MaximoEspacioCamara;
    [SerializeField] private float MinimoEspacioCamara;
    void Start()
    {
        moveComponent = gameObject.GetComponent<MoveComponent>();
        ship = gameObject.GetComponent<ShipComponent>();
    }

    void Update(){
        if(Math.Abs(Input.GetAxis("Horizontal")) > 0 || Math.Abs(Input.GetAxis("Vertical")) > 0){
            moveComponent.Move(Input.GetAxis("Vertical"),Input.GetAxis("Horizontal"));}

        if(Input.GetAxis("Mouse ScrollWheel") < 0 && mainCamera.orthographicSize < MaximoEspacioCamara){mainCamera.orthographicSize += 0.25f;}
        if(Input.GetAxis("Mouse ScrollWheel") > 0 && mainCamera.orthographicSize > MinimoEspacioCamara){mainCamera.orthographicSize -= 0.25f;}

        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        for(int i = 0; i < ship.GetGunComponents().Length; i++){
            GunComponent gun = ship.GetGunComponents()[i];
            if(!gun.isAimed(mouse)){gun.Aim(mouse);}
            if(Input.GetMouseButtonDown(0)){gun.Shoot();}
        }



    }

}
