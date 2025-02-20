using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TriggerEuroTruck : MonoBehaviour
{
    [SerializeField] int index;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            GM_Euro_Drive.instance.HookTrailerTimeline();// index);
            this.gameObject.SetActive(false);
        }
    }

}
