using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {
    public AudioSource audioSource;

    public AudioClip[] bounceSounds;

    public float force;

    public float movementForce;

    public float maxVelocity;


    public float smallJumpForce;
    public float mediumJumpForce;
    public float largeJumpForce;


    int count = 0;
    Rigidbody2D rigidbody;

    // Use this for initialization
    void Start () {
        rigidbody = transform.GetComponent<Rigidbody2D>();
    }
	void Update() {
		Vector2 newVelocitiy = Vector2.ClampMagnitude(rigidbody.velocity, maxVelocity);
        rigidbody.velocity = new Vector2(newVelocitiy.x, rigidbody.velocity.y);
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
            rigidbody.AddForce(Vector2.up * force);
        } else if (coll.gameObject.tag == "Border") {
			//End scene
			Application.LoadLevel (Application.loadedLevel);
		}
    }

	public void Right()
    {
		rigidbody.AddForce(new Vector2(1,0) * movementForce);
	}
	public void Left()
    {
		rigidbody.AddForce(new Vector2(-1, 0) * movementForce);
	}

    public void BigSize()
    {
        transform.localScale.Set(2f,2f,2f);
    }

    public void SmallSize()
    {
        transform.localScale.Set(.5f, .5f, .5f);
    }

    public void NoJump()
    {
        force = 0f;
    }

    public void SmallJump()
    {
        force = smallJumpForce;
    }

    public void MediumJump()
    {
        force = mediumJumpForce;
    }

    public void LargeJump()
    {
        force = largeJumpForce;
    }

    public void NorthGravity()
    {

    }
}
