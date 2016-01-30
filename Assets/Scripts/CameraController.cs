using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Transform _transform;
	public Transform ball;
	public Camera mainCamera;

	public float smoothTime = 0.1f;    // time for dampen
	public float cameraHeight = 2.5f; // height of camera adjustable
	public Vector2 velocity; // speed of camera movement
	private float currentYPosition;

	// Use this for initialization
	void Start () {
		currentYPosition = _transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		currentYPosition = _transform.position.y;
		Vector3 potentialNewPoint = new Vector3(Mathf.SmoothDamp(_transform.position.x, ball.position.x, ref velocity.x, smoothTime), 
		                                        Mathf.SmoothDamp(_transform.position.y, ball.position.y, ref velocity.y, smoothTime), 
		                                        _transform.position.z);

		float potentialNewY = potentialNewPoint.y;
		float topThresh = TopThreshHold ();
		float bottomThresh = BottomThreshHold ();
		float newYPosition = currentYPosition;

		if (ball.position.y > topThresh) {
			newYPosition = potentialNewY;
			PerlinShake.instance.CommonShake();
		}
		if (ball.position.y < bottomThresh) {
			newYPosition = potentialNewY;
			PerlinShake.instance.CommonShake();
		}

		transform.position = new Vector3(Mathf.SmoothDamp(_transform.position.x, ball.position.x, ref velocity.x, smoothTime), newYPosition, _transform.position.z);
	}

	float TopThreshHold () {
		float fourth = Screen.height * 0.25f;
		float thresh = Screen.height - fourth;
		Vector3 screenPos = new Vector3 (0, thresh, 0);
		Vector3 worldThresh = mainCamera.ScreenToWorldPoint (screenPos);

		return worldThresh.y;
	}

	float BottomThreshHold () {
		float fourth = Screen.height * 0.25f;
		Vector3 screenPos = new Vector3 (0, fourth, 0);
		Vector3 worldThresh = mainCamera.ScreenToWorldPoint (screenPos);

		return worldThresh.y;
	}
}
