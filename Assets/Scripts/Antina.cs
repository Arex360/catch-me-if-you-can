using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Antina : MonoBehaviour
{
    private Animator animator;
    public Antina hardRef;
    public float fillRate;
    public bool interact;
    public float fillInstance;
    public bool pending = false;
    public bool completed;
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }
    void Update()
    {
        if(!pending && Input.GetKey(KeyCode.E)){
            StartCoroutine(fill());
        }
        if(completed && interact){
            GameManager.activatedAntinas++;
            GameManager.instance.disableInteractiveUI();
            animator.SetTrigger("spin");
            this.enabled = false;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        GameManager.instance.currentAntina = this.transform;
        if(other.transform.CompareTag("Player")){
            interact = true;
            GameManager.instance.enableInteractUI();
        }        
    }
    void OnTriggerExit(Collider other)
    {
        if(other.transform.CompareTag("Player")){
            interact = false;
            GameManager.instance.disableInteractiveUI();
        }        
    }
    IEnumerator fill(){
        pending = true;
        if(interact)
        GameManager.instance.AddFill(fillInstance);
        yield return new WaitForSeconds(1/fillRate);
        pending = false;
    }
}
