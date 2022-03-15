using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class centinode : MonoBehaviour
{
    public Transform head;
    public float speed;
    private Rigidbody rigidbody;

  void Start(){
      rigidbody = this.GetComponent<Rigidbody>();
  }
    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.LookAt(head);
        rigidbody.MovePosition(this.transform.position + this.transform.forward * speed * Time.fixedDeltaTime);
    }
}
