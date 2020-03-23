using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour {

    void Update(){
        if (GameControl.canMove())
            transform.position += Vector3.up * GameControl.cameraMovespeed * Time.deltaTime;

        if (transform.position.y > 7)
            gameObject.SetActive(false);
    }




}
