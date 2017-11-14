using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle1Script : MonoBehaviour {

    public GameObject[] objects;
    public GameObject emptyObject;
    public GameObject[] objArray = new GameObject [3];
    public int[] puzzleSequence = new int[3];
    public int[] correctSequence = new int[3];

    public GameObject[] visuals;
    public int counter;

    public bool completed;
    public GameObject door;
    private DoorScript ds;
    
    // Use this for initialization
    void Start()
    {
        ds = door.GetComponent<DoorScript>();
        counter = 0;
        completed = false;
    }

    public void AddIcon(int i)
    {
        Debug.Log("1");
        objArray[counter] = Instantiate(visuals[i], objects[counter].transform.position, objects[counter].transform.rotation) as GameObject;
        puzzleSequence[counter] = i+1;
        if(counter < 2)
        {
            Debug.Log("2");
            counter++;
            return;
        }
        if(counter == 2)
        {
            Debug.Log("3");
            CheckSequence();
        }
    }
    IEnumerator WrongSequence()
    {
        yield return new WaitForSeconds(1.0f);
        puzzleSequence[0] = 0;
        puzzleSequence[1] = 0;
        puzzleSequence[2] = 0;
        Destroy(objArray[0]);
        Destroy(objArray[1]);
        Destroy(objArray[2]);

        counter = 0;
    }
    void CheckSequence()
    {
        for(int i = 0; i < 2; i++)
        {
            if (puzzleSequence[i] != correctSequence[i])
            {
                StartCoroutine(WrongSequence());
                return;
            }
        }
        completed = true;
        PlayerPrefs.SetInt("Puzzle1Completed", 1);
        ds.CallOpen();
    }
    // Update is called once per frame
    void Update () {
		
	}
}
