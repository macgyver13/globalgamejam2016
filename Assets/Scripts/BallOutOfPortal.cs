using UnityEngine;
using System.Collections;

public class BallOutOfPortal : MonoBehaviour
{

    public Transform portal;

    SpriteRenderer spriteRenderer;
    AudioSource audioSource;

    Vector2 startPosition;
    float time;

    // Use this for initialization
    void OnEnable()
    {
        startPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time <= 1f && portal != null)
        {
            transform.localScale = Vector2.Lerp(new Vector2(.5f, .5f), new Vector2(1f, 1f), time);
            spriteRenderer.color = Color.Lerp(Color.clear, Color.white, time);
        }
    }
}
