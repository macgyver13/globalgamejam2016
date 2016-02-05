using UnityEngine;
using System.Collections;

public class CameraSizeFixer : MonoBehaviour {

    Camera camera;
    float originalOrtho;

    Vector2 screenSize;

    Vector2 preferedWorldScreenSize;

    float cameraHeight;
    float desiredAspect = 16f / 9f;

    // Use this for initialization
    void Start () {
        camera = GetComponent<Camera>();
        cameraHeight = camera.orthographicSize;
    }
	
	// Update is called once per frame
	void Update () {
        if(screenSize.x != camera.pixelWidth || screenSize.y != camera.pixelHeight)
        {
            FixCameraSize();
        }

    }

    void FixCameraSize()
    {
        if (cameraHeight * desiredAspect > cameraHeight * camera.aspect)
        {
            float ratio = desiredAspect / camera.aspect;
            Camera.main.orthographicSize = cameraHeight * ratio;
        }
    }
}
