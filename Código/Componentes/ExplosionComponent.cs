using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionComponent : MonoBehaviour
{
    AudioSource source;
    void Start(){
        source = gameObject.GetComponent<AudioSource>();
    }

    void OnEnd(){   
        source.Stop();
        Destroy(this.gameObject);   
    }
}
