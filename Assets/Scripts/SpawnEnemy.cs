using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public Transform enemy;
    public float Delay;
    void Start()
    {
        Invoke(nameof(Spawn),Delay);
    }
    private void Spawn(){
        Instantiate(enemy.gameObject,this.transform.position,Quaternion.identity);
        Destroy(this.gameObject);
    }
}
