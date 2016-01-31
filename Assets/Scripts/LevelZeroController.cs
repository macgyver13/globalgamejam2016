using UnityEngine;
using System.Collections;

public class LevelZeroController : MonoBehaviour {
	public GameObject fallingCamera;
	public GameObject gameCamera;
	public Animation mainCamera;
	public CameraController cameraController;
	public ParallaxController parallaxController;
	private bool cameraIntro = false;
	private bool waiting = false;
	private bool called = false;

	// Use this for initialization
	void Start () {
		BallController.instance.CanControl (false);
		BallController.instance.isOnGround = false;
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
				print ("camerIntro finished");
				cameraIntro = false;
				BallController.instance.CanControl (true);
				parallaxController.enabled = true;
				fallingCamera.SetActive(false);
				gameCamera.SetActive (true);
			}
		}
	}

	void OnTriggerEnter2D (Collider2D collider) {
		if (!called) {
			waiting = true;
			called = true;
		}
	}
}
