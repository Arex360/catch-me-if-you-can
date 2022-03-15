using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IkController : MonoBehaviour
{
    public static IkController controller;
    private Animator animator;
    public Quaternion rotation;
    public Ray aimPoint;
    public Vector3 hitpoint;
    public static Transform target;
    private playerController player;
    public Transform localTarget;
    public Transform parent;
    public static bool aiminig;
    public LayerMask mask;
    public Vector3 offset;
    MeshRenderer meshRenderer;
    public bool aimable;
   

    public Transform leftHand;
    public Transform leftHandHint;
    public Transform rightHand;
    public Transform rightHandHint;

    public Transform Foot;
    public float weight;
    void Start()
    {
        animator = this.GetComponent<Animator>();
        player = this.GetComponent<playerController>();
        target = localTarget;
        meshRenderer = localTarget.GetComponent<MeshRenderer>();
        controller = this;
        Cursor.visible = false;
    }

    void Update()
    {
        weight = player.playerState == GameState.GirlLift ? 1:0;
        aiminig = Input.GetMouseButton(1) && aimable;
        animator.SetBool("aiming",aiminig);
        meshRenderer.enabled = aiminig && (player.playerState == GameState.GunMovement);
        if(aiminig){
                this.transform.parent = parent;
                //body.LookAt(new Vector3(target.position.x,this.transform.position.y,target.position.z));
            }else{
                this.transform.parent = null;
                //body.localEulerAngles = Vector3.zero;
            }
        aimPoint = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(aimPoint,out RaycastHit hit,Mathf.Infinity,mask)){
            target.position = hit.point + offset;
        }
    }
    void OnAnimatorIK(int layerIndex)
    {
        animator.SetBoneLocalRotation(HumanBodyBones.Head,rotation);
        animator.SetIKPosition(AvatarIKGoal.LeftHand,leftHand.position);
        animator.SetIKPosition(AvatarIKGoal.RightHand,rightHand.position);

        animator.SetIKRotation(AvatarIKGoal.LeftHand,leftHand.rotation);
        animator.SetIKRotation(AvatarIKGoal.RightHand,rightHand.rotation);

        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand,weight);
        animator.SetIKPositionWeight(AvatarIKGoal.RightHand,weight);

        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand,weight);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand,weight);

        animator.SetIKHintPosition(AvatarIKHint.LeftElbow,leftHandHint.position);
        animator.SetIKHintPosition(AvatarIKHint.RightElbow,rightHandHint.position);

        animator.SetIKHintPositionWeight(AvatarIKHint.LeftElbow,weight);
        animator.SetIKHintPositionWeight(AvatarIKHint.RightElbow,weight);

        animator.SetIKRotation(AvatarIKGoal.LeftFoot,Foot.rotation);
        animator.SetIKRotation(AvatarIKGoal.RightFoot,Foot.rotation);

        animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot,weight);
        animator.SetIKRotationWeight(AvatarIKGoal.RightFoot,weight);

        
        if(aiminig){
           // animator.SetBoneLocalRotation(HumanBodyBones.Spine,spine.rotation);
        }
        
    }
}
