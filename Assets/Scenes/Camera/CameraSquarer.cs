using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class CameraSquarer : MonoBehaviour
{
    public Vector2 camSize;
    public Vector2 screenPos;

    [Range(0, 1)]
    public float scale;

    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        ValidateCam();
        ValidateSize();
        SetOrthSize();

        var ss = GetScreenSize();
        var sp = GetScreenPos();

        cam.rect = new Rect(sp.x, sp.y, ss.x, ss.y);

    }

    private void ValidateCam() {
        cam = cam ?? GetComponent<Camera>();
        cam.orthographic = true;
    }

    void ValidateSize() {
        camSize = new Vector2(
            camSize.x > 0 ? camSize.x : -camSize.x,
            camSize.y > 0 ? camSize.y : -camSize.y
            );
    }

    void SetOrthSize() {
        cam.orthographicSize = camSize.y / 2;
    }


    Vector2 GetScreenSize() {
        float y = camSize.y / camSize.x;


        int w = Screen.width;
        int h = Screen.height;


        float yp = (y * w);

        float r = yp / h;
        if (yp > h) {
            return new Vector2(1/r, 1) * scale;
        }
        else {
            return new Vector2(1, r) * scale;
        }
    }

    Vector2 GetScreenPos() {
        screenPos.x = Mathf.Clamp(screenPos.x, 0, 1);
        screenPos.y = Mathf.Clamp(screenPos.y, 0, 1);
        float xr = 1-cam.rect.width;
        float yr = 1-cam.rect.height;

        float x = screenPos.x * xr;
        float y = screenPos.y * yr;

        return new Vector2(x, y);
    }
}
