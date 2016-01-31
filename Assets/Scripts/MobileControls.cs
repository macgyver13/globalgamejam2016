using UnityEngine;
using System.Collections;

public class MobileControls : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Action() {
		BoardManager.instance.ActionFunction ();
	}
}
