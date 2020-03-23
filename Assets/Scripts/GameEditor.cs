using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEditor : MonoBehaviour {

    public float cameraMovespeed;

    private void Update() {
        GameControl.cameraMovespeed = cameraMovespeed;
    }

}
