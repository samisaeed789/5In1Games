using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEnter : MonoBehaviour
{
    [SerializeField] bool Swat;
    [SerializeField] bool Ford;
    [SerializeField] bool Regular;
    GM_PoliceDrive gm;
    private void Start()
    {
        gm = GM_PoliceDrive.instance;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Swat)
            {
                if (GM_PoliceDrive.instance) 
                {
                    gm.SetGP(1);
                }
            }
            if (Ford)
            {
                    gm.SetGP(2);
            }

            if (Regular)
            {
                gm.SetGP(0);
            }
        }
    }

}
