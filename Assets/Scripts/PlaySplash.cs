using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySplash : MonoBehaviour
{
    [SerializeField] ParticleSystem Splash;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") 
        {
            Transform pos = other.gameObject.transform;
            GM_PoliceDrive gm = GM_PoliceDrive.instance;
            gm.CarFellOcean();
            Splash.transform.position = pos.position;
            Splash.Play();
        }
    }


}
