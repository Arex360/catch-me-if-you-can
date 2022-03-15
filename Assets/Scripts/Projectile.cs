using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
   public float destTime = 2f;
	public float height = 5f;
	public Transform target;
	public float interpolate;
    public Vector3 distination;
    public bool start;
    private Vector3 initialPosition;
    public GameObject effect;
    public float weight = 10f;
    public float radius;
    public Vector3 offset;
    public LayerMask mask;
    void Start()
    {
        initialPosition = this.transform.position;
        distination = target.position;
        start = false;
        Invoke(nameof(enable),0.7f);
        Destroy(this.gameObject,6f);
    }
    void enable(){
        start = true;
        distination = target.position;
    }
    // Update is called once per frame
    void Update()
    {
        bool collised = Physics.CheckSphere(this.transform.position + offset,radius,mask);
        if(collised){
            Instantiate(effect,this.transform.position,Quaternion.identity);
            Destroy(this.gameObject);
        }
        if(start){
            interpolate += Time.deltaTime;
		    interpolate = interpolate % destTime;
		//transform.position = MathParabola.Parabola(Vector3.zero, Vector3.forward * 10f, 5f, Animation / destTime);
		    transform.position = MathParabola.Parabola(initialPosition, distination, height, interpolate / destTime);
        }
    }
    void OnCollisionEnter(Collision other)
    {
        Destroy(this.gameObject);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")){
            PlayerHealth controller = other.GetComponent<PlayerHealth>();
            controller.TakeDamage(weight);
        }
        
        if(!other.CompareTag("enemy")){
            Instantiate(effect,this.transform.position,Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
    public void init(){
        initialPosition = this.transform.position;
        distination = target.position;
        start = true;
    }
    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(this.transform.position + offset,radius);
    }   
}
