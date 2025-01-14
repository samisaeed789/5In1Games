using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    Animation animationComponent;
    public void HitEffect() 
    {

        Animator animator = GetComponent<Animator>();

        if (animator != null)
        {
            
            animator.Play("ObsAnim");  
        }
        else
        {
          //  Debug.LogError("Animator component not found on this GameObject.");
        }
    }
}
