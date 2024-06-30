using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletComponent : MonoBehaviour
{
    [SerializeField] private int TTL;
    private ShipComponent ship;
    public GameObject Explosion;
    
    void Start()
    {
        StartCoroutine("KillBullet");
    }

    public void SetShip(ShipComponent s){ship = s;}

    IEnumerator KillBullet(){
        yield return new WaitForSeconds(TTL);
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.GetComponent<ShipComponent>() != null){
            ShipComponent collidingShip = col.gameObject.GetComponent<ShipComponent>();
            if(collidingShip.gameObject != ship.gameObject && collidingShip.GetEquipo() != ship.GetEquipo() && collidingShip.alive){
                collidingShip.Dañar(ship.GetDañoBalas());
                Instantiate(Explosion, gameObject.transform.position, gameObject.transform.rotation);
                Destroy(this.gameObject);
            }
        }
        else if(!col.isTrigger){
            Instantiate(Explosion, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
