using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollWater : MonoBehaviour {

    public float scrollSpeed;
    public Renderer rend;

	// Use this for initialization
	void Start () {
        scrollSpeed = 0.05f;
        rend = GetComponent<Renderer>();   
	}
	
	// Update is called once per frame
	void Update ()
    {
        float offset = Time.time * scrollSpeed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(offset, offset));
	}
}
