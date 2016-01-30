using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {
    public AudioSource audioSource;

    public AudioClip[] bounceSounds;

    public float[] force;

    public float movementForce;

    public float maxVelocity;

    int count = 0;
    Rigidbody2D rigidbody;

    // Use this for initialization
    void Start () {
        rigidbody = transform.GetComponent<Rigidbody2D>();
    }
	void Update() {
		rigidbody.velocity = Vector2.ClampMagnitude(rigidbody.velocity, maxVelocity);
	}

    void PlayBounceSound()
    {
        int index = Random.Range(0, bounceSounds.Length);
        audioSource.clip = bounceSounds[index];
        audioSource.Play();
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Floor")
        {
            PlayBounceSound();
            rigidbody = transform.GetComponent<Rigidbody2D>();
            if (rigidbody.velocity.y < 0)
            {
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
                rigidbody.AddForce(Vector2.up * force[count]);
                count++;
                if (count >= force.Length)
                    count = 0;
            }
        } else if (coll.gameObject.tag == "Border") {
			//End scene
			Application.LoadLevel (Application.loadedLevel);
		}
    }

	public void Right() {
		rigidbody.AddForce(new Vector2(1,0) * movementForce);
	}
	public void Left() {
		rigidbody.AddForce(new Vector2(-1, 0) * movementForce);
	}

}
