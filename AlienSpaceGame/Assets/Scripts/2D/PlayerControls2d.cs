using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControls2d : MonoBehaviour {

    private float speed;
    private float maxSpeed;
    private float minSpeed;
    private float rotationSpeed;
    private float baserotSpeed;
    private float extrarotSpeed;

    public bool shipLaunched;
    public bool canLaunch;

    private float timer;

    private Vector2 playerStartPos;
    private Vector2 boostStartPos;
    private Vector2 boostEndPos;
    private float boostDistance;

    private StarManager starManager;
    private SoundManager soundManager;
    private LevelManager lvlManager;
    private DeathManager dthManager;
    private GameOverManager goManager;
    private Rigidbody2D rb2d;
    private CameraFollow2d cf2d;

    private GameObject motorFire;
   
 

    void Awake()
    {
        //HAKEE KOMPONENTIT
        rb2d = GetComponent<Rigidbody2D>();
        cf2d = GameObject.Find("Main Camera").GetComponent<CameraFollow2d>();
        dthManager = GameObject.Find("ScriptBlock").GetComponent<DeathManager>();
        goManager = GameObject.Find("ScriptBlock").GetComponent<GameOverManager>();
        starManager = GameObject.Find("ScriptBlock").GetComponent<StarManager>();
        soundManager = GameObject.Find("ScriptBlock").GetComponent<SoundManager>();
        lvlManager = GameObject.Find("ScriptBlock").GetComponent<LevelManager>();
        motorFire = this.gameObject.transform.GetChild(0).gameObject;
        
    }
    void Start ()
    {
        //ANTAA ALKUARVOT
        shipLaunched = false;
        canLaunch = true;

        maxSpeed = 10f;
        minSpeed = 1f;
        baserotSpeed = .75f;
        extrarotSpeed = 0f;
        rotationSpeed = baserotSpeed + extrarotSpeed;
        playerStartPos = transform.position;
    }
	
	void Update ()
    {
        //KUTSUU LIIKUTUS JA KOSKETUS FUNKTIOT
        MoveShip();
        TouchControls();

        //AETTAA MOOTTORIN LIEKIN NOPEUDEN
        SetMotorFire();
        //UNITY EDITORISSA HIIRELLE "KOSKETUS"
#if UNITY_EDITOR
        ClickControls();
        #endif
        

    }
    public void MoveToStart()
    {
        speed = 0;
        SetMotorFire();

        transform.position = playerStartPos;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    void MoveShip()
    {
        //LIIKUTTAA ALUSTA ETEENPÄIN JOS 'shipLaunched == true'
        if(shipLaunched)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
    }

    void ClickControls()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if(hit.collider.tag == "TapField" && canLaunch)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                //OTTAA YLÄS ALKUNAPAUTUSKOHDAN
                boostStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

            if(Input.GetKey(KeyCode.Mouse0))
            {
                //LASKEE KULMAN MIHIN PALLO TÄHTÄÄ
                Vector2 poz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 pozz = new Vector3(poz.x, poz.y, 0);
                Vector3 vTT = pozz - transform.position;
                float angle = Mathf.Atan2(vTT.y, vTT.x) * Mathf.Rad2Deg;
                Quaternion q = Quaternion.AngleAxis(angle + 90, Vector3.forward);
                boostEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 360);
                //LASKEE ETÄISYYDEN
                boostDistance = Vector2.Distance(boostStartPos, boostEndPos);
                //ASETTAA LAUKAISUNOPEUDEN
                SetSpeed();
            }

            if(Input.GetKeyUp(KeyCode.Mouse0))
            {
                shipLaunched = true;
                canLaunch = false;
                cf2d.hasStarted = true;
                cf2d.cameraState = 2;
            }
        }
        else
        {
        }
    }

    void SetMotorFire()
    {
        float y = 0;
        y = 0.1f / maxSpeed * speed;
        motorFire.transform.localPosition = new Vector3(0, -(y + 0.03f), 0.1f);
    }

    void TouchControls()
    {
 
        for(int i = 0; i < Input.touchCount; ++i)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), Vector2.zero);

            if(hit.collider.tag == "TapField" && canLaunch)
            {
                if(Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    //OTTAA YLÖS MISTÄ KOSKETUS ALKOI
                    boostStartPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
                }

                if(Input.GetTouch(i).phase == TouchPhase.Moved || Input.GetTouch(i).phase == TouchPhase.Stationary)
                {
                    //KÄÄNTÄÄ ALUSTA 
                    Vector2 poz = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
                    Vector3 pozz = new Vector3(poz.x, poz.y, 0);
                    Vector3 vTT = pozz - transform.position;
                    float angle = Mathf.Atan2(vTT.y, vTT.x) * Mathf.Rad2Deg;
                    Quaternion q = Quaternion.AngleAxis(angle + 90, Vector3.forward);
                    boostEndPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 360);
                    //LASKEE ETÄISYYDEN
                    boostDistance = Vector2.Distance(boostStartPos, boostEndPos);
                    //ASETTAA NOPEUDEN
                    SetSpeed();
                }

                if(Input.GetTouch(i).phase == TouchPhase.Ended || Input.GetTouch(i).phase == TouchPhase.Canceled)
                {
                    //LAUKAISEE ALUKSEN JA VARMISTAA ETTEI VOIDA AMPUA UUDESTAAN LIIKKUESSA
                    shipLaunched = true;
                    canLaunch = false;
                    cf2d.hasStarted = true;
                    cf2d.cameraState = 2;
                }
            }
            else
            {
            }
        }
    }

    void SetSpeed()
    {
        //LASKEE NOPEUDEN TIETTYJEN RAJOJEN SISÄÄN
        speed = boostDistance / 3 * maxSpeed;
        if(speed > maxSpeed)
        {
            speed = maxSpeed;
        }
        if(speed < minSpeed)
        {
            speed = minSpeed;
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        //TARKISTAA OSUUKO PELAAJA TÄHTEEN
        if(col.gameObject.tag == "Star")
        {
            //TUHOAA TÄHDEN, SOITTAA ÄÄNEN JA LISÄÄ TÄHDEN KERÄTYKSI
            Destroy(col.gameObject);
            soundManager.Play(0);
            starManager.AddStar();
        }
        if(col.gameObject.tag == "Blackhole")
        {
            extrarotSpeed = 0f;
            Debug.Log("ENTERD");
            timer = 0f;
        }
        if(col.gameObject.tag == "Death")
        {
            dthManager.DeathTriggered();
        }
        if(col.gameObject.tag == "Goal")
        {
            speed = 0;
            lvlManager.CallWaitTimes("SpaceMenu");
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        //TARKISTAA ONKO PELAAJA MINKÄÄN PELIELEMENTIN SISÄLLÄ
        if(col.gameObject.tag == "Blackhole")
        {
            //KÄÄNTÄÄ ALUSTA KOHTI MUSTAA AUKKOA
            Vector3 vectorToTarget = col.transform.localPosition - transform.localPosition;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            transform.rotation = Quaternion.Lerp(transform.rotation, q, Time.deltaTime * rotationSpeed);
            //LISÄÄ KÄÄNTYMIS VAUHTIA MUSTAAN AUKKOON
            extrarotSpeed += .05f * Time.deltaTime;
            rotationSpeed = baserotSpeed + extrarotSpeed;
            timer += 1f * Time.deltaTime;
            if(timer > 6)
            {
                dthManager.DeathTriggered();
            }
        }
        if(col.gameObject.tag == "Repeller")
        {
            //KÄÄNTÄÄ ALUSTA POISPÄIN HYLKIJÄSTÄ
            Vector3 vectorToTarget = col.transform.localPosition - transform.localPosition;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(-(angle - 90), Vector3.forward);
            transform.rotation = Quaternion.Lerp(transform.rotation, q, Time.deltaTime * rotationSpeed);
            
        }
    }
}
