using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Centipede : MonoBehaviour
{
    private Transform player;
    private Rigidbody rigidbody;
    public List<Transform> nodes;
    public Transform tail;
    public List<Vector3> history;
    public int length;
    public int gap;
    public float speed;
    public bool flusable;
    public int flushDelay;
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        player = GameObject.FindObjectOfType<playerController>().transform;
        flusable = true;
        Grow();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.LookAt(player.position);
        rigidbody.MovePosition(this.transform.position + this.transform.forward * speed * Time.fixedDeltaTime);
    }
    public void Grow(){
        for(int i = 0;i < length;i++){
            GameObject newtail = Instantiate(tail.gameObject);
            nodes.Add(newtail.transform);
        }
    }
}
