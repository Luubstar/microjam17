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
    }

    public void TurnButtons(bool mode){
        botonMain.SetActive(mode);
    }

    public void ClickButton(int button){
        buttons[button-1].Select();
        buttons[button-1].onClick.Invoke();
    }
}
