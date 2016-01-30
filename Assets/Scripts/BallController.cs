using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {

    public float force;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Floor")
        {
            Rigidbody2D rigidbody = transform.GetComponent<Rigidbody2D>();
            transform.GetComponent<Rigidbody2D>().AddForce(Vector2.up * force);
        }
    }
}
