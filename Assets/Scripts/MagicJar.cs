﻿using UnityEngine;
using System.Collections;

public class MagicJar : MonoBehaviour {
    public Transform TeleportLocation;

    private GameObject objectToTele;
    private BallToPortal btp;

    void OnTriggerEnter2D(Collider2D coll)
    {
        GameObject otherObject = coll.gameObject;
        if (otherObject.tag == "Ball")
        {
            objectToTele = otherObject;
            objectToTele.GetComponent<Rigidbody2D>().isKinematic = true;
            btp = otherObject.AddComponent<BallToPortal>();
            btp.portal = transform;
            StartCoroutine(Teleport());
        }
    }

   IEnumerator Teleport()
    {
        yield return new  WaitForSeconds(1.1f);
        objectToTele.transform.position = TeleportLocation.position;
        Destroy(btp);
        BallOutOfPortal botp = objectToTele.AddComponent<BallOutOfPortal>();
        botp.portal = TeleportLocation;
        yield return new WaitForSeconds(1.1f);
        Destroy(botp);
        objectToTele.GetComponent<Rigidbody2D>().isKinematic = false;
    }
}
