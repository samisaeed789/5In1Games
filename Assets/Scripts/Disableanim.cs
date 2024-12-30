using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disableanim : MonoBehaviour
{
    private void OnEnable()
    {
        this.GetComponent<Animator>().enabled = true;

    }
    void _Disable() 
    {
        this.GetComponent<Animator>().enabled = false;

    }

    private void OnDisable()
    {
        this.GetComponent<Animator>().enabled = false;

    }
}
