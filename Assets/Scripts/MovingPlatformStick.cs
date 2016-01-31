using UnityEngine;
using System.Collections;

public class MovingPlatformStick : MonoBehaviour {

    // Moving platform support
    private Transform player;
    private Rigidbody2D playerRigidbody;
    private float xOffset;


    void Update()
    {
        if (player)
        {
            if (playerRigidbody.velocity.x < .1f && playerRigidbody.velocity.x > -.1f)
            {
                player.transform.position = new Vector2(transform.position.x + xOffset, player.transform.position.y);
            }
            else
            {
                xOffset = player.transform.position.x - transform.position.x;
            }
        }
            
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject == BallController.instance.gameObject)
        {
            xOffset = coll.transform.position.x - transform.position.x;
            player = coll.gameObject.transform;
            playerRigidbody = player.GetComponent<Rigidbody2D>();
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject == BallController.instance.gameObject)
            player = null;
    }
}
