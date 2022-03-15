using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Transform foot;
    public float range;
    public LayerMask mask;
    private Animator animator;
    private playerController controller;
    private Vector2 inputs;
    public static bool isGrounded;
    void Start()
    {
        this.animator = this.GetComponentInChildren<Animator>();
        controller = this.GetComponent<playerController>();
    }

    // Update is called once per frame
    void Update()
    {
        inputs = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
        animator.SetBool("aim",controller.playerState == GameState.GunMovement);
        animator.SetFloat("dashMag",inputs.magnitude);
        isGrounded = Physics.CheckSphere(foot.position,range,mask);
        if(controller.playerState != GameState.ClimbMovement){
            animator.SetBool("fall",!isGrounded && !IkController.aiminig);
        }
        if(Input.GetKeyDown(KeyCode.Space)){
            animator.SetTrigger("dash");
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(foot.position,range);
    }
}
