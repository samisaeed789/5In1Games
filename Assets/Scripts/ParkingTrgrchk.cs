using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkingTrgrchk : MonoBehaviour
{
    public ParticleSystem[] lvlconfti;
    private bool hasParked = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !hasParked) 
        {
            PlayParticles();
            ParkingGm.instance.CarFinalPark();
            hasParked = true;
        }
    }

    void PlayParticles()
    {
        foreach (ParticleSystem particle in lvlconfti)
        {
            particle.Play();
        }
    }

}
