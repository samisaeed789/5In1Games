using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vehicleEnterExit : MonoBehaviour
{
    public GameObject enterDoorButton;
    public GameObject carDoor;
    public GameObject triggerToEnter;
    public Transform rollDwonTrnasform;
    public GameObject seatTransfrom;
    public GameObject enterCharacter;
    public GameObject exitbutton;
    public AudioSource gamePlayBgMusic;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="doorTriger")
        {
            enterDoorButton.SetActive(true);
            triggerToEnter = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "doorTriger")
        {
            enterDoorButton.SetActive(false);
        }
    }
    public void enterFunction()
    {
        this.gameObject.GetComponent<Animator>().SetBool("openDoor", true);
        carDoor.GetComponent<Animator>().SetBool("carDoor", true);
        enterCharacter.SetActive(true);
        this.gameObject.SetActive(false);
        enterDoorButton.SetActive(false);
        //this.transform.position = triggerToEnter.transform.position;
        //this.transform.rotation = triggerToEnter.transform.rotation;
        //this.gameObject.GetComponent<thirdPersonInput>().enabled = false;

    }
    void delayToEnableGetInCar()
    {
        this.gameObject.GetComponent<Animator>().SetBool("openDoor", false);
        this.gameObject.GetComponent<Animator>().SetBool("getInCar", true);
        this.transform.rotation = rollDwonTrnasform.transform.rotation;
        this.transform.position = triggerToEnter.transform.position;
        //Invoke(nameof(delayTosit), 0.3f);

    }
    void delayTosit()
    {
        this.transform.position = rollDwonTrnasform.transform.position;
        this.transform.rotation = rollDwonTrnasform.transform.rotation;
        //Invoke(nameof(activeSitAnimaiton), 0.35f);
    }
    void activeSitAnimaiton()
    {
        this.gameObject.GetComponent<Animator>().SetBool("getInCar", false);
        this.gameObject.GetComponent<Animator>().SetBool("sitInCar", true);
        this.transform.position = seatTransfrom.transform.position;
        this.transform.rotation = seatTransfrom.transform.rotation;
    }

}
