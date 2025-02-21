using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySplash : MonoBehaviour
{
    [SerializeField] ParticleSystem Splash;
    [SerializeField] bool IsPolice;
    [SerializeField] bool IsTruck;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") 
        {


            Transform pos = other.gameObject.transform;


            if (IsPolice) 
            {
                GM_PoliceDrive gm = GM_PoliceDrive.instance;
                gm?.CarFellOcean();
            }

            if (IsTruck) 
            {
                GM_Euro_Drive gm = GM_Euro_Drive.instance;
                gm?.CarFellOcean();
            }


            Splash.transform.position = pos.position;
            Splash.Play();
        }
    }
}
