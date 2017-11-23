using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TractorBeam : MonoBehaviour {

    private Puzzle2Script p2s;

	// Use this for initialization
	void Start ()
    {
        p2s = GameObject.Find("Puzzle2").GetComponent<Puzzle2Script>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnCollisionEnter(Collision col)
    {
        p2s.moving = false;
    }
}
