using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable_CD : MonoBehaviour
{

    [SerializeField] bool Coins;
    [SerializeField] bool Cash;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Coins)
            {
                GM_CD.instance?.CollectablePlay(isCoin: true);
            }
            if (Cash)
            {
                GM_CD.instance?.CollectablePlay(isCash: true);
            }
            this.transform.parent.gameObject.SetActive(false);

        }
    }
}
