using UnityEngine;
using System.Collections;

public class MobileControls : MonoBehaviour {
	public GameObject leftButton;
	public GameObject rightButton;

	// Use this for initialization
	void Start () {
		#if (!UNITY_IOS || !UNITY_ANDROID)
		leftButton.SetActive(false);
		rightButton.SetActive (false);
		#endif
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Action() {
		BoardManager.instance.ActionFunction ();
	}
}
