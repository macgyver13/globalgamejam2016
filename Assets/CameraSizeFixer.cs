using UnityEngine;
using System.Collections;

public class CameraSizeFixer : MonoBehaviour {

    Camera camera;
    float originalOrtho;

    Vector2 screenSize;

    Vector2 preferedWorldScreenSize;

    float cameraHeight;
    float desiredAspect = 16f / 9f;
    float oldAspect;

    // Use this for initialization
    void Start () {
        camera = GetComponent<Camera>();
        cameraHeight = camera.orthographicSize;
    }
	
	// Update is called once per frame
	void Update () {
        if (oldAspect != camera.aspect)
        {
            FixCameraSize();
        }
    }

    void FixCameraSize()
    {
        oldAspect = camera.aspect;
        if (cameraHeight * desiredAspect > cameraHeight * camera.aspect)
        {
            print("works" + camera.aspect);
            float ratio = desiredAspect / camera.aspect;
            Camera.main.orthographicSize = cameraHeight * ratio;
        }
        else
        {
            camera.orthographicSize = cameraHeight;
        }
    }
}
