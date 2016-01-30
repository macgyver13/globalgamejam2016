using UnityEngine;
using System.Collections;

public class Bouncer : MonoBehaviour {

    public Transform trajectoryObject;
    public float force;
    private Vector2 velocityToUse;

    void ApplyBounce(GameObject otherObject, Collision2D coll)
    {
        Rigidbody2D rgbd2D = otherObject.GetComponent<Rigidbody2D>();


            rgbd2D.velocity = Vector2.zero;
            rgbd2D.AddForce((trajectoryObject.position - transform.position) * force, ForceMode2D.Impulse);
       
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        GameObject otherObject = coll.gameObject;
        if (otherObject.tag == "Ball")
            ApplyBounce(otherObject, coll);
    }
}
