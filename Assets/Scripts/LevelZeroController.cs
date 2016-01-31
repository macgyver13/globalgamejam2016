using UnityEngine;
using System.Collections;

public class LevelZeroController : MonoBehaviour {

	public Animation mainCamera;
	public CameraController cameraController;
	private bool cameraIntro = false;
	private bool waiting = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (waiting) {
			if (!cameraController.moving) {
				cameraController.enabled = false;	
				mainCamera.Play ();
				cameraIntro = true;
				waiting = false;
			}
		}
		if (cameraIntro) {
			if (!mainCamera.isPlaying) {
				cameraIntro = false;
			}
		}
	}

	void OnTriggerEnter2D (Collider2D collider) {
		print ("TRIGGER ENTER");
		waiting = true;
	}
}
