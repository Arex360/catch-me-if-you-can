using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastDetector : MonoBehaviour
{
    private RaycastHit hit;
    private Ray ray;
    public float range;
    public LayerMask mask;
    public playerController controller;
    public bool isfacingWall;
    private bool over;
    void Start()
    {
        over = false;
    }

    // Update is called once per frame
    void Update()
    {
        isfacingWall = Physics.CheckSphere(this.transform.position,range,mask);
        if(isfacingWall){
            over = true;
            controller.climbAble = true;
        }else{
            if(over){
                //controller.ClimbOverEvent();
                over = false;
            }
        }
    }

    void OnDrawGizmos()
    {
       Gizmos.DrawSphere(this.transform.position,range);
    }
   
}
