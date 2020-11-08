using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInteraction : MonoBehaviour {   
    Vector3 touchPosition;

    // panning limits
    private float panUpLimit;
    private float panDownLimit;
    private float panLeftLimit;
    private float panRightLimit;

    // zooming limits
    private float zoomInLimit;
    private float zoomOutLimit;

    // true if currently zooming
    private bool zooming;

    // world points of the camera viewport
    private Vector3 cameraBottomLeftPosition;
    private Vector3 cameraTopRightPosition;

    void Awake() {
        Screen.orientation = ScreenOrientation.LandscapeRight;
    }

    void Start() {
        zoomInLimit = Camera.main.orthographicSize / 2;
        zoomOutLimit = Camera.main.orthographicSize;
        zooming = false;

        cameraBottomLeftPosition = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        cameraTopRightPosition = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

        panUpLimit = cameraTopRightPosition.y;
        panDownLimit = cameraBottomLeftPosition.y;
        panRightLimit = cameraTopRightPosition.x;
        panLeftLimit = cameraBottomLeftPosition.x;
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
                touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

            // GetMouseButton(0) is true as long as the screen touch (or left mouse button) is being held
            if (Input.GetMouseButton(0)) {
                Vector3 panDistance = touchPosition - Camera.main.ScreenToWorldPoint(Input.mousePosition);

                cameraBottomLeftPosition = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
                cameraTopRightPosition = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
                
                // if the panDistance brings the camera farther than the limit, set it so 
                //  the panDistance brings the camera exactly to the limit
                if (cameraTopRightPosition.y + panDistance.y > panUpLimit) {
                    panDistance.y = panUpLimit - cameraTopRightPosition.y;
                }
                else if (cameraBottomLeftPosition.y + panDistance.y < panDownLimit) {
                    panDistance.y = panDownLimit - cameraBottomLeftPosition.y;
                }
                if (cameraTopRightPosition.x + panDistance.x > panRightLimit) {
                    panDistance.x = panRightLimit - cameraTopRightPosition.x;
                }
                else if (cameraBottomLeftPosition.x + panDistance.x < panLeftLimit) {
                    panDistance.x = panLeftLimit - cameraBottomLeftPosition.x;
                }

                Camera.main.transform.position += panDistance;
            }
        }

        // allows zooming with a mouse. Not important for final build on mobile
        Zoom(Input.GetAxis("Mouse ScrollWheel"));
    }

    private void Zoom(float increment) {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomInLimit, zoomOutLimit);
    }
}
