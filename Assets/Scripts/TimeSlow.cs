using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlow : MonoBehaviour
{
   
    void OnTriggerEnter(Collider other)
    {
        if(other.name == "hero"){
            Animator animator = other.GetComponent<Animator>();
            animator.SetFloat("time",0.5f);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.name == "hero"){
            Animator animator = other.GetComponent<Animator>();
            animator.SetFloat("time",1f);
        }
    }
}
