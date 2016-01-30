using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputController : MonoBehaviour {
	
	public int currentTouch;
	
	public BallController ballController;
	
	private enum Control {
		Right,
		Left,
		None
	};
	private Control currentControl;
	
	// Use this for initialization
	void Start () {
		currentTouch = -1;
	}
	
	// Update is called once per frame
	void Update () {
		#if (UNITY_IOS || UNITY_ANDROID) && !UNITY_EDITOR
		foreach (Touch touch in Input.touches) {
			if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled) {
				if(currentControl == Control.None) {
					currentTouch = touch.fingerId;
					CaculateTouch(touch);
				}
			}
			else {
				if(touch.fingerId == currentTouch && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)) {
					currentControl = Control.None;
				}
			}
		}
		#else
		if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D)) {
			Right ();
		} else if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)) {
			Left ();		
		} else {
			currentControl = Control.None;
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
		currentControl = Control.Right;
	}
	
	void Left () {
		ballController.Left ();
		currentControl = Control.Left;
	}
}
