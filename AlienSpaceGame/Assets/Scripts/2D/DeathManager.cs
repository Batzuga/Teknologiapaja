using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManager : MonoBehaviour {

    private PlayerControls2d pc2d;
    private CameraFollow2d cf2d;

    // Use this for initialization
    void Awake()
    {
        pc2d = GameObject.Find("Player").GetComponent<PlayerControls2d>();
        cf2d = GameObject.Find("Main Camera").GetComponent<CameraFollow2d>();
    }
	
    public void DeathTriggered()
    {
        cf2d.hasStarted = false;
        cf2d.cameraState = 0;
        pc2d.canLaunch = true;
        pc2d.shipLaunched = false;
        pc2d.MoveToStart();
    }
}
