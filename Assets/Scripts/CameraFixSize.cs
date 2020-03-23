using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFixSize : MonoBehaviour {

    float multiplier;

    void Start(){
        float defaultAspectRatio = 9f / 16f;
        multiplier = defaultAspectRatio * 5;

        if (Mathf.Abs(multiplier/Camera.main.aspect - 5) < 0.1f) {
            Camera.main.orthographicSize = 5;
        }
        else
            Camera.main.orthographicSize = multiplier / Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {

        Camera.main.orthographicSize = multiplier / Camera.main.aspect;

    }
}
