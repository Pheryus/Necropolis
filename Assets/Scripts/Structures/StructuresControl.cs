using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructuresControl : MonoBehaviour {

    StructuresPool structuresPool;

    MapGenerator mapGenerator;

    float distance;

    private void Start() {
        structuresPool = GetComponent<StructuresPool>();
        mapGenerator = new MapGenerator();
        mapGenerator.SetStructuresPool(structuresPool);
        mapGenerator.CreateMap();
    }


    private void Update() {
        if (GameControl.verticalMoveEnabled) {
            distance += Time.deltaTime * GameControl.cameraMovespeed;
            if (distance >= 20) {
                distance = 0;
                mapGenerator.CreateMap();
            }
        }
    }

}
