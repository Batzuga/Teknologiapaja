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
    public GameObject lo;
    public Material mat1;
    // Use this for initialization
    void Start()
    {
        ds = door.GetComponent<DoorScript>();
        counter = 0;
        completed = false;
    }
    void LightOn()
    {
        lo.GetComponent<MeshRenderer>().material = mat1;
    }
    public void AddIcon(int i)
    {
        objArray[counter] = Instantiate(visuals[i], objects[counter].transform.position, objects[counter].transform.rotation) as GameObject;
        puzzleSequence[counter] = i+1;
        if(counter < 2)
        {
            counter++;
            return;
        }
        if(counter == 2)
        {
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
        for(int i = 0; i <= 2; i++)
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
        LightOn();
    }
    // Update is called once per frame
    void Update () {
		
	}
}
