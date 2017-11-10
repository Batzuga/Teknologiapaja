using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2d : MonoBehaviour {

    private GameObject player;
    private GameObject goal;

    public int cameraState;
    public bool isZooming;
    public bool hasStarted;

    private Vector3 startPos;
    private Vector3 playerPos;
    private Vector3 goalPos;
    private Vector3 midPos;

    private float camRange;
    public float camMaxRange;
    private float camMinRange;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float toY;
    public float toX;

    private GameObject tapScreen;

	// Use this for initialization
	void Start ()
    {
        camMinRange = 5;
        camRange = GetComponent<Camera>().orthographicSize;
        startPos = new Vector3(0, 0, -10f);
        player = GameObject.FindGameObjectWithTag("Player");
        goal = GameObject.FindGameObjectWithTag("Goal");
        playerPos = player.transform.position;
        goalPos = goal.transform.position;
        midPos = new Vector3((goalPos.x + playerPos.x)/2, (goalPos.y + playerPos.y) / 2, - 10f);
        tapScreen = GameObject.Find("TapField");
	}
	
	// Update is called once per frame
	void Update ()
    {
        MoveToStates(cameraState);
        CheckMaxes();
	}
    void CheckMaxes()
    {
        playerPos = player.transform.position;
        if(player.transform.position.x < minX)
        {
            toX = minX;
        }
        if(player.transform.position.x > maxX)
        {
            toX = maxX;
        }
        if(player.transform.position.x < maxX && player.transform.position.x > minX)
        {
            toX = player.transform.position.x;
        }

        if(player.transform.position.y < minY)
        {
            toY = minY;
        }
        if(player.transform.position.y > maxY)
        {
            toY = maxY;
        }
        if(player.transform.position.y < maxY && player.transform.position.y > minY)
        {
            toY = player.transform.position.y;
        }
    }
    public void LevelZoom(int i)
    {
        switch(i)
        {
            case 0:
                if(hasStarted)
                {
                    cameraState = 2;
                }
                if(!hasStarted)
                {
                    // player.GetComponent<PlayerControls2d>().canLaunch = true;
                    tapScreen.active = true;
                    cameraState = 0;
                }
                break;
            case 1:
                if(!hasStarted)
                {
                    //  player.GetComponent<PlayerControls2d>().canLaunch = false;
                    tapScreen.active = false;
                }   
                cameraState = 1;
                break;
        }
    }

    void MoveToStates(int i)
    {
        switch(i)
        {
            case 0:
                transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, -10f), new Vector3(playerPos.x, playerPos.y + 2f, -10f), 4f * Time.deltaTime);
                camRange = Mathf.Lerp(camRange, camMinRange, 4f * Time.deltaTime);
                break;
            case 1:
                transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, -10f), midPos, 4f * Time.deltaTime);
                camRange = Mathf.Lerp(camRange, camMaxRange, 4f * Time.deltaTime);
                break;
            case 2:
                transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, -10f), new Vector3(toX, toY + 2f, -10f), 2f * Time.deltaTime);
                camRange = Mathf.Lerp(camRange, camMinRange, 4f * Time.deltaTime);
                break;
        }
        GetComponent<Camera>().orthographicSize = camRange;
    }
}
