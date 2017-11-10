using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class SpaceMenu : MonoBehaviour {

    public string lvlSelection;
    public Image[] stars;
    public Color c1;
    public Color c2;

    private LevelManager lmanager;

	// Use this for initialization
	void Start ()
    {
        lmanager = GameObject.Find("ScriptBlock").GetComponent<LevelManager>();
        Screen.orientation = ScreenOrientation.Landscape;
        for(int i = 0; i < stars.Length; i++)
        {
            stars[i].color = c1;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    public void SetStars()
    {
        for(int i = 0; i < stars.Length; i++)
        {
            stars[i].color = c1;
        }
        GameObject.Find(lvlSelection).GetComponent<Image>().color = c2;
    }
    public void SelectLevel(string s)
    {
        lvlSelection = s;
        SetStars();
    }
    public void AbortMission(string s)
    {
        lvlSelection = s;
        EnterLevel();
    }
    public void EnterLevel()
    {
        lmanager.CallWaitTimes(lvlSelection);
    }
}
