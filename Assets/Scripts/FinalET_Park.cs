using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalET_Park : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }
            GM_Euro_Drive.instance.HandleCeleb(this.transform.GetChild(0));
            this.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
}
