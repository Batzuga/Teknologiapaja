using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

    public Image energyBar;
    private float energy;
    private float maxEnergy;

    public Button jumpButton;
    private int canJump;

    public Button interactButton;
    private bool interactVisible;

    private PlayerControls3d pc3d;
    private Interact interact;

	
	void Start ()
    {
        maxEnergy = 100f;
        pc3d = GameObject.Find("Player3D").GetComponent<PlayerControls3d>();
        canJump = PlayerPrefs.GetInt("JumpUnlocked");
        canJump = 1; //DEBUG VALUE
        interact = interactButton.gameObject.GetComponent<Interact>();
        if(canJump == 1)
        {
            jumpButton.interactable = true;
        }
	}

    public void SetInteract(int i)
    {
        interactButton.interactable = true;
        interact.interaction = i;
    }

    public void HideInteract()
    {
        interactButton.interactable = false;
    }

    public void GetEnergy(float f)
    {
        energy = f;
        energyBar.fillAmount = energy / maxEnergy;
    }

    public void DebugMenu()
    {
        SceneManager.LoadScene("SpaceMenu");   
    }
}
