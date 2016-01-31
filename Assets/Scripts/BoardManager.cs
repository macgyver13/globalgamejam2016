using UnityEngine;
using System;
using System.Collections.Generic;

public enum ballModifier{
    jump,
	bounceNone, bounceShort, bounceNormal, bounceHigh,
	sizeSmall, sizeNormal, sizeLarge,
	speedSlow, speedNormal, speedFast,
	gravityWest, gravityNorth, gravityEast, gravitySouth

};

public class BoardManager : MonoBehaviour {

	public ballModifier[] modifierList;
	public int modifierPosition = 0;

	//Clears our list gridPositions and prepares it to generate a new board.
	void InitialiseList ()
	{
		
	}

	void Start(){
		ResetBoard ();
		//SetModifier ();


	}

	void ResetBoard(){
//		totalTime = 0;
		modifierPosition = 0;
        if (GUIController.instance != null)
        {
            GUIController.instance.modifierList = modifierList;
            GUIController.instance.SetupIcons();
        }
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space) && BallController.instance.IsOnGround())
        {
            if (modifierPosition < modifierList.Length) {
				Debug.Log ("Set Modifier" + modifierList [modifierPosition]);
				SetModifier ();

				modifierPosition += 1;
                if(GUIController.instance != null)
                    GUIController.instance.SetModifierPosition(modifierPosition);
            } else {
				GameManager.instance.ResetLevel ();
			}
        }
	}


	void BoardSetup ()
	{
		//Instantiate Board and set boardHolder to its transform.
		//boardHolder = new GameObject ("Board").transform;


	}

	void SetModifier(){
		switch (modifierList [modifierPosition]) {
            case ballModifier.jump:
                 BallController.instance.Jump();
                 break;
            case ballModifier.gravityNorth:
                BallController.instance.NorthGravity();                
                break;
            case ballModifier.gravityEast:
			    BallController.instance.EastGravity ();
                break;
		    case ballModifier.gravityWest:
			    BallController.instance.WestGravity ();
                break;
		    case ballModifier.gravitySouth:
			    BallController.instance.SouthGravity ();
                break;
		    case ballModifier.bounceNone:
			    BallController.instance.NoJump ();
			    break;
		    case ballModifier.bounceShort:
			    BallController.instance.SmallJump ();
			    break;
		    case ballModifier.bounceNormal:
			    BallController.instance.MediumJump ();
			    break;
		    case ballModifier.bounceHigh:
			    BallController.instance.LargeJump ();
			    break;
		    case ballModifier.sizeSmall:
			    BallController.instance.SmallSize ();
			    break;
		    case ballModifier.sizeNormal:
			    BallController.instance.NormalSize ();
			    break;
		    case ballModifier.sizeLarge:
			    BallController.instance.BigSize ();
			    break;
		    case ballModifier.speedSlow:
			    BallController.instance.SlowSpeed ();
			    break;
		    case ballModifier.speedNormal:
			    BallController.instance.NormalSpeed ();
			    break;
		    case ballModifier.speedFast:
			    BallController.instance.FastSpeed ();
			    break;
		    }
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
