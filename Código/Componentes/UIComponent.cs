using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class UIComponent : MonoBehaviour
{
    private TimeComponent time;
    private PointComponent points;
    [SerializeField] private TMP_Text tiempoText;
    [SerializeField] private TMP_Text dineroText;
    [SerializeField] private TMP_Text conquistaText;
    [SerializeField] private GameObject botonMain;
    [SerializeField] private Button[] buttons;
    [SerializeField] private Button vidaBoton;
    [SerializeField] private Button balasBoton;
    [SerializeField] private Button cañonesBoton;
    [SerializeField] private Button velocidadBoton;
    [SerializeField] private Button añadirIA;
    public AIMaster aimaster;
    void Start()
    {
        time = gameObject.GetComponent<TimeComponent>();
        points = gameObject.GetComponent<PointComponent>();
        TurnButtons(false);
    }

    void Update()
    {
        tiempoText.SetText(time.GetSegundos() + " s");
        dineroText.SetText(points.GetMonedasJugador() + "$");
        conquistaText.SetText(points.Ganas() + " ");

        velocidadBoton.interactable = points.puedeMejorarVelocidad;
        balasBoton.interactable = points.puedeMejorarDaño;
        cañonesBoton.interactable = points.puedeMejorarCañones;
        vidaBoton.interactable = points.puedeCurarse;
        añadirIA.interactable = aimaster.canAddAllies();
    }

    public void TurnButtons(bool mode){
        botonMain.SetActive(mode);
    }

    public void ClickButton(int button){
        buttons[button-1].onClick.Invoke();
    }
}
