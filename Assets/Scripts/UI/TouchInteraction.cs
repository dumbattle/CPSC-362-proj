using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInteraction : MonoBehaviour {   
    Vector3 touchPosition;

    // camera boundaries
    public float cameraTopLimit;
    public float cameraBottomLimit;
    public float cameraLeftLimit;
    public float cameraRightLimit;

    // zooming limits
    private float zoomInLimit;
    private float zoomOutLimit;

    // true if currently zooming
    private bool zooming;

    Camera camera;
    // world points of the camera viewport
    private Vector3 cameraBottomLeftPosition;
    private Vector3 cameraTopRightPosition;

    void Awake() {
        Screen.orientation = ScreenOrientation.LandscapeRight;
    }

    void Start() {
        camera = Camera.main;

        zoomInLimit = camera.orthographicSize / 2;
        zoomOutLimit = camera.orthographicSize;
        zooming = false;

        cameraBottomLeftPosition = camera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        cameraTopRightPosition = camera.ViewportToWorldPoint(new Vector3(1, 1, 0));

        cameraTopLimit = cameraTopRightPosition.y;
        cameraBottomLimit = cameraBottomLeftPosition.y;
        cameraRightLimit = cameraTopRightPosition.x;
        cameraLeftLimit = cameraBottomLeftPosition.x;
    }

    void Update() {   
        // ZOOMING
        // touchCount is the number of simlutaneous touches on the screen
        if (Input.touchCount == 2) {
            zooming = true;

            // GetTouch(0) is the first touch and GetTouch(1) is the second touch
            Touch touchOne = Input.GetTouch(0);
            Touch touchTwo = Input.GetTouch(1);

            // position - deltaPosition gives the previous position
            Vector2 prevTouchOnePosition = touchOne.position - touchOne.deltaPosition;
            Vector2 prevTouchTwoPosition = touchTwo.position - touchTwo.deltaPosition;

            // magnitude gives the length of the distance between two positions
            float prevDistance = (prevTouchOnePosition - prevTouchTwoPosition).magnitude;
            float curDistance = (touchOne.position - touchTwo.position).magnitude;

            float difference = curDistance - prevDistance;
            
            // 0.01f determines the zooming speed
            Zoom(difference * 0.01f);     
        }
        // 'zooming' is set to false once both fingers are off the screen after zooming.
        // prevents panning during zooming when the user lets go of one finger but not both
        else if (Input.touchCount == 0) {
            zooming = false;
        }

        // PANNING
        if (!zooming) {
            // GetMouseButtonDown(0) is equivalent to tapping the screen once on mobile
            if (Input.GetMouseButtonDown(0)) {
                touchPosition = camera.ScreenToWorldPoint(Input.mousePosition);
            }

            // GetMouseButton(0) is true as long as the screen touch (or left mouse button) is being held
            if (Input.GetMouseButton(0)) {
                Vector3 panDistance = touchPosition - camera.ScreenToWorldPoint(Input.mousePosition);

                cameraBottomLeftPosition = camera.ViewportToWorldPoint(new Vector3(0, 0, 0));
                cameraTopRightPosition = camera.ViewportToWorldPoint(new Vector3(1, 1, 0));
                
                camera.transform.position += panDistance;
            }
        }

        // allows zooming with a mouse. Not important for final build on mobile
        Zoom(Input.GetAxis("Mouse ScrollWheel"));

        BringCameraIntoBounds();
    }

    private void Zoom(float increment) {
        camera.orthographicSize = Mathf.Clamp(camera.orthographicSize - increment, zoomInLimit, zoomOutLimit);
    }

    private void BringCameraIntoBounds() {
        cameraBottomLeftPosition = camera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        cameraTopRightPosition = camera.ViewportToWorldPoint(new Vector3(1, 1, 0));

        Vector3 moveDistance = new Vector3(0, 0, 0);

        if (cameraTopRightPosition.y > cameraTopLimit) {
            moveDistance.y = cameraTopLimit - cameraTopRightPosition.y;
        }
        else if (cameraBottomLeftPosition.y < cameraBottomLimit) {
            moveDistance.y = cameraBottomLimit - cameraBottomLeftPosition.y;
        }
        if (cameraTopRightPosition.x > cameraRightLimit) {
            moveDistance.x = cameraRightLimit - cameraTopRightPosition.x;
        }
        else if (cameraBottomLeftPosition.x < cameraLeftLimit) {
            moveDistance.x = cameraLeftLimit - cameraBottomLeftPosition.x;
        }

        camera.transform.position += moveDistance;
    }
}
