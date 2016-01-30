using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputController : MonoBehaviour {

	private Touch currentTouch;
	public BallController ballController;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
//#if (UNITY_IOS || UNITY_ANDROID) && !UNITY_EDITOR
		foreach (Touch touch in Input.touches) {
			if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled) {
				if(!currentTouch) {
					touch = currentTouch;
					CaculateTouch();
				}
			}
			else {
				if(touch == currentTouch && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)) {
					currentTouch = null;
				}
			}
		}
//#else
		if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) {
			Right();
		}
		if (Input.GetKeyUp (KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) {
			Left();		
		}
	}

	void CaculateTouch(Touch touch) {
		float mid = Screen.width * 0.5f;
		if (touch.position.x < mid) {
			Left ();
		}
		else {
			Right ();
		}
	}


	void Right() {

	}

	void Left () {

	}
}
