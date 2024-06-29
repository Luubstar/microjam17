using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GunComponent : MonoBehaviour
{
    [SerializeField] private GameObject prefabBala;
    private int ultimoDisparo = -1;
    private TimeComponent time;
    private ShipComponent ship;

    private float horquillaAngulo = 1f;
    public void SetShip(ShipComponent s){ship = s;}
    public void SetTime(TimeComponent t){time = t;}

    public void Aim(Vector3 pos){
        pos.z = 0;
        Vector3 direction = (pos - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, ship.GetVelocidadDeRotacionDeTorreta() * Time.deltaTime);
    }

    public bool isAimed(Vector3 pos){
        pos.z = 0;
        Vector3 targetDirection = (pos - transform.position).normalized;
        float currentAngle = transform.eulerAngles.z;
        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        float angleDifference = Mathf.DeltaAngle(currentAngle, targetAngle);
        return Mathf.Abs(angleDifference) < horquillaAngulo;
        }

    public void Shoot(){
        if(canShoot()){
            ultimoDisparo = time.GetSegundos();
            GameObject bullet = Instantiate(prefabBala, transform.position, transform.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            bullet.GetComponent<BulletComponent>().SetShip(ship);
            rb.velocity = ship.GetComponent<Rigidbody2D>().velocity;
            rb.AddForce(transform.right * ship.GetFuerzaDeDisparo(), ForceMode2D.Impulse);
        }
    }

    public bool canShoot(){return ultimoDisparo - time.GetSegundos() > ship.GetTiempoRecarga() || ultimoDisparo == -1;}
}
