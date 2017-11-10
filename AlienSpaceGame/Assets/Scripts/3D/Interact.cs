using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Interact : MonoBehaviour {

    public int interaction;
    private LevelManager lmanager;
    private PlayerControls3d pc3d;

	void Start ()
    {
        lmanager = GameObject.Find("ScriptBlock").GetComponent<LevelManager>();
        interaction = 0;
        pc3d = GameObject.Find("Player3D").GetComponent<PlayerControls3d>();
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
        }
    }
}
