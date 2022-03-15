using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public Transform player;
    private Rigidbody rigidbody;
    private Animator animator;
    public EnemyState state;
    public float speed;
    public Vector3 offset;
    public Vector3 raycastSource;
    private Ray ray;
    private RaycastHit hit;
    public float range;
    public bool beingCollided;
    public float turnFactor = 2;
    EnemyHealth enemyHealth;
    public SphereCollider collider;
    public bool predictable;
    public float distance;
    public Vector2 threshold;
    public Vector2 thresold2;
    public float r;
    public Transform hand;
    public GameObject stone;
    public bool throwable;
    public Vector3 initailPosition;
    public Vector3 targetPosition;
    public bool onAir;
    private float interpolate = 0;
    public float height;
    public float destTime;
    public float targetDistance;
    void Start()
    {
        throwable = true;
        animator = this.GetComponent<Animator>();
        rigidbody = this.GetComponent<Rigidbody>();
        player = GameObject.FindObjectOfType<playerController>().transform;   
        enemyHealth = this.GetComponent<EnemyHealth>();
        collider.transform.GetComponent<MeshRenderer>().enabled = false;     
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(GameManager.instance.isCinematicScene){
            return;
        }

        distance = Vector3.Distance(this.transform.position,player.transform.position);
        if(distance > threshold[0] && distance < threshold[1] && predictable){
            StartCoroutine(Throw());
        }
        if(distance > thresold2[0] && distance < thresold2[1] && predictable){
            StartCoroutine(ThinkToJump());
        }
        if(enemyHealth.dead){
            return;
        }
        raycastSource = this.transform.position + offset;
        ray = new Ray(raycastSource,this.transform.forward);
        if(Physics.Raycast(ray,out hit,range)){
            if(hit.transform.CompareTag("obs") ||hit.transform.CompareTag("rock") ){
                beingCollided = true;
            }
        }else{
            beingCollided = false;
        }
        if(state == EnemyState.Chase){
            rigidbody.freezeRotation = false;
            animator.SetBool("walk",true);
            animator.SetBool("attacking",false);
            if(!beingCollided){
                this.transform.LookAt(player.position);
                rigidbody.MovePosition(this.transform.position + this.transform.forward * speed * Time.fixedDeltaTime);
            }else{
                rigidbody.MovePosition(this.transform.position + this.transform.right * speed * turnFactor* Time.fixedDeltaTime);
            }
            
        }else if(state == EnemyState.Attack){
            animator.SetBool("walk",false);
            animator.SetBool("attacking",true);
        }else if(state == EnemyState.Throw){
            ThrowStone();
        }else if(state == EnemyState.Jump){
            targetDistance = Vector3.Distance(this.transform.position,targetPosition);
            if(!onAir){
                DoJump();
                onAir = true;
            }else{
                if(targetDistance <= 1){
                    animator.SetBool("shouldJump",false);
                    state = EnemyState.Chase;
                }else{
                    ProjectileJump();
                }
            }
        }
        
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(this.transform.position+offset,this.transform.position + offset + this.transform.forward*range);        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")){
            state = EnemyState.Attack;
        }
    }
    private void ProjectileJump(){
        animator.SetBool("shouldJump",true);
        interpolate += Time.fixedDeltaTime;
        interpolate = interpolate % destTime;
        this.transform.position = MathParabola.Parabola(initailPosition,targetPosition,height,interpolate/destTime);
    }
    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player")){
            state = EnemyState.Chase;
        }
    }
    IEnumerator Throw(){
        predictable = false;
        r = Random.value;
        if(r > 0.75f){
            state = EnemyState.Throw;
        }
        yield return new WaitForSeconds(3f);
        predictable = true;
    }
    IEnumerator ThinkToJump(){
        initailPosition = this.transform.position;
        targetPosition = player.transform.position;
        predictable = false;
        r = Random.value;
        if(r > 0.75f){
            state = EnemyState.Jump;
        }
        yield return new WaitForSeconds(3f);
        predictable = true;
    }
    private void DoJump(){
        animator.SetTrigger("jump");
    }
    private void ThrowStone(){
        rigidbody.freezeRotation = true;
        animator.SetBool("walk",false);
        animator.SetBool("attacking",false);
        animator.SetTrigger("throw");
    }
    public void resetToChase(){
        throwable = true;
        state = EnemyState.Chase;
    }
    public void throwStoneObject(){
        GameObject _stone = Instantiate(stone,hand.transform.position,Quaternion.identity);
        Projectile projectile = _stone.GetComponent<Projectile>();
        projectile.target = player.transform;
        projectile.init();
        throwable = false;
    }
}
