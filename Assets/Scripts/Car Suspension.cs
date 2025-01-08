using UnityEngine;
using DG.Tweening;

public class CarSuspensionRelease : MonoBehaviour
{
    public float compressionAmount = 0.2f;   // How much the suspension is compressed
    public float releaseDuration = 1f;        // Time to release and rise
                                              //  public int bounceCount = 3;               // Number of bounces before the car settles
                                              // public float bounceStrength = 0.2f;       // The strength of the bouncing

    [SerializeField] ParticleSystem Dust;
    [SerializeField] Transform Pos;

    private Vector3 initialPosition;
    private bool isSuspensionReleased = false;


    void OnEnable()
    {

        // Store the initial position of the car
        initialPosition = transform.position;

        // Make sure the car starts in a compressed position
        transform.position = initialPosition - Vector3.up * compressionAmount;
        ApplyCompression();
        // When the car is enabled, release the suspension
        ReleaseSuspension();
        Dust.transform.position = Pos.position;
        if (Dust)
            Dust.Play();
    }

    
    void ApplyCompression()
    {
        // Compress the car downwards
        transform.position = initialPosition - Vector3.up * compressionAmount;
        isSuspensionReleased = false;
    }

    void ReleaseSuspension()
    {
        if (isSuspensionReleased) return; 

        isSuspensionReleased = true;

        Sequence suspensionSequence = DOTween.Sequence();

        suspensionSequence.Append(transform.DOMoveY(initialPosition.y, releaseDuration)
                                    .SetEase(Ease.OutElastic, 1f, 0.3f)); 

     
    }

}
