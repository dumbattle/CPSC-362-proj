using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchUIManager : UIManager {   
    private static Vector3 touchPosition;

    // camera boundaries
    private static float cameraTopLimit;
    private static float cameraBottomLimit;
    private static float cameraLeftLimit;
    private static float cameraRightLimit;

    // zooming limits
    private static float zoomInLimit;
    private static float zoomOutLimit;

    // true if currently zooming
    private static bool zooming;

    private static Camera camera;
    // world points of the camera viewport
    private static Vector3 cameraBottomLeftPosition;
    private static Vector3 cameraTopRightPosition;

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

    public static new void CustomUpdate() {
        Zoom();
        Pan();
        
        // allows zooming with a mouse. Not important for final build on mobile
        camera.orthographicSize = Mathf.Clamp(camera.orthographicSize - Input.GetAxis("Mouse ScrollWheel"), zoomInLimit, zoomOutLimit);
        
        BringCameraIntoBounds();
    }

    // zooms into the screen using "two-finger pinch"
    private static void Zoom() {
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
            
            // resizes camera based on 'difference'
            // 0.01f determines the zooming speed
            // Clamp restricts the zoom to the limits set in Start()
            camera.orthographicSize = Mathf.Clamp(camera.orthographicSize - (difference * 0.01f), zoomInLimit, zoomOutLimit);
        }
        // 'zooming' is set to false once both fingers are off the screen after zooming.
        // prevents panning during zooming when the user lets go of one finger but not both
        else if (Input.touchCount == 0) {
            zooming = false;
        }
    }

    // moves the screen by touching the screen and dragging (or by clicking and dragging with a mouse)
    private static void Pan() {
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
    }

    // moves the camera into the bounds if it is currently outside the bounds
    private static void BringCameraIntoBounds() {
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
