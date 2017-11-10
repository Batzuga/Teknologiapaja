using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFunctions : MonoBehaviour {

    private int totalStars;
    private int batteryFound;

    public GameObject[] lamps;
    public Color[] lightColor;

    public GameObject screen;
    public Texture screenTexture;

    private float gasYPos;
    private float gasYDist;
    public GameObject starGas;

    private int stars;
    private int maxStars;

    public GameObject starObject;
    public Material[] starLit;
    void Start ()
    {
        batteryFound = PlayerPrefs.GetInt("BatteryCollected");
        stars = PlayerPrefs.GetInt("Stars");
        maxStars = 21;
        gasYDist = 6.6f;
        SetLightsAndScreen();
    }

    void SetLightsAndScreen()
    {
        if(batteryFound == 1)
        {
            lamps[0].GetComponent<Light>().color = lightColor[1];
            lamps[1].GetComponent<Light>().color = lightColor[1];
            lamps[2].GetComponent<Light>().color = lightColor[1];

            screen.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", screenTexture);
        }

        if(batteryFound == 0)
        {
            lamps[0].GetComponent<Light>().color = lightColor[0];
            lamps[1].GetComponent<Light>().color = lightColor[0];
            lamps[2].GetComponent<Light>().color = lightColor[0];
        }

        gasYPos = -3f + (gasYDist / (maxStars * 1.0f) * (stars * 1.0f));
        Debug.Log(gasYPos);
        starGas.transform.position = new Vector3(starGas.transform.position.x, gasYPos, starGas.transform.position.z);

        if(stars == maxStars)
        {
            starObject.GetComponent<MeshRenderer>().material = starLit[1];
        }
        else {
            starObject.GetComponent<MeshRenderer>().material = starLit[0];

        }
    }

    

    void Update ()
    {
		
	}
}
