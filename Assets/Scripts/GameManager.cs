using UnityEngine;
using System.Collections;
using Time;

public class GameManager : MonoBehaviour {

	private static GameManager instance = null;

	public int score;
	public var totalTime;

	public static GameManager GetInstance()
	{
		if(instance == null)
		{
			instance = new GameManager();
		}

		return instance;
	}

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
		InitGame();
	}

	void InitGame()
	{
		//Set level, setup scene
		score = 0;
	}

	
	// Update is called once per frame
	void Update () {
		totalTime += Time.deltaTime;
	}

	void addCollectible(int value){
		score += value;
	}

	int getScore(){
		
		return score;
	}
}
