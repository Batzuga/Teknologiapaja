using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour {

    public Color[] color;
    private int i;
    public Image img;
	// Use this for initialization
	void Start ()
    {
        //ALOITTAA AJASTIMEN
        StartCoroutine(SplashScreenTimer());	
        
	}
    void Update()
    {
        switch(i)
        {
            case 1:
                img.color = Color.Lerp(img.color, color[1], 6f * Time.deltaTime);
                break;
            case 2:
                img.color = Color.Lerp(img.color, color[0], 6f * Time.deltaTime);
                break;
        }
    }
    void FirstTimeSetup()
    {
        
        if(PlayerPrefs.GetInt("FirstTime") == 0)
        {
            //ASETTAA ARVOT ENSIMMÄISTÄ PELIKERTAA VARTEN
            PlayerPrefs.SetInt("Sounds", 1);
            PlayerPrefs.SetInt("Music", 1);
            PlayerPrefs.SetInt("FirstTime", 1);
        }
    }
    IEnumerator SplashScreenTimer()
    {
        i = 1;
        yield return new WaitForSeconds(1.5f);
        i = 2;
        yield return new WaitForSeconds(2.0f);
        FirstTimeSetup();
        //LATAA ALKUVALIKON
        SceneManager.LoadScene(1);
    }
}
