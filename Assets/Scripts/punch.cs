using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class punch : MonoBehaviour
{
    public float damage;
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")){
            print("clicked");
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            health.TakeDamage(damage);
        }
    }
}
