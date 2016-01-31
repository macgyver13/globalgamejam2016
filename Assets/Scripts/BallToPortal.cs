using UnityEngine;
using System.Collections;

public class BallToPortal : MonoBehaviour {

    public Transform portal;

    SpriteRenderer spriteRenderer;
    AudioSource audioSource;

    Vector2 startPosition;
    float time;

	// Use this for initialization
	void OnEnable () {
        startPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(animate());
    }

    IEnumerator animate()
    {
        audioSource.Play();
        yield return null;
        
        Hashtable arg = iTween.Hash("position", portal.position, "time", 1f);
        iTween.MoveTo(gameObject, arg);
    }

    // Update is called once per frame
    void Update () {
        time += Time.deltaTime;
        if (time <= 1f && portal != null)
        {
            transform.localScale = Vector2.Lerp(new Vector2(1f,1f), new Vector2(.5f, .5f), time);
            spriteRenderer.color = Color.Lerp(Color.white, Color.clear, time - .5f);
        }
	}
}
