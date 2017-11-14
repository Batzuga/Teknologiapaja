using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class PlayerControls3d : MonoBehaviour{

    //JUMP VARIABLES
    private bool canJump;
    private bool jumpUnlocked;
    private float jumpPower;
    private float airControl;
    public bool grounded;

    //MOVEMENT VARIABLES
    public float movementSpeed;
    private float baseSpeed;
    public bool moving;
    public bool canMove;

    //ENERGY
    private float energy;
    private float maxEnergy;
    private float batteryGrade;
    
    //COMPONENTS
    private Rigidbody rb;
    private PlayerUI pui;

    //OTHER
    private string levelName;
    public Transform spawnPoint;
    public bool toShip;

    //PARTS COLLECTED
    private int batteryCollected;

    void Awake()
    {
        //GETS COMPONENTS
        rb = GetComponent<Rigidbody>();
        pui = GameObject.Find("Canvas").GetComponent<PlayerUI>();
        transform.position = spawnPoint.position;
    }

    void Start ()
    {
        Screen.orientation = ScreenOrientation.Landscape;
        grounded = true;
        toShip = false;
        GetCollected();
        //SETS START VALUES
        canJump = true;
        canMove = true;
        jumpUnlocked = true; //DEBUG VALUE
        jumpPower = 350f;
        airControl = 1f;
        baseSpeed = 5f;
        maxEnergy = 100f;
        energy = 100f;
        pui.GetEnergy(energy);
        levelName = SceneManager.GetActiveScene().name;

	}
	void GetCollected()
    {
        batteryCollected = PlayerPrefs.GetInt("BatteryCollected");
    }
	// Update is called once per frame
	void Update ()
    {
        Movements();
        ToShipEffect();
	}
    public void StartBeaming()
    {
        toShip = true;
    }
    void ToShipEffect()
    {
        if(toShip)
        {
            canJump = false;
            canMove = false;
            rb.velocity = new Vector3(rb.velocity.x, 10f, rb.velocity.z);
        }
    }
    void Movements()
    {
        //SETS MOVEMENT SPEED
        movementSpeed = baseSpeed;
        //REMOVES ENERGY IF MOVING
        if(moving && levelName != "SpaceShip")
        {
            energy -= 1 * Time.deltaTime;
            pui.GetEnergy(energy);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
       
    }


    public void Jump()
    {
        if(canJump && jumpUnlocked)
        {
            canJump = false;
            grounded = false;
            rb.AddForce(Vector3.up * jumpPower);
            airControl = 0.25f;
            energy -= 5;
            pui.GetEnergy(energy);
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Ground")
        {
            canJump = true;
            grounded = true;
            airControl = 1f;
        }
    }

    void OnTriggerStay(Collider col)
    {
        if(col.gameObject.tag == "EnergyStation")
        {
            if(energy < maxEnergy)
            {
                energy += 5f * Time.deltaTime;              
            }
            if(energy > maxEnergy)
            {
                energy = maxEnergy;
            }
            pui.GetEnergy(energy);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "SpacePad")
        {
            pui.SetInteract(0);
        }
        if (col.gameObject.tag == "SpacePad2")
        {
            pui.SetInteract(1);
        }
        if (col.gameObject.tag == "ShipCMD")
        {
            pui.SetInteract(2);
        }
        if (col.gameObject.tag == "P1T1")
        {
            pui.SetInteract(3);
        }
        if (col.gameObject.tag == "P1T2")
        {
            pui.SetInteract(4);
        }
        if (col.gameObject.tag == "P1T3")
        {
            pui.SetInteract(5);
        }
    }
    void OnTriggerExit(Collider col)
    {
        if(col.gameObject.tag == "SpacePad")
        {
            pui.HideInteract();
        }
        if (col.gameObject.tag == "SpacePad2")
        {
            pui.HideInteract();
        }
        if (col.gameObject.tag == "ShipCMD")
        {
            pui.HideInteract();
        }
        if (col.gameObject.tag == "P1T1")
        {
            pui.HideInteract();
        }
        if (col.gameObject.tag == "P1T2")
        {
            pui.HideInteract();
        }
        if (col.gameObject.tag == "P1T3")
        {
            pui.HideInteract();
        }
    }
}
