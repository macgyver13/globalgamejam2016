using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputController : MonoBehaviour {

	public int currentTouch;

	BallController ballController;

	// Use this for initialization
	void Start () {
        ballController = GetComponent<BallController>();
        currentTouch = -1;
	}
	
	// Update is called once per frame
	void Update () {
#if (UNITY_IOS || UNITY_ANDROID) && !UNITY_EDITOR
//		foreach (Touch touch in Input.touches) {
//			print ("TOUCH");
//			if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled) {
//				if(currentTouch == touch.fingerId) {
//					touch = currentTouch;
//					CaculateTouch();
//				}
//			}
//			else {
//				if(touch == currentTouch && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)) {
//					currentTouch = null;
//				}
//			}
//		}
#else
		if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
			Right();
		}
		if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
			Left();		
		}
#endif
	}

	void CaculateTouch(Touch touch) {
		float mid = Screen.width * 0.5f;
		if (touch.position.x < mid) {
			print("LEFT");
			Left ();
		}
		else {
			print("RIGHT");
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
