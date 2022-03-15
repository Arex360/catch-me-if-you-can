using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public bool rotate;
    public Transform targetRotation;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(rotate){
            this.transform.rotation = targetRotation.rotation;
        }
        this.transform.position = player.position + offset;
    }
}
