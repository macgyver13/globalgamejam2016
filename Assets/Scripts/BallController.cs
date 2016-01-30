using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {
    public static BallController instance;

    public AudioSource audioSource;

    public AudioClip[] bounceSounds;

    public float movementForce;

    public float maxVelocity;


    public float smallJumpForce;
    public float mediumJumpForce;
    public float largeJumpForce;

    Vector2 gravityVector;

    int count = 0;
    float force;
    Rigidbody2D rigidbody;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Use this for initialization
    void Start () {
        rigidbody = transform.GetComponent<Rigidbody2D>();
        SouthGravity();
        MediumJump();
    }
	void Update() {
		Vector2 newVelocitiy = Vector2.ClampMagnitude(rigidbody.velocity, maxVelocity);
        rigidbody.velocity = new Vector2(newVelocitiy.x, rigidbody.velocity.y);
        rigidbody.AddForce(gravityVector, ForceMode2D.Force);
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
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0.0f);
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
        gravityVector = new Vector3(0.0f, 9.81f, 0.0f);
    }

    public void SouthGravity()
    {
        gravityVector = new Vector3(0.0f, -9.81f, 0.0f);
    }

    public void WestGravity()
    {
        gravityVector = new Vector3(-9.81f, 0.0f, 0.0f);
    }

    public void EastGravity()
    {
        gravityVector = new Vector3(9.81f, 0.0f, 0.0f);
    }
}
