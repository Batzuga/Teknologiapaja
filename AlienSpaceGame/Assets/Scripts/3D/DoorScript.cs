using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

    public GameObject door;
    private bool opening;
    public Vector3 startPos;
    public Vector3 endPos;

    private int wiring;
    public string doorName;
    private bool locked;

	// Use this for initialization
	void Start ()
    {
        PlayerPrefs.DeleteAll();
        locked = false;
        wiring = PlayerPrefs.GetInt("WiringCollected");
        opening = false;
        doorName = this.gameObject.name;
        CheckIfLocked();
	}
	
    void CheckIfLocked()
    {
        if(doorName == "SpaceDoor" && wiring == 0)
        {
            locked = true;
        }
        if(doorName == "Puzzle1Door" && PlayerPrefs.GetInt("Puzzle1Completed") == 0)
        {
            locked = true;
        }
        if(doorName == "Puzzle1Door" && PlayerPrefs.GetInt("Puzzle1Completed") == 1)
        {
            Debug.Log("DOOR UNLOCKED");
            locked = false;
        }
    }
    public void CallOpen()
    {
        CheckIfLocked();
    }
	void Update ()
    {
        MoveDoor();
	}

    void MoveDoor()
    {
        //CHECKS IF DOOR IS OPENING OR CLOSING AND OPENS AND CLOSES
        if(!locked)
        {
            switch (opening)
            {
                case true:
                    door.transform.localPosition = Vector3.Lerp(door.transform.localPosition, endPos, 10f * Time.deltaTime);
                    break;

                case false:
                    door.transform.localPosition = Vector3.Lerp(door.transform.localPosition, startPos, 10f * Time.deltaTime);
                    break;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //WHEN YOOU ENTER TRIGGER IT OPENS THE DOOR
        if(other.gameObject.tag == "Player")
        {
            opening = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        //AND HERE IT CLOSES
        if(other.gameObject.tag == "Player")
        {
            opening = false;
        }
    }
}
