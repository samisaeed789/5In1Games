using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CP_ET : MonoBehaviour
{
    GM_Euro_Drive gm;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            if (gm == null)
                gm = GM_Euro_Drive.instance;

            gm.CollectablePlay(isConfetti:true);
            this.gameObject.SetActive(false);
        }
    }
}
