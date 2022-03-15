using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brokenSystem : MonoBehaviour
{

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1.3f);
        foreach(Transform obj in this.transform){
            MeshCollider meshCollider = obj.GetComponent<MeshCollider>();
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            meshCollider.enabled = false;
            rb.useGravity = false;
        }
    }


}
