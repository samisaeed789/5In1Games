using UnityEngine;

public class PlayerCarCollision : MonoBehaviour
{

    public float damagePercentage = 0.3f;
    bool hasCollided=false;
    MySoundManager soundman;
    private void Start()
    {
        soundman = MySoundManager.instance;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !hasCollided)
        {
            EnemyCarHealth enemyHealth = collision.gameObject.GetComponent<EnemyCarHealth>();
            if (enemyHealth != null)
            {
                soundman?.PlayHitSound();
                enemyHealth.ReduceHealth(damagePercentage);
                hasCollided = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            hasCollided = false;
        }
    }

}
