using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(ShipComponent))]
public class MoveComponent : MonoBehaviour
{
    private Rigidbody2D body;
    private ShipComponent ship;
    AudioSource source;
    
    void Start(){
        body = gameObject.GetComponent<Rigidbody2D>();
        ship = gameObject.GetComponent<ShipComponent>();
        source = gameObject.GetComponent<AudioSource>();
    }
    
    public void Move(float x, float y, bool isback){
        float v = 1;
        if (isback) {v = 0.25f;}
        body.AddRelativeForce(new Vector2(x * ship.GetVelocidadAvance() * v, 0));
        body.AddTorque(-y * body.inertia * ship.GetVelocidadGiro()*body.velocity.magnitude/10, ForceMode2D.Impulse);

        float val = body.velocity.magnitude/30f;
        source.volume = val;

    }
}
