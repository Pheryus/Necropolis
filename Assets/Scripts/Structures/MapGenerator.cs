using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MapGenerator {


    public const float mapSize = 50;

    public const float minDistanceBetweenStructures = 5, maxDistanceBetweenStructures = 8;

    int iterations = 0;

    List<GenerateObjects> structures;

    StructuresPool structuresPool;

    public void SetStructuresPool (StructuresPool s) {
        structuresPool = s;
    }

    public class GenerateObjects {
        private float objXPosition, objYPosition;
        private int side;

        private Quaternion objRotation;

        public void GenerateXAxis() {
            side = Random.Range(0, 2);

            if (side == 0) {
                objXPosition = 1.819f;
            }
            else
                objXPosition = -1.753f;
        }

        public int GetSide() {
            return side;
        }
        
        public void GenerateYAxis(float lastObjectY) {
            float minRange = lastObjectY - minDistanceBetweenStructures;
            //Debug.Log("Range between: " + (lastObjectY - maxDistanceBetweenStructures) + " and " + minRange);
            objYPosition = Random.Range(lastObjectY - maxDistanceBetweenStructures, minRange);
            //Debug.Log("objYPosition: " + objYPosition);
        }

        public float GetObjectYPosition() {
            return objYPosition;
        }

        public Vector3 GetObjectVector  () {
            return new Vector3(objXPosition, objYPosition - 8, 0);
        }

        public Quaternion GetObjectRotation() {
            Vector3 rot = Vector3.zero;
            if (side == 0) {
                rot = new Vector3(0, 180, 0);
            }
            return Quaternion.Euler(rot);
        }
    }


    public void CreateMap() {
        int structuresNumber = NumberOfStructures();

        structures = new List<GenerateObjects>();

        float[] lastYPosition = new float[2];
        lastYPosition[0] = lastYPosition[1] = -1;

        for (int i = 0; i < structuresNumber; i++) {
            GenerateObjects obj = new GenerateObjects();
            obj.GenerateXAxis();
            int side = obj.GetSide();
            obj.GenerateYAxis(lastYPosition[side]);
            lastYPosition[side] = obj.GetObjectYPosition();

            structures.Add(obj);
            structuresPool.SpawnFromPool("House1", obj.GetObjectVector(), obj.GetObjectRotation());
        }        
    }

    public int NumberOfStructures() {
        return Random.Range(8, 10);
    }
    
    /// <summary>
    /// Verifica se o objeto está próximo de outros objetos do cenario.
    /// Se estiver muito proximo, sua posição está inválida.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public bool PositionIsValid(GenerateObjects obj) {
        foreach (GenerateObjects structure in structures) {
            if (Mathf.Abs(obj.GetObjectYPosition() - structure.GetObjectYPosition()) < 3)
                return false;
            iterations++;
        }
        return true;
    }


}