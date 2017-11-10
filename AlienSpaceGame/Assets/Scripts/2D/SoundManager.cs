using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    private AudioSource audioSource;
    private AudioClip[] audioClips; //Star Collect Sound


	void Awake ()
    {
        //HAKEE KOMPONENTIT
        audioSource = GameObject.Find("ScriptBlock").GetComponent <AudioSource>();
	}
	
    
    public void Play(int i)
    {
        //SOITTAA MÄÄRITETYN ÄÄNEN
        audioSource.PlayOneShot(audioClips[i]);
    }
}
