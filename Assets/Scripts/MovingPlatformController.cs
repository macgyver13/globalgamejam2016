using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class MovingPlatformController : MonoBehaviour {

    public Rigidbody2D ballplatformRgbd;

    [SerializeField]
    public iTween.EaseType easeType;

    public float rayDistance;
    //public float offsetFromCollidingWall;

    public float movementSpeed;
    public float movementTime;

    public bool returnToOrigin;
    //public bool reverseInitialLookDirection;
    public bool lookVertically;
    public bool lookHorizontally;

    private Vector2 origin;
    private List<Vector2> lookDirections;

    private bool hasTween
    {
        get {
            return gameObject.GetComponent<iTween>() != null;
        }
    }

    void Start()
    {
        origin = transform.position;
        lookDirections = new List<Vector2>();
        if(lookHorizontally)
            lookDirections.AddRange(new Vector2[] { Vector2.right, Vector2.left });
        else if (lookVertically)
            lookDirections.AddRange( new Vector2[]{ Vector2.up, Vector2.down});

        //if (reverseInitialLookDirection)
        //    lookDirections.Reverse();

        StartTween(GetWallPosition());
    }

   Vector2 GetWallPosition()
    {
        RaycastHit2D hit = new RaycastHit2D();
        for (int i = 0; i < lookDirections.Count; i++)
        {
            hit = Physics2D.Raycast(transform.position, lookDirections[i], rayDistance);
            if (hit.collider != null && (hit.collider.gameObject.tag == "Floor" || hit.collider.gameObject.tag == "Wall"))
            {
                Vector2 movePosition = hit.collider.gameObject.transform.position;
                return AdjustPosition(movePosition);
            }
        }

        return Vector2.zero;
    }

    Vector2 AdjustPosition(Vector2 movePosition)
    {
        if (lookHorizontally)
        {
            float newX = (movePosition.x > 0) ? movePosition.x - transform.lossyScale.x : movePosition.x + transform.lossyScale.x;
            movePosition = new Vector2(newX, movePosition.y);
        }
        else if (lookVertically)
        {
            float newY = (movePosition.y > 0) ? movePosition.y - transform.lossyScale.y : movePosition.y + transform.lossyScale.y;
            movePosition = new Vector2(movePosition.x, newY);
        }

        return movePosition;
    }

    void StartTween(Vector2 moveToPosition)
    {
        lookDirections.Reverse();
        Debug.Log(moveToPosition);
        if (moveToPosition != Vector2.zero)
        {
            if (hasTween)
                DestroyTween();

            Hashtable args = new Hashtable();
            args.Add("x", moveToPosition.x );
            args.Add("y", moveToPosition.y);
            args.Add("easetype", easeType);
            args.Add("oncomplete", "TweenCompleted");

            if (movementSpeed > float.Epsilon)
                args.Add("speed", movementSpeed);
            else
                args.Add("time", movementTime);

            iTween.MoveTo(gameObject, args);
        }
    }

    void TweenCompleted()
    {
        if (returnToOrigin)
        {
            if ((Vector2)transform.position != origin)
                StartTween(origin);
            else
                StartTween(GetWallPosition());
        }
        else
            StartTween(GetWallPosition());
    }

    void DestroyTween()
    {
        Destroy(gameObject.GetComponent<iTween>());
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        GameObject otherObject = coll.gameObject;
        if ((otherObject.tag == "Floor" || otherObject.tag == "Wall"))
        {
            Debug.Log(otherObject.name);
            StartTween(GetWallPosition());
        }
    }

}
