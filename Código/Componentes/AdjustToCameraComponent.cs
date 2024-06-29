using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustToCameraComponent : MonoBehaviour
{
    private Camera mainCamera;
    void Start(){mainCamera = Camera.main;}
    void Update()
    {
        gameObject.transform.rotation = mainCamera.transform.rotation;       
    }
}
