using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GunComponent : MonoBehaviour
{
    [SerializeField] private GameObject prefabBala;
    [SerializeField] private GameObject[] cañonesNivel1;
    [SerializeField] private GameObject[] cañonesNivel2;
    [SerializeField] private GameObject[] cañonesNivel3;
    [SerializeField] private GameObject[] cañonesNivel4;
    private GameObject[] cañonesActivos;
    private int ultimoDisparo = -1;
    public TimeComponent time;
    private ShipComponent ship;
    private float horquillaAngulo = 1f;
    int level = 1;
    private AudioSource source;

    void Start(){
        cañonesActivos = cañonesNivel1;
        source = gameObject.GetComponent<AudioSource>();
    }

    public void SetShip(ShipComponent s){ship = s;}
    public void SetTime(TimeComponent t){time = t;}

    public void UpdateLevel(){
        level++;    
        if(level==2){ActivarCañones(cañonesNivel2);}
        else if(level==3){ActivarCañones(cañonesNivel3);}
        else if(level==4){ActivarCañones(cañonesNivel4);}
    }
 
    public void ActivarCañones(GameObject[] c){
        foreach(GameObject cañon in cañonesActivos){cañon.SetActive(false);}
        foreach(GameObject cañon in c){cañon.SetActive(true);}
        cañonesActivos = c;
    }

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
            foreach(GameObject c in cañonesActivos){
                GameObject bullet = Instantiate(prefabBala, c.transform.position, transform.rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                bullet.GetComponent<BulletComponent>().SetShip(ship);
                rb.velocity = ship.GetComponent<Rigidbody2D>().velocity;
                rb.AddForce(transform.right * ship.GetFuerzaDeDisparo(), ForceMode2D.Impulse);
            }
            source.Play();
        }
    }

    public bool canShoot(){return ultimoDisparo - time.GetSegundos() > ship.GetTiempoRecarga() || ultimoDisparo == -1;}
}
