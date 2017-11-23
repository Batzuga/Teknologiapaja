using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle2Critter : MonoBehaviour {

    public GameObject target;
    private Rigidbody rb;
	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay(Collider col)
    {
        if(col.gameObject.tag == "Beam")
        {
            transform.position = Vector3.Lerp(transform.position, target.transform.position, 10f * Time.deltaTime);
            rb.useGravity = false;
        }
    }
    public void SetGravity()
    {
        Debug.Log("GravityOn");
        rb.useGravity = true;
    }
}
