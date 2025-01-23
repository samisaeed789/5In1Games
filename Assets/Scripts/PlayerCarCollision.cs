using UnityEngine;

public class PlayerCarCollision : MonoBehaviour
{
    public float damagePercentage = 0.3f;
    bool hasCollided=false;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !hasCollided)
        {
            EnemyCarHealth enemyHealth = collision.gameObject.GetComponent<EnemyCarHealth>();
            if (enemyHealth != null)
            {
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
