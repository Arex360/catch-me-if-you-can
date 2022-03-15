using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionSystem : MonoBehaviour
{
   
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "climbable"){
            playerController player = GameObject.FindObjectOfType<playerController>();
            player.climbAble = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
         if(other.tag == "climbable"){
            playerController player = GameObject.FindObjectOfType<playerController>();
            player.transform.GetComponent<Animator>().SetTrigger("finish");
            player.climbAble = false;
            player.GetComponent<CharacterController>().Move(player.transform.up * 2);
  
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if(other.transform.CompareTag("enemy")){
            EnemyHealth enemy = other.transform.GetComponent<EnemyHealth>();
            enemy.TakeDamage(Mathf.Infinity);
        }
    }
}
