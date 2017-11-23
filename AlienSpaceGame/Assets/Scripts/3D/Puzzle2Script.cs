using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle2Script : MonoBehaviour {

    public GameObject tB;
    private float speed;
    public int counter;
    public bool beaming;
    public bool moving;
    public GameObject beam;
    public Puzzle2Critter critter;

	// Use this for initialization
	void Start ()
    {
        try
        {
            critter = GameObject.Find("Critter").GetComponent<Puzzle2Critter>();
        }
        catch
        {

        }
        speed = 2f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Move(counter);
	}
    public void BeamSet()
    {
        if(beaming == true)
        {
            beaming = false;
            beam.GetComponent<MeshCollider>().enabled = false;
            beam.GetComponent<MeshRenderer>().enabled = false;
            critter.SetGravity();
            return;
        }
        if (beaming == false)
        {
            beaming = true;
            beam.GetComponent<MeshCollider>().enabled = true;
            beam.GetComponent<MeshRenderer>().enabled = true;
            
        }
    }
    void Move(int i)
    {
        if(moving)
        {
            switch (i)
            {
                case 1:
                    tB.transform.Translate(Vector3.right * speed * Time.deltaTime);
                    break;
                case 2:
                    tB.transform.Translate(-Vector3.right * speed * Time.deltaTime);
                    break;
                case 3:
                    tB.transform.Translate(Vector3.forward * speed * Time.deltaTime);
                    break;
                case 4:
                    tB.transform.Translate(-Vector3.forward * speed * Time.deltaTime);
                    break;
            }
        }  
    }
}
