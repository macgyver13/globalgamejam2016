﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

    public Image FadeImg;

    public string[] level;

	public int score;
	public float totalTime;

    int levelIndex = 0;

    float fadeTime = 0f;
    public bool sceneStarting = true;

    bool fadeToBlack = false;
    bool fadeToClear = false;

    AudioSource audioSource;

    //Awake is always called before any Start functions
    void Awake()
	{
        //Check if instance already exists
        if (instance == null)

			//if not, set instance to this
			instance = this;

		//If instance already exists and it's not this:
		else if (instance != this)

			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy(gameObject);    

		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);

        //Get a component reference to the attached BoardManager script
        //boardScript = GetComponent<BoardManager>();

        //Call the InitGame function to initialize the first level 
        LoadLevel();
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    IEnumerator startLoadLevel()
    {
        yield return null;
        LoadLevel();
    }

    void Update()
    {
        if (fadeToBlack)
        {
            fadeTime += Time.deltaTime * 2.0f;
            Debug.Log("test: " + fadeTime);
            FadeImg.color = Color.Lerp(Color.clear, Color.black, fadeTime);
            if (fadeTime >= 1f)
            {
                fadeToBlack = false;
                fadeTime = 0f;
            }
        }
        if (fadeToClear)
        {
            fadeTime += Time.deltaTime * 2.0f;
            FadeImg.color = Color.Lerp(Color.black, Color.clear, fadeTime);
            if (fadeTime >= 1f)
            {
                fadeToClear = false;
                fadeTime = 0f;
            }
        }

		if (Input.GetKey (KeyCode.LeftShift) && Input.GetKey (KeyCode.T)) {
			levelIndex++;
			LoadLevel ();
		}
    }

    public void FadeOut()
    {

    }

    public void FadeIn()
    {

    }

	void LoadLevel()
	{
        if (level.Length == 0)
            return;
        if (levelIndex > level.Length - 1)
            levelIndex = 0;
        //Set level, setup scene
        Debug.Log("Loading level: " + level[levelIndex]);
        SceneManager.LoadScene(level[levelIndex]);

	}

	void AddCollectible(int value){
		score += value;
	}

	int GetScore(){
		
		return score;
	}

	public void ResetLevel(){
		Debug.Log ("You died");
        StartCoroutine(ResetLevelAfterDelay());
    }

	public void EndLevel(){
		Debug.Log ("Level is over");
        StartCoroutine(EndLevelAfterDelay());
    }

    IEnumerator ResetLevelAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        fadeToBlack = true;
        yield return new WaitForSeconds(.5f);
        Application.LoadLevel(Application.loadedLevel);
        fadeToClear = true;
    }

    IEnumerator EndLevelAfterDelay()
    {
        yield return new WaitForSeconds(.5f);
        fadeToBlack = true;
        yield return new WaitForSeconds(.5f);
        levelIndex++;
        LoadLevel();
        fadeToClear = true;
    }

    public void ChangeGravity()
    {
        audioSource.pitch = Random.Range(.9f, 1.1f);
    }
}
