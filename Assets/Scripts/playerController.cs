using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public GameObject footstep;
    public bool climbAble;
    private Animator animator;
    public Vector2 inputs;
    public GameState playerState;
    private Rigidbody rigidbody;
    private CharacterController cc;
    public GameObject girl;
    public RaycastDetector detector;
    public bool ClimbOver;
    public float speed;
    public float walkSpeed;
    public bool controllable;
    void Start()
    {
        animator = this.GetComponent<Animator>();
        rigidbody = this.GetComponent<Rigidbody>();
        cc = this.GetComponent<CharacterController>();
        girl.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!controllable)
        return;
       InputSystem();
       animator.applyRootMotion = !IkController.aiminig;
    }
    void FixedUpdate(){
         if(!controllable)
        return;
        if(playerState == GameState.NoGunMoment){
            NoGunMovement();
        }else if(playerState == GameState.GunMovement){
            GunMovement();
        }
        else if(playerState == GameState.ClimbMovement){
            Climb();
        }else if(playerState == GameState.GirlLift){
            GirlLiftMovement();
        }
}
    private void NoGunMovement(){
        EmotionController.instance.Neural();
        changeCenter(0.98f);
        animator.SetFloat("climbSpeed",1);
        animator.SetBool("climb",false);
        animator.SetBool("fastRun",false);
        animator.applyRootMotion = true;
        float horizontalInput = Input.GetAxis("Horizontal") * -1;
        float verticalInput = Input.GetAxis("Vertical") * -1;
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput).normalized;
        if (movement == Vector3.zero)
        {
        return;
        }
        float mag = new Vector2(horizontalInput,verticalInput).magnitude;
        animator.SetFloat("run",mag);
        Quaternion targetRotation = Quaternion.LookRotation(movement);
        targetRotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                360 * Time.fixedDeltaTime);
        this.transform.rotation = targetRotation;
    }
    private void GirlLiftMovement(){
        girl.SetActive(true);
        changeCenter(0.98f);
        animator.SetFloat("climbSpeed",1);
        animator.SetBool("climb",false);
        animator.SetBool("fastRun",true);
        animator.applyRootMotion = true;
        float horizontalInput = Input.GetAxis("Horizontal") * -1;
        float verticalInput = Input.GetAxis("Vertical") * -1;
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput).normalized;
        if (movement == Vector3.zero)
        {
        return;
        }
        float mag = new Vector2(horizontalInput,verticalInput).magnitude;
        animator.SetFloat("run",mag);
        Quaternion targetRotation = Quaternion.LookRotation(movement);
        targetRotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                360 * Time.fixedDeltaTime);
        this.transform.rotation = targetRotation;
    }
     private void GunMovement(){
         EmotionController.instance.Angry();
        changeCenter(0.98f);
        //sanimator.applyRootMotion = false;
        animator.SetFloat("climbSpeed",1);
        animator.SetBool("climb",false);
        float horizontalInput = Input.GetAxis("Horizontal") * -1;
        float verticalInput = Input.GetAxis("Vertical") * -1;
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput).normalized;
        if (movement == Vector3.zero)
        {
        return;
        }
        float mag = new Vector2(horizontalInput,verticalInput).magnitude;
        animator.SetFloat("run",mag);
        Quaternion targetRotation = Quaternion.LookRotation(movement);
        targetRotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                360 * Time.fixedDeltaTime);
        this.transform.rotation = targetRotation;
        //cc.Move(this.transform.forward * speed * movement.magnitude* Time.deltaTime);
    }
    private void Climb(){
        changeCenter(2.5f);
        animator.SetBool("climb",true);
        float y = detector.isfacingWall ? Input.GetAxis("Vertical"):0;
        animator.SetFloat("run",y);
        animator.SetFloat("climbSpeed",y != 0 ? 1:0);
        if(!detector.isfacingWall){
            ClimbOverEvent();
        }
    }
    private void changeCenter(float y){
        Vector3 center = cc.center;
        center.y = y;
        cc.center = center;
    }
    public void ClimbOverEvent(){
        CamManager.instance.EnableMainCam();
        animator.SetTrigger("finish");
        climbAble = false;
        cc.Move(this.transform.up * 1);
        playerState = GameState.NoGunMoment;
    }
    private void InputSystem(){
        if(Input.GetKeyDown(KeyCode.C) && climbAble){
            CamManager.instance.EnableClimbCam();
            playerState = GameState.ClimbMovement;
            ClimbOver = false;
        }
        if(Input.GetMouseButtonDown(1)){
            playerState = GameState.GunMovement;
            //animator.SetBool("carriedWeapon",true);
        }else if(Input.GetMouseButtonUp(1)){
            playerState = GameState.NoGunMoment;
        }
        if(Input.GetMouseButton(0)){
            animator.SetBool("fire",true);
        }else if(Input.GetMouseButtonUp(0)){
            animator.SetBool("fire",false);
        }
    }
}
