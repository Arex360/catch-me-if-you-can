using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class abilitiesSystem : MonoBehaviour
{
    public static abilitiesSystem instance;
    private Animator animator;
    public GameObject jutsu1;
    public GameObject jutsu2;
    public bool pendingAbility;
    void Start()
    {
      instance = this;
      animator = this.GetComponent<Animator>();   
    }
    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if(!pendingAbility){
            if(Input.GetKeyDown(KeyCode.Z)){
                CamManager.instance.EnableJutsu1();
                jutsu1.SetActive(false);
                jutsu1.SetActive(true);
                pendingAbility = true;
                animator.SetBool("pose1",true);
            }
        }
        if(Input.GetKeyDown(KeyCode.X)){
            jutsu2.SetActive(true);
        }
    }
    public void DisableAllAbilities(){
        //jutsu1.SetActive(false);
        pendingAbility = false;
        animator.SetBool("pose1",false);
    }
}
