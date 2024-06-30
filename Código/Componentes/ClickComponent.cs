using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickComponent : MonoBehaviour
{
    AudioSource source;
    void Start(){
        source = gameObject.GetComponent<AudioSource>();
    }

    public void Click(){
        source.Play();
    }
}
