using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow3d : MonoBehaviour {

    private GameObject player;
    private Vector3 pp;
    private bool freezeCamera;

	void Start ()
    {
        player = GameObject.Find("Player3D");
        freezeCamera = false;
        pp = player.transform.position;
        transform.position = new Vector3(pp.x, 20f, pp.z - 17f);
    }

    void Update ()
    {
        pp = player.transform.position;
        //transform.position = new Vector3(pp.x + 10f, 16f, pp.z - 10f);
        if(!freezeCamera)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(pp.x, 20f, pp.z - 17f), 2f * Time.deltaTime);
        }
    }
    
    void SetFreeze(bool b)
    {
        freezeCamera = b;
    }
       
}
