using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TriggerEuroTruck : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            GM_Euro_Drive.instance.HookTrailerTimeline();
            this.gameObject.SetActive(false);
        }
    }

}
