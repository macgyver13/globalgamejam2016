using UnityEngine;
using System.Collections;

public class MagicJar : MonoBehaviour {
    public Transform TeleportLocation;

    private GameObject objectToTele;

    void OnCollisionEnter2D(Collision2D coll)
    {
        GameObject otherObject = coll.gameObject;
        if (otherObject.tag == "Ball")
        {
            objectToTele = otherObject;
            objectToTele.GetComponent<Rigidbody2D>().isKinematic = true;
            Hashtable args = iTween.Hash("alpha", 0f, "time", 1.5f);
            iTween.FadeTo(otherObject, args);

            StartCoroutine(Teleport());
        }
    }

   IEnumerator Teleport()
    {
        yield return new  WaitForSeconds(1.5f);
        objectToTele.transform.position = TeleportLocation.position;
        Hashtable args = iTween.Hash("alpha", 1f, "time", 1.5f);
        iTween.FadeTo(objectToTele, args);
        yield return new WaitForSeconds(1.5f);
        objectToTele.GetComponent<Rigidbody2D>().isKinematic = false;
    }
}
