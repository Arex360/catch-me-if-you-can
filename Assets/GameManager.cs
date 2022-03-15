using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    public bool isCinematicScene;
    public TextMeshProUGUI killText;
    public TextMeshProUGUI antinaText;
    public static int kills = 0;
    public static GameManager instance;
    public static int activatedAntinas;
    public Transform currentAntina;
    public Cage cage;
    public Image fill;
    public Image fill2;
    public float targetFill;
    public GameObject interactUI;
    public GameObject interactUI2;
    public bool allAntinasActivated;
    public GameObject message2;
    public GameObject countdown;
    public float timeOver;
    public int sceneIndex;
    void Start()
    {
        instance = this;
    }
    void Update()
    {
        allAntinasActivated = activatedAntinas >= 3? true:false;
        killText.text = $"kills : {kills}";
        activatedAntinas = Mathf.Clamp(activatedAntinas,0,3);
        antinaText.text = $"{activatedAntinas} / 3 activated antinas";
        targetFill = Mathf.Clamp(targetFill,0,1);
        if(fill){
            fill.fillAmount = targetFill;
        }
        if(fill2){
            fill2.fillAmount = targetFill;
        }
        if(targetFill >= 1){
            if(currentAntina){
               currentAntina.GetComponent<Antina>().completed = true;
               targetFill = 0;
            }
            if(cage){
                cage.completed = true;
                countdown.SetActive(true);
                Timer timer = GameObject.FindObjectOfType<Timer>();
                timer.timeRemaining = timeOver;
                timer.timerIsRunning = true;
            }
        }
    }
    public void enableInteractUI(){
        fill.fillAmount = 0;
        targetFill = 0;
        interactUI.SetActive(true);
    }
    public void disableInteractiveUI(){
        interactUI.SetActive(false);
    }
     public void enableInteractUI2(){
        fill2.fillAmount = 0;
        targetFill = 0;
        if(allAntinasActivated){
            interactUI2.SetActive(true);
        }else{
            message2.SetActive(true);
        }
        
    }
    public void disableInteractiveUI2(){
        if(allAntinasActivated){
            interactUI2.SetActive(false);
        }else{
            message2.SetActive(false);
        }
    }
    public void AddFill(float ammount){
        targetFill += ammount;
    }
    void OnEnable()
    {
        fill.fillAmount = 0;
        targetFill = 0;
    }
}
