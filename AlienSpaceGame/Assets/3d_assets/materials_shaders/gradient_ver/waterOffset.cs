using UnityEngine;
using System.Collections;

public class waterOffset : MonoBehaviour {
	public float scrollSpeed = 0.05F;
	public Renderer rend;
	void Start() {
		rend = GetComponent<Renderer>();
	}
	void Update() {
		float offset = Time.time * scrollSpeed/4;
		rend.material.SetTextureOffset("_MainTex", new Vector2(-offset, 0));
	}
}