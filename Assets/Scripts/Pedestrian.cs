using UnityEngine;

public class Pedestrian : MonoBehaviour
{
    public Transform endPoint;    
    public float speed = 2f;     

    private Animator animator;   
    private bool isCrossing = false;
    private bool hasCrossed = false;
    public bool isRanOver = false;
    private Collider pedestrianCollider;

    private Rigidbody[] ragdollRigidbodies;  
    private Collider[] ragdollColliders;  
    private bool isRagdoll = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        ragdollColliders = GetComponentsInChildren<Collider>();
        pedestrianCollider = GetComponent<Collider>();
        SetRagdollState(false);
    }
    public void SetRagdollState(bool state)
    {
        foreach (var rb in ragdollRigidbodies)
        {
            rb.isKinematic = !state; 
        }

        foreach (var col in ragdollColliders)
        {
            col.enabled = state;
        }
        pedestrianCollider.enabled = !state;
        animator.enabled = !state;

        isRagdoll = state;
    }
    private void Update()
    {
        if (isCrossing && !hasCrossed  && !isRanOver)
        {
            MovePedestrian();
        }
    }

    
    public void StartCrossing()
    {
        if (hasCrossed)
            return;

        isCrossing = true;
        animator.SetBool("isWalking", true);
    }

    public void StopCrossing()
    {
        isCrossing = false;
        animator.SetBool("isWalking", false); 
    }

    private void MovePedestrian()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, endPoint.position, step);

        if (Vector3.Distance(transform.position, endPoint.position) < 0.1f)  
        {
            if (!hasCrossed)  
            {
                hasCrossed = true;
                animator.SetBool("isWalking", false);
            }
        }
    }

    public bool HasCrossed()
    {
        return hasCrossed;
    }
    
    public bool HasranOver()
    {
        return isRanOver;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && isCrossing)
        {
            SetRagdollState(true);
            isRanOver = true;
            Rigidbody carRigidbody = other.GetComponent<Rigidbody>();
            if (carRigidbody != null)
            {
                foreach (var rb in ragdollRigidbodies)
                {
                    rb.AddForce(carRigidbody.velocity * 5f, ForceMode.Impulse);
                }
            }
        }
    }
}
