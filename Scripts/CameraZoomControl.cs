using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomControl : MonoBehaviour
{

    private Camera cam;
    private float targetzoom;
    private float zoomfactor = 3f;
    private float zoomspeed = 10;

    // Start is called before the first frame update
    void Start()
    {

        cam = Camera.main;
        targetzoom = cam.orthographicSize;

    }

    // Update is called once per frame
    void Update()
    {

        float scrollinput;
        scrollinput = Input.GetAxis("Mouse ScrollWheel");
        targetzoom -= scrollinput * zoomfactor;
        targetzoom = Mathf.Clamp(targetzoom, 4.5f, 8f);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetzoom, Time.deltaTime * zoomspeed);

    }
}
