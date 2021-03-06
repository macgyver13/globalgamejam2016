﻿using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {
	public static BallController instance;

    public GameObject ballExplosion;
    public GameObject ballStandin;

    public AudioSource audioSource;
	
	public AudioClip[] bounceSounds;

    public float gravityScale = 1f;
	
	public float slowSpeed;
	public float nomralSpeed;
	public float fastSpeed;
	
	public float slowMaxVelocity;
	public float normalMaxVelocity;
	public float fastMaxVelocity;
	
	public float smallJumpForce;
	public float mediumJumpForce;
	public float largeJumpForce;
	
	float maxVelocity;
	float movementForce;
	public Vector2 gravityVector;
	public bool canControl = true;
	int count = 0;
	float force;
    public bool isOnGround = true;
	Rigidbody2D rigidbody;

    [HideInInspector]
    public RelativeJoint2D jointConnectTo;
	
	void Awake()
	{
		if (instance == null)
			instance = this;
		SouthGravity();
		MediumJump();
		NormalSpeed();
	}
	
	// Use this for initialization
	void Start () {
		rigidbody = transform.GetComponent<Rigidbody2D>();
	}
	void FixedUpdate() {
        if (gravityVector.x > 0 || gravityVector.x < 0)
        {
            Vector2 newVelocity = Vector2.ClampMagnitude(new Vector2(0, rigidbody.velocity.y), maxVelocity);
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, newVelocity.y);
        }
        else
        {
            Vector2 newVelocity = Vector2.ClampMagnitude(new Vector2(rigidbody.velocity.x, 0), maxVelocity);
            rigidbody.velocity = new Vector2(newVelocity.x, rigidbody.velocity.y);
        }

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
            PerlinShake.instance.PlayShake (0.2f, 10.0f, 0.1f);
			ContactPoint2D contact = coll.contacts[0];
			Vector2 direction = (Vector2)transform.position - contact.point;
			print(direction);
			if (gravityVector.x > 0 && ((direction.y > 0.2f || direction.y < -0.2f) || direction.x > 0))
			{
                return;
			}
			else if (gravityVector.x < 0 && ((direction.y > 0.2f || direction.y < -0.2f) || direction.x < 0))
			{
                return;
			}
			else if (gravityVector.y > 0 && ((direction.x > 0.2f || direction.x < -0.2f) || direction.y > 0))
			{
                return;
			}
			else if (gravityVector.y < 0 && ((direction.x > 0.2f || direction.x < -0.2f) || direction.y < 0))
			{
                return;
			}

            PlayBounceSound();
            isOnGround = true;


            
			
			
		} else if (coll.gameObject.tag == "Border") {
			PerlinShake.instance.PlayShake (0.2f, 15.0f, 0.15f);
            gameObject.SetActive(false);
            Instantiate(ballExplosion, transform.position, Quaternion.identity);
            if (GameManager.instance != null)
            {
                GameManager.instance.ResetLevel();
            }
            else
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        } else if (coll.gameObject.tag == "End") {
            //End scene
            gameObject.SetActive(false);
            GameObject ballS = (GameObject)Instantiate(ballStandin, transform.position, Quaternion.identity);
            ballS.GetComponent<BallToPortal>().portal = coll.gameObject.transform;
            if (GameManager.instance != null)
            {
                GameManager.instance.EndLevel();
            }
            else
            {
                Application.LoadLevel(Application.loadedLevel);
            }
		}
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "End") {
            //End scene
            gameObject.SetActive(false);
            GameObject ballS = (GameObject)Instantiate(ballStandin, transform.position, Quaternion.identity);
            ballS.GetComponent<BallToPortal>().portal = coll.gameObject.transform;
            if (GameManager.instance != null)
            {
                GameManager.instance.EndLevel();
            }
            else
            {
                Application.LoadLevel(Application.loadedLevel);
            }
		}
    }

    public bool IsOnGround()
    {
        Debug.Log("Ball stuff" + isOnGround);
        return isOnGround || jointConnectTo != null;
    }

    public void Jump()
    {
        if (!IsOnGround())
            return;
        isOnGround = false;
        if (gravityVector.x > 0)
        {
            rigidbody.velocity = new Vector2(0.0f, rigidbody.velocity.y);
            rigidbody.AddForce(Vector2.left * force);
        }
        else if (gravityVector.x < 0)
        {
            rigidbody.velocity = new Vector2(0.0f, rigidbody.velocity.y);

            rigidbody.AddForce(Vector2.right * force);
        }
        else if (gravityVector.y > 0)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0.0f);

            rigidbody.AddForce(Vector2.down * force);
        }
        else if (gravityVector.y < 0)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0.0f);

            rigidbody.AddForce(Vector2.up * force);
        }
    }

	public void CanControl (bool _canControl) {
		canControl = _canControl;
	}
	
	public void Right()
	{
		if (!canControl) {
			return;
		}
		if (gravityVector.x > 0)
		{
			rigidbody.AddForce(new Vector2(0, 1) * movementForce);
		}
		else if (gravityVector.x < 0)
		{
			rigidbody.AddForce(new Vector2(0, -1) * movementForce);
		}
		else if (gravityVector.y > 0)
		{
			rigidbody.AddForce(new Vector2(-1, 0) * movementForce);
		}
		else if (gravityVector.y < 0)
		{
			rigidbody.AddForce(new Vector2(1, 0) * movementForce);
		}
	}
	public void Left()
	{
		if (!canControl) {
			return;
		}
		if (gravityVector.x > 0)
		{
			rigidbody.AddForce(new Vector2(0, -1) * movementForce);
		}
		else if (gravityVector.x < 0)
		{
			rigidbody.AddForce(new Vector2(0, 1) * movementForce);
		}
		else if (gravityVector.y > 0)
		{
			rigidbody.AddForce(new Vector2(1, 0) * movementForce);
		}
		else if (gravityVector.y < 0)
		{
			rigidbody.AddForce(new Vector2(-1, 0) * movementForce);
		}
	}

    public void Stop()
    {
        rigidbody.angularVelocity = 0;
       // if (isOnGround)
       //     rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
    }
	
	public void BigSize()
	{
		transform.localScale.Set(2f,2f,2f);
	}
	
	public void NormalSize()
	{
		transform.localScale.Set(1f, 1f, 1f);
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
        print("gravity north");
		gravityVector = new Vector3(0.0f, 9.81f, 0.0f) * gravityScale;
	}
	
	public void SouthGravity()
	{
		gravityVector = new Vector3(0.0f, -9.81f, 0.0f) * gravityScale;
	}
	
	public void WestGravity()
	{
		gravityVector = new Vector3(-9.81f, 0.0f, 0.0f) * gravityScale;
	}
	
	public void EastGravity()
	{
		gravityVector = new Vector3(9.81f, 0.0f, 0.0f) * gravityScale;
	}
	
	public void SlowSpeed()
	{
		maxVelocity = slowMaxVelocity;
		movementForce = slowSpeed;
	}
	
	public void NormalSpeed()
	{
		maxVelocity = normalMaxVelocity;
		movementForce = nomralSpeed;
	}
	
	public void FastSpeed()
	{
		maxVelocity = fastMaxVelocity;
		movementForce = fastSpeed;
	}
}
