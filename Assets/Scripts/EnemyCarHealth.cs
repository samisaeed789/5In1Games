using System;
using UnityEngine;
using UnityEngine.UI; 

public class EnemyCarHealth : MonoBehaviour
{
    public float health =1f;
    public Image healthBar; 
    public ParticleSystem smokeEffect;

    [SerializeField] RCC_AICarController ai;
        [SerializeField]Rigidbody rb;
 //   public ParticleSystem blastEffect; 
    public GameObject destroyedCarPrefab;
    //  public float destructionForce = 1000f;
    // public GameObject[] OffCar;

    public event Action OnEnemyDestroyed;

    public void ReduceHealth(float percentage)
    {
        health -= percentage;
        health = Mathf.Clamp(health, 0f, 1f); 


       
        if (healthBar != null)
        {
            healthBar.fillAmount = health;
        }
        if (health <= 0.3f && !smokeEffect.isPlaying)
        {
            StartSmoke();
            DestroyCar();
            healthBar.gameObject.SetActive(false);
        }

        //if (health <= 0f)
        //{
        //    DestroyCar();
        //}
    }

    private void StartSmoke()
    {
        if (smokeEffect != null)
        {
            smokeEffect.Play(); 
        }
    }

    private void DestroyCar()
    {
        //foreach(GameObject g in OffCar) 
        //{
        //    g.SetActive(false);
        //}
        rb.isKinematic = true;
        ai.enabled = false;
        ValStorage.TriggerEnemyDestroyed();
       // Instantiate(destroyedCarPrefab,this.transform.position,this.transform.rotation);
        //Destroy(this.gameObject);
    }
   


}
