using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    private Rigidbody rigidbody;
    public GameObject rockEffect;
    public GameObject enemyEffect;
    private Transform crossHair;
    public GameObject curshSound;
    public float offsetY;
    public float damage;
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        crossHair = GameObject.FindGameObjectWithTag("cross").transform;
        this.transform.LookAt(new Vector3(crossHair.position.x,this.transform.position.y + offsetY,crossHair.position.z));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigidbody.MovePosition(rigidbody.position + this.transform.forward * speed * Time.fixedDeltaTime);
    }
  
    void OnTriggerEnter(Collider other)
    {
        print("bullet collided");
        
        if(other.CompareTag("rock")){
            GameObject _effect = Instantiate(rockEffect,this.transform.position,Quaternion.identity);
            GameObject impact = Instantiate(curshSound,this.transform.position,Quaternion.identity);
            Destroy(impact,0.7f);
            Destroy(_effect,0.7f);
            Destroy(this.gameObject);
        }
        if(other.CompareTag("enemy")){
            GameObject _effect = Instantiate(enemyEffect,this.transform.position,Quaternion.identity);
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            GameObject impact = Instantiate(curshSound,this.transform.position,Quaternion.identity);
            Destroy(impact,0.7f);
            enemyHealth.TakeDamage(damage);
            Destroy(_effect,0.7f);
            Destroy(this.gameObject);
        }
    }
}
