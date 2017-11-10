using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseScript : MonoBehaviour {

    private bool inHouse;
    public GameObject node1;
    public GameObject node2;

	void Start ()
    {
        inHouse = false;
        node2.SetActive(false);
        node1.SetActive(true);
	}

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            node2.SetActive(true);
            node1.SetActive(false);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            node2.SetActive(false);
            node1.SetActive(true);
        }
    }
}
