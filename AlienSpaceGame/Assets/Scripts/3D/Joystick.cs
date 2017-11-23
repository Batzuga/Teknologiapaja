using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class Joystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{

    public Image joyStick;
    public Image knob;
    public GameObject knobSpace;
    public Vector3 InputDirection
    {
        get; set;
    }
    public float movementSpeed;
    private GameObject player;
    public GameObject playerBody;
    private Rigidbody rb;
    private PlayerControls3d pc3d;
    private bool canMove;
    private float angle;
    public GameObject rotater;
    private bool grounded;
    private float aerial;

	void Start ()
    {
        player = GameObject.Find("Player3D");
        playerBody = GameObject.Find("PlayerBody");
        rb = player.GetComponent<Rigidbody>();
        pc3d = player.GetComponent<PlayerControls3d>();
        InputDirection = Vector3.zero;
        rotater = GameObject.Find("Rotater" );
	}
	
	void Update ()
    {
        MovePlayer();
        grounded = pc3d.grounded;
        if(grounded)
        {
            aerial = 1f;
        }
        if(!grounded && aerial > 0)
        {
            aerial -= 0.5f * Time.deltaTime;
        }

	}

    void MovePlayer()
    {
        GetStats();
        if(InputDirection != Vector3.zero && canMove)
        {
            Vector3 d = rotater.transform.position - player.transform.position;
            d = new Vector3(d.x, d.y, d.z);
            Quaternion rot = Quaternion.LookRotation(d);
            playerBody.transform.localRotation = rot;
            rb.velocity = new Vector3(InputDirection.x * movementSpeed, rb.velocity.y, InputDirection.z * movementSpeed);
            pc3d.moving = true;
            playerBody.GetComponent<Animator>().SetBool("isMoving", true);
            
        }
        if(InputDirection == Vector3.zero || !canMove)
        {
            rb.velocity = new Vector3(0,rb.velocity.y,0);
            pc3d.moving = false;
            playerBody.GetComponent<Animator>().SetBool("isMoving", false);

        }

    }

    void GetStats()
    {
        movementSpeed = pc3d.movementSpeed * aerial;
        canMove = pc3d.canMove;
    }
    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos = Vector2.zero;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(joyStick.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / joyStick.rectTransform.sizeDelta.x);
            pos.y = (pos.y / joyStick.rectTransform.sizeDelta.y);

            float x = (joyStick.rectTransform.pivot.x == 1) ? pos.x * 2 + 1 : pos.x * 2 - 1;
            float y = (joyStick.rectTransform.pivot.y == 1) ? pos.y * 2 + 1 : pos.y * 2 - 1;

            InputDirection = new Vector3(x, 0, y);

            InputDirection = (InputDirection.magnitude > 1) ? InputDirection.normalized : InputDirection;

            knob.rectTransform.anchoredPosition = new Vector3(InputDirection.x * (joyStick.rectTransform.sizeDelta.x / 3), InputDirection.z * (joyStick.rectTransform.sizeDelta.y / 3));

            rotater.transform.localPosition = new Vector3(InputDirection.x, 0, InputDirection.z);
        }
    }
    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }
    public virtual void OnPointerUp(PointerEventData ped)
    {
        InputDirection = Vector3.zero;
        knob.rectTransform.anchoredPosition = Vector3.zero;
        
        
    }
}
