using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIComponent : MonoBehaviour
{
    private TimeComponent time;
    private PointComponent points;
    [SerializeField] private TMP_Text tiempoText;
    [SerializeField] private TMP_Text dineroText;
    [SerializeField] private GameObject botonMain;
    [SerializeField] private Button[] buttons;
    [SerializeField] private Button vidaBoton;
    [SerializeField] private Button balasBoton;
    [SerializeField] private Button cañonesBoton;
    [SerializeField] private Button velocidadBoton;
    [SerializeField] private Button añadirIA;
    public GameObject pausaMenu;
    public MapGeneration map;
    public Slider slider;
    public GameObject Victoria;
    public GameObject Derrota;
    public AIMaster aimaster;
    public GameObject help;
    void Start()
    {
        time = gameObject.GetComponent<TimeComponent>();
        points = gameObject.GetComponent<PointComponent>();
        TurnButtons(false);
    }

    void Update()
    {
        slider.value = points.Ganas();

        tiempoText.SetText(time.ToString());
        dineroText.SetText(points.GetMonedasJugador() +"");

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

    public void Pause(bool val){
        pausaMenu.SetActive(val);
    }

    public void Retry(){
        SceneManager.LoadScene(1);
    }

    public void Exit(){
        Application.Quit();
    }

    public void Win(bool w){
        if(w){Victoria.SetActive(true);}
        else{Derrota.SetActive(false);}
    }

    public void Help(bool h){help.SetActive(h);}
}
