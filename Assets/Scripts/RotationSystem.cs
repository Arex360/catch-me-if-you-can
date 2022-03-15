using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationSystem : MonoBehaviour
{
    public Transform body;
    public float offset;
    public float offset2;
    public float magnitude;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(IkController.aiminig){
            bool fire = Input.GetMouseButton(0);
            magnitude = new Vector2(Input.GetAxis("Vertical"),Input.GetAxis("Horizontal")).magnitude;
            if(fire)
                body.LookAt(new Vector3(IkController.target.position.x,this.transform.position.y,IkController.target.position.z));
            else
                body.LookAt(new Vector3(IkController.target.position.x,this.transform.position.y,IkController.target.position.z));
            if(magnitude < 0.2f){    
                Vector3 temp = body.transform.localEulerAngles;
                Vector3 localRot = body.transform.localEulerAngles;
                localRot.y = temp.y + offset;
                body.transform.localEulerAngles = localRot;
                print("hksdjlkds");
            }
            if(magnitude > 0.2f){
                Vector3 temp = body.transform.localEulerAngles;
                Vector3 localRot = body.transform.localEulerAngles;
                localRot.y = temp.y + offset2;
                print("offet 2");
                body.transform.localEulerAngles = localRot;
            }
        }
    }
}
