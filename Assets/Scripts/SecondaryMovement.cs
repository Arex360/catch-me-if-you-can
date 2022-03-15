using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryMovement : MonoBehaviour
{
    public Transform body;
    public float speed;
    private CharacterController cc;
    public float gravity;
    void Start()
    {
        cc = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(IkController.aiminig){
            cc.enabled = true;
            float horizontalInput = Input.GetAxis("Horizontal") * -1;
            float verticalInput = Input.GetAxis("Vertical") * -1;
            Vector3 movement = new Vector3(horizontalInput, 0, verticalInput).normalized;
            if (movement == Vector3.zero)
            {
            return;
            }
            float mag = new Vector2(horizontalInput,verticalInput).magnitude;
            Quaternion targetRotation = Quaternion.LookRotation(movement);
             targetRotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                360 * Time.fixedDeltaTime);
             this.transform.rotation = targetRotation;
            cc.Move(this.transform.forward * speed * Time.deltaTime);
            if(!AnimationController.isGrounded){
                cc.Move(this.transform.up * gravity * Time.deltaTime);
            }
        }else{
            this.transform.position = body.position;
            cc.enabled = false;
        }
    }
}
