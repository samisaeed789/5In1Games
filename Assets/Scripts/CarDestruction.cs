using UnityEngine;

public class CarDestruction : MonoBehaviour
{
    private void Start()
    {
        DestroyCar();
    }
    void DestroyCar()
    {

        // Get all the rigidbodies of the destroyed car parts (assuming all parts have Rigidbody components)
        Rigidbody[] rigidbodies = this.GetComponentsInChildren<Rigidbody>();

        // Apply force to each part
        foreach (Rigidbody rb in rigidbodies)
        {
            // Apply a force to simulate destruction
            if (rb.CompareTag("CarBody"))
            {
                // Apply an upwards force to the body
                rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
            }
            else if (rb.CompareTag("CarTire"))
            {
                // Apply a sideways force to the tires
                rb.AddForce(new Vector3(10, 0, 10), ForceMode.Impulse);
            }
        }

        // Optionally, destroy the original enemy car after instantiating the destroyed version
        Destroy(gameObject,5f);
    }
}
