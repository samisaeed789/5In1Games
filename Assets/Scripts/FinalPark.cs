using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPark : MonoBehaviour
{
   
    public Transform targetpoint;
    public float lerpDuration = 2.0f;


    private void Start()
    {
        int selCar = ValStorage.GetCarNumber()-1;
        targetpoint = transform.GetChild(selCar);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            StartCoroutine(MoveCarSmoothly(other.gameObject));
        }
    }

    private IEnumerator MoveCarSmoothly(GameObject Car)
    {

        this.transform.GetChild(3).gameObject.SetActive(false);
        GM_CD.instance.Celeb();


        Car.gameObject.GetComponent<Rigidbody>().isKinematic = true;

        Vector3 targetPos = targetpoint.position;  
        Vector3 startPos = Car.transform.position;
        Quaternion startRot = Car.transform.rotation;  
        Quaternion targetRot = targetpoint.rotation;  
        float elapsedTime = 0f;

        while (elapsedTime < lerpDuration)
        {
            Car.transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / lerpDuration);

            Car.transform.rotation = Quaternion.Lerp(startRot, targetRot, elapsedTime / lerpDuration);

            elapsedTime += Time.deltaTime; 
            yield return null; 
        }

        Car.transform.position = targetPos;
        Car.transform.rotation = targetRot;
    }
}
