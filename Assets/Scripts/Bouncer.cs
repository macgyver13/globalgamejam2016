using UnityEngine;
using System.Collections;

public class Bouncer : MonoBehaviour {
	public Animator bounceAnimator;
	public bool bounced = false;
    public Transform trajectoryObjectNorth;
    public Transform trajectoryObjectSouth;
    public Transform trajectoryObjectEast;
    public Transform trajectoryObjectWest;

    public float force;
    private Vector2 velocityToUse;


    void ApplyBounce(GameObject otherObject, Collision2D coll)
    {
        Rigidbody2D rgbd2D = otherObject.GetComponent<Rigidbody2D>();

        rgbd2D.velocity = Vector2.zero;
        Transform trajectory;
        if (BallController.instance.gravityVector.x > 0 && trajectoryObjectEast != null)
            trajectory = trajectoryObjectEast;
        else if (BallController.instance.gravityVector.x < 0 && trajectoryObjectWest != null)
            trajectory = trajectoryObjectWest;
        else if (BallController.instance.gravityVector.y > 0 && trajectoryObjectNorth != null)
            trajectory = trajectoryObjectNorth;
        else
            trajectory = trajectoryObjectSouth;

		bounceAnimator.SetTrigger("Hit");

        rgbd2D.AddForce((trajectory.position - transform.position) * force, ForceMode2D.Impulse);

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        GameObject otherObject = coll.gameObject;
        if (otherObject.tag == "Ball")
            ApplyBounce(otherObject, coll);
    }
}
