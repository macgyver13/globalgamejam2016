using UnityEngine;
using System.Collections;

public class Sticky : MonoBehaviour {

    public MovingPlatformController mpController;

    void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("Trigger entered");
        mpController.ApplyTheSticky(coll, GetComponent<RelativeJoint2D>());
    }
}
