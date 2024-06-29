using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(ShipComponent))]
public class MoveComponent : MonoBehaviour
{
    private Rigidbody2D body;
    private ShipComponent ship;
    
    void Start(){
        body = gameObject.GetComponent<Rigidbody2D>();
        ship = gameObject.GetComponent<ShipComponent>();
    }
    
    public void Move(float x, float y){
        //TODO: Hacia atrás 25% max velocidad y no puede girar quieto
        body.AddRelativeForce(new Vector2(x * ship.GetVelocidadAvance(), 0));
        body.AddTorque(-y * body.inertia * ship.GetVelocidadGiro(), ForceMode2D.Impulse);
    }
}
