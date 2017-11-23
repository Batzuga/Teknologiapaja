using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Interact : MonoBehaviour {

    public int interaction;
    private LevelManager lmanager;
    private PlayerControls3d pc3d;
    private Puzzle1Script p1s;


	void Start ()
    {
        lmanager = GameObject.Find("ScriptBlock").GetComponent<LevelManager>();
        interaction = 0;
        pc3d = GameObject.Find("Player3D").GetComponent<PlayerControls3d>();
        try
        {
            p1s = GameObject.Find("Puzzle1").GetComponent<Puzzle1Script>();
        }
        catch { }


    }

    public void InteractFunction()
    {
        switch(interaction)
        {
            case 0:
                lmanager.CallWaitTimes("SpaceShip");
                pc3d.toShip = true;
                break;
            case 1:
                lmanager.CallWaitTimes("Planet");
                break;
            case 2:
                lmanager.CallWaitTimes("SpaceMenu");
                break;
            case 3:
                p1s.AddIcon(0);
                break;
            case 4:
                p1s.AddIcon(1);
                break;
            case 5:
                p1s.AddIcon(2);
                break;
            case 6:
                lmanager.CallWaitTimes("Planet");
                //change spawn point in level
                break;
            case 7:
                lmanager.CallWaitTimes("Planet2");
                //change spawn point in level
                break;
        }
    }
}
