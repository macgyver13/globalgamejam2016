using UnityEngine;
using System.Collections;

public class ParallaxController : MonoBehaviour {

	public Transform _transform;
	public Transform ballTransform;
	public Vector3 previousBallPosition;
	public float scale;
	public float buffer;

	public float smoothTime = 0.1f;    // time for dampen
	public Vector2 velocity; // speed of camera movement

	
	// Update is called once per frame
	void Update () {
		float topX = previousBallPosition.x + buffer;
		float bottomX = previousBallPosition.x - buffer;
		float topY = previousBallPosition.y + buffer;
		float bottomY = previousBallPosition.y - buffer;

		if ((ballTransform.position.y > topY) || (ballTransform.position.y < bottomY) || 
			(ballTransform.position.x > topX) || (ballTransform.position.x < bottomX))  {
			Vector3 newPosition = CalculateBackgroundPosition ();
			print (newPosition);

			transform.position = new Vector3 (Mathf.SmoothDamp (_transform.position.x, newPosition.x, ref velocity.x, smoothTime), Mathf.SmoothDamp (_transform.position.y, newPosition.y, ref velocity.y, smoothTime), _transform.position.z);
		}
	}

	private Vector3 CalculateBackgroundPosition() {
		float yDiff = previousBallPosition.y - ballTransform.position.y;
		float xDiff = previousBallPosition.x - ballTransform.position.x;

		float yPos = yDiff * scale;
		float xPos = xDiff * scale;

		previousBallPosition = ballTransform.position;

		return new Vector3 (xPos, yPos, _transform.position.z);
	}
}
