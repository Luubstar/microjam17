using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIComponent : MonoBehaviour
{
    private TimeComponent time;
    private PointComponent points;
    [SerializeField] private TMP_Text tiempoText;
    [SerializeField] private TMP_Text dineroText;
    [SerializeField] private TMP_Text conquistaText;
    void Start()
    {
        time = gameObject.GetComponent<TimeComponent>();
        points = gameObject.GetComponent<PointComponent>();
    }

    void Update()
    {
        tiempoText.SetText(time.GetSegundos() + " s");
        dineroText.SetText(points.GetMonedasJugador() + "$");
        conquistaText.SetText(points.Ganas() + " ");
    }
}
