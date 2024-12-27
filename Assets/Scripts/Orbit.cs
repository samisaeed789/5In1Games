using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public Transform sun; 
    public float orbitRadius = 5f; 
    public float orbitDuration = 5f; 
    public bool clockwise = true;

    private void Start()
    {
        OrbitG();
    }

    void OrbitG()
    {
       
        int numPoints = 100; 
        Vector3[] orbitPath = new Vector3[numPoints];

        for (int i = 0; i < numPoints; i++)
        {
            
            float angle = (i / (float)numPoints) * Mathf.PI * 2;
           
            orbitPath[i] = new Vector3(Mathf.Cos(angle) * orbitRadius, Mathf.Sin(angle) * orbitRadius, 0) + (Vector3)sun.position;
        }

       
        if (!clockwise)
        {
           
            System.Array.Reverse(orbitPath);
        }

      
        transform.DOPath(orbitPath, orbitDuration, PathType.CatmullRom, PathMode.Full3D, 10, Color.white)
                 .SetLoops(-1, LoopType.Restart) 
                 .SetEase(Ease.Linear);
    }
}
