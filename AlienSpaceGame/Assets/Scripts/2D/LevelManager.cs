using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {     

    public string levelName;
    private Scene scene;
    private Image fadeScreen;
    private float fadeTimer;
    private int inOut;
    public Color[] color;

    void Awake()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        //HAKEE LEVELIN NIMEN
        scene = SceneManager.GetActiveScene();
        levelName = scene.name;
        fadeScreen = GameObject.Find("FadeScreen").GetComponent<Image>();
        fadeScreen.color = color[0];
        inOut = 0;
        StartCoroutine(StartWait());
    }
    void Start()
    {

    }
    void Update()
    {
        FadeScreen();
    }
    IEnumerator StartWait()
    {
        yield return new WaitForSeconds(1.0f);
        inOut = 1;
    }
    void FadeScreen()
    {
        switch (inOut)
        {
            case 1:
                fadeScreen.color = Color.Lerp(fadeScreen.color, color[1], 4f * Time.deltaTime);
                break;
            case 2:
                fadeScreen.color = Color.Lerp(fadeScreen.color, color[0], 4f * Time.deltaTime);
                Debug.Log("Lerping Color to black");
                break;
        }
    }
    public void CallWaitTimes(string s)
    {
        StartCoroutine(WaitTimes(2.0f, 2, s));
    }
    IEnumerator WaitTimes(float f, int i, string s)
    {
        inOut = i;
        yield return new WaitForSeconds(f);
        SceneManager.LoadScene(s);
    }
    public void ReloadLevel()
    {
        //LATAA TASON UUDESTAAN
        SceneManager.LoadScene(scene.name);
    }
    
    public void LoadOther(string s)
    {
        //LATAA HALUTUN KENTÄN f.e MENU tai SEURAAVA KENTTÄ
        WaitTimes(3.0f, 2, s);
        
    }
}
