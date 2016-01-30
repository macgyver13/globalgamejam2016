using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

    public float[] force;

    public float movementForce;

    public float maxVelocity;

    int count = 0;
    Rigidbody2D rigidbody;

    // Use this for initialization
    void Start () {
        rigidbody = transform.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rigidbody.AddForce(new Vector2(1,0) * movementForce);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rigidbody.AddForce(new Vector2(-1, 0) * movementForce);
        }

        rigidbody.velocity = Vector2.ClampMagnitude(rigidbody.velocity, maxVelocity);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Floor")
        {
            rigidbody = transform.GetComponent<Rigidbody2D>();
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
            rigidbody.AddForce(Vector2.up * force[count]);
            count++;
            if (count >= force.Length)
                count = 0;
        }
    }
}
