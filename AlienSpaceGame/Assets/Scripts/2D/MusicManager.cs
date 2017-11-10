using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    private AudioSource audioSource;
    private int isMusicOn;
    public AudioClip auc;

	void Awake ()
    {   
        //HAKEE KOMPONENTIT JA TALLENNETUT TIEDOT
        audioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        isMusicOn = PlayerPrefs.GetInt("Music");
	}

    void Start()
    {
        //SOITTAA MUSIIKIN JOS MUSIIKKI ON LAITETTU PÄÄLLE
        if(isMusicOn == 1)
        {
            audioSource.clip = auc;
            audioSource.Play();
        }
    }

}
