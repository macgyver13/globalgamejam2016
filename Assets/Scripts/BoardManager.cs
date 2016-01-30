using UnityEngine;
using System;
using System.Collections.Generic;

public enum ballModifier{
	bounceNone, bounceShort, bounceNormal, bounceHigh,
	sizeSmall, sizeNormal, sizeLarge,
	speedSlow, speedNormal, speedFast,
	gravityLeft, gravityUp, gravityRight, gravityDown

};

public class BoardManager : MonoBehaviour {

	public int modifierDuration = 5;
	public int[] modifierList;
	public int modifierPosition = 0;
	public float totalTime;

	//Clears our list gridPositions and prepares it to generate a new board.
	void InitialiseList ()
	{
		
	}

	void Awake(){
		totalTime = 0;
	}

	// Update is called once per frame
	void Update () {
		totalTime += Time.deltaTime;

		Debug.Log("Update Time = " + totalTime + " ____ " + modifierPosition);
		if (totalTime > modifierDuration * (modifierPosition + 1)) {
			//send the calculated position
			modifierPosition += 1;
		}
		if (totalTime >= modifierDuration * modifierList.Length) {
			//End level
			Application.LoadLevel (Application.loadedLevel);
		}
	}


	void BoardSetup ()
	{
		//Instantiate Board and set boardHolder to its transform.
		//boardHolder = new GameObject ("Board").transform;


	}

	//SetupScene initializes our level and calls the previous functions to lay out the game board
	public void SetupScene (int level)
	{
		//Creates the outer walls and floor.
		BoardSetup ();

		//Reset our list of gridpositions.
		InitialiseList ();


	}
}
