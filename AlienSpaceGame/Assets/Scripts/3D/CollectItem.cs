using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour {

    public int itemID;
    public string[] itemNames;
	// Use this for initialization
    
    void Start()
    {
        itemNames = new string[8];
        itemNames[0] = "BatteryCollected";
        itemNames[1] = "WiringCollected";
        itemNames[2] = "JumpPack";
        itemNames[3] = "MovementUpgrade";
        itemNames[4] = "JumpUpgrade";
        itemNames[5] = "BatteryUpgrade";
        itemNames[6] = "SolarPanel";
        itemNames[7] = "MapChip";

    }
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            PlayerPrefs.SetInt(itemNames[itemID], 1);
            //effect
        }
    }
}
