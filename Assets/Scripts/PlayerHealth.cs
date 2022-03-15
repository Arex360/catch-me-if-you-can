using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{
    public Image image;

    public float totalHealth;
    public float health = 300;
    [Range(0,1)]
    public float fill;
    public float targetFill;
    public float speed = 5f;
    public bool dead;
    private Animator animator;
    public GameObject effect;
    public Vector3 offset;
    public GameObject explosion;
    public GameObject saturation;
    void Start()
    {
        totalHealth = health;
        targetFill = health;
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        fill = Mathf.Lerp(fill,targetFill,speed * Time.deltaTime);
        image.fillAmount = fill;
    }
    public void TakeDamage(float damage){
        health -= damage;
        health = Mathf.Clamp(health,0,Mathf.Infinity);
        float percentage = (health/totalHealth);
        targetFill = percentage;
        if(health == 0 && !dead){
            dead = true;
            animator.SetTrigger("die");
            saturation.SetActive(true);
            Invoke(nameof(Die),2f);
        }
    }
    public void Die(){
        LoadScene();
        Destroy(this.gameObject);
    }
    public void LoadScene(){
        SceneManager.LoadScene(0);
    }
}
