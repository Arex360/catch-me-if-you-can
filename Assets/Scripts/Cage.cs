using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : MonoBehaviour
{
    public float fillRate;
    public bool interact;
    public float fillInstance;
    public bool pending = false;
    public bool completed;
    public GameObject broken;

    
    void Update()
    {
        if(!GameManager.instance.allAntinasActivated){
            return;
        }
        if(!pending && Input.GetKey(KeyCode.E)){
            StartCoroutine(fill());
        }
        if(completed && interact){
            GameManager.activatedAntinas++;
            GameManager.instance.disableInteractiveUI2();
            playerController controller = GameObject.FindObjectOfType<playerController>();
            controller.playerState = GameState.GirlLift;
            Instantiate(broken,this.transform.position,Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Player")){
            interact = true;
            GameManager.instance.enableInteractUI2();
        }        
    }
    void OnTriggerExit(Collider other)
    {
        if(other.transform.CompareTag("Player")){
            interact = false;
            GameManager.instance.disableInteractiveUI2();
        }        
    }
    IEnumerator fill(){
        GameManager.instance.cage = this;
        pending = true;
        if(interact)
        GameManager.instance.AddFill(fillInstance);
        yield return new WaitForSeconds(1/fillRate);
        pending = false;
    }
}
