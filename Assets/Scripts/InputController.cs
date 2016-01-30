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
#if (UNITY_IOS || UNITY_ANDROID) && !UNITY_EDITOR
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
#else
		if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
			print("RIGHT");
			Right();
		}
		if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
			print("LEFT");
			Left();		
		}
#endif
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
		ballController.Right ();
	}

	void Left () {
		ballController.Left ();
	}
}
