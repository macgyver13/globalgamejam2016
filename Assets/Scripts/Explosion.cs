using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

    public Rigidbody2D[] pices;

    void OnEnable()
    {
        for (int i = 0; i < pices.Length; i++)
        {
            pices[i].AddForce(Random.insideUnitCircle * 100f);
            pices[i].AddTorque(Random.Range(100f, 100f));
        }
    }
}
