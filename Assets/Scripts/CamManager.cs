using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamManager : MonoBehaviour
{
    public GameObject mainCam;
    public GameObject climbCam;
    public GameObject cineCam;
    public GameObject jutsu1;
    public GameObject jutsu2;
    public GameObject pickCam;
    public GameObject lastScene;
    public static CamManager instance;
    public GameObject leaving;
    void Start()
    {
        instance = this;
    }
    public void disableAll(){
        mainCam.SetActive(false);
        climbCam.SetActive(false);
        jutsu1.SetActive(false);
        cineCam.SetActive(false);
        pickCam.SetActive(false);
        lastScene.SetActive(false);
        leaving.SetActive(false);
    }
    public void EnableMainCam(){
        disableAll();
        mainCam.SetActive(true);
        print("enabling mainCam");
        abilitiesSystem.instance.DisableAllAbilities();
    }
    public void EnableClimbCam(){
        disableAll();
        climbCam.SetActive(true);
        cineCam.SetActive(true);
    }
    public void EnableJutsu1(){
        disableAll();
        jutsu1.SetActive(true);
        print("enabling jutsu cam");
    }
    public void EnableJutsu2(){
        disableAll();
        jutsu2.SetActive(true);
        print("enabling jutsu cam");
    }
    public void EnablePickCam(){
        disableAll();
        pickCam.SetActive(true);
    }
    public void EnableLastScene(){
        GameManager.instance.isCinematicScene = true;
        disableAll();
        lastScene.SetActive(true);
    }
    public void enableLeaving(){
        disableAll();
        playerController player = GameObject.FindObjectOfType<playerController>();
        GameObject canvas = GameObject.FindGameObjectWithTag("canvas");
        Destroy(canvas);
        Destroy(player.gameObject);
        leaving.SetActive(true);
        
    }
}
