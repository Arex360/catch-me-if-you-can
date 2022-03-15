using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private MeshRenderer renderer;
    public List<MeshRenderer> parts;
    public Transform tip;
    public float rate;
    public GameObject bullet;
    public bool shootable;
    private bool aiming;
    void Start()
    {
        renderer = this.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        aiming = IkController.aiminig;
        renderer.enabled = aiming;
        foreach(MeshRenderer mesh in parts){
            mesh.enabled = aiming;
        }
        if(aiming && shootable && Input.GetMouseButton(0)){
            StartCoroutine(Shoot());
        }
    }
    IEnumerator Shoot(){
        shootable = false;
        GameObject _bullet = Instantiate(bullet,tip.position,Quaternion.identity);
        yield return new WaitForSeconds(1/rate);
        shootable = true;
    }
}
