using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class MovingPlatformController : MonoBehaviour {

    [SerializeField]
    public iTween.EaseType easeType;

    public float rayDistance;

    public float speed;
    public float time;

    public bool sticky;

    [HideInInspector]
    public bool useBoth;
    public bool rotate;

    [SerializeField]
    public iTween.LoopType rotationLoopType;
    public Vector3 rotationAngles;

    public bool returnToOrigin;
    public bool reverseInitialLookDirection;
    public bool lookVertically;
    public bool lookHorizontally;

    private Vector2 origin;
    private Vector3 originRotation;
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
        originRotation = transform.eulerAngles;
        lookDirections = new List<Vector2>();
        if(lookHorizontally)
            lookDirections.AddRange(new Vector2[] { Vector2.right, Vector2.left });
        else if (lookVertically)
            lookDirections.AddRange( new Vector2[]{ Vector2.up, Vector2.down});

        if (reverseInitialLookDirection)
            lookDirections.Reverse();

        if (useBoth)
        {
            StartTween( rotationAngles, "rotation" );
            StartTween((Vector3)GetWallPosition(), "movement");
        }
        else
            StartTween((rotate) ? rotationAngles : (Vector3)GetWallPosition(), "rotation");
    }

   Vector2 GetWallPosition()
    {
        RaycastHit2D hit = new RaycastHit2D();
        for (int i = 0; i < lookDirections.Count; i++)
        {
            hit = Physics2D.Raycast(transform.position, lookDirections[i], rayDistance);
            if (hit.collider != null && (hit.collider.gameObject.tag == "Floor" || hit.collider.gameObject.tag == "Wall"))
            {
                Vector2 movePosition = hit.point;
                return AdjustPosition(movePosition);
            }
        }

        return Vector2.zero;
    }

    Vector2 AdjustPosition(Vector2 movePosition)
    {
        if (lookHorizontally)
        {
            float newX = (movePosition.x > 0) ? movePosition.x : movePosition.x;
            movePosition = new Vector2(newX, movePosition.y);
        }
        else if (lookVertically)
        {
            float newY = (movePosition.y > 0) ? movePosition.y  : movePosition.y ;
            movePosition = new Vector2(movePosition.x, newY);
        }

        return movePosition;
    }

    void StartTween(Vector3 moveToOrRotatePosition, string tweenType)
    {
        lookDirections.Reverse();
        Debug.Log(moveToOrRotatePosition);
        if (moveToOrRotatePosition != Vector3.zero)
        {
            if (hasTween)
                DestroyTween(tweenType);

            Hashtable args = new Hashtable();
            args.Add("x", (rotate) ? rotationAngles.x : moveToOrRotatePosition.x );
            args.Add((rotate) ? "z" : "y", (rotate) ? rotationAngles.z : moveToOrRotatePosition.y);
            args.Add("easetype", easeType);
            if((returnToOrigin && !rotate && (Vector2)transform.position == origin))
                args.Add("oncomplete", "TweenCompleted");

            if (speed > float.Epsilon)
                args.Add("speed", speed);
            else
                args.Add("time", time);

            if (rotate || (useBoth && tweenType == "rotation"))
            {
                args.Add("name", "rotation");
                args.Add("looptype", rotationLoopType);
                iTween.RotateAdd(gameObject, args);
            }
            else
            {
                args.Add("name", "movement");
                iTween.MoveTo(gameObject, args);
            }
        }
    }

    void TweenCompleted()
    {
        StartTween(origin, "movement");
    }

    void DestroyTween(string tweenType)
    {
        iTween[] tweens =  gameObject.GetComponents<iTween>();
        for (int i = 0; i < tweens.Length; i++)
        {
            Debug.Log(tweens[i]._name);
            if (tweens[i]._name == tweenType)
            {
                Destroy(tweens[i]);
                break;
            }

        }
    }

    public void ApplyTheSticky(Collider2D coll, RelativeJoint2D jointToApplyStick)
    {
        GameObject otherObject = coll.gameObject;
        if (otherObject.tag == "Ball" && sticky)
        {
            jointToApplyStick.connectedBody = otherObject.GetComponent<Rigidbody2D>();
            otherObject.GetComponent<BallController>().jointConnectTo = jointToApplyStick;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        GameObject otherObject = coll.gameObject;
        if ((otherObject.tag == "Floor" || otherObject.tag == "Wall"))
        {
            if (rotate)
                 StartTween(rotationAngles, "rotation");
            else
               StartTween((returnToOrigin && (Vector2)transform.position != origin) ? origin : GetWallPosition(), "movement");
        }
    }

}
