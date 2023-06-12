using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour {
    // Start is called before the first frame update
    public GameObject floorPrefab;
    public int floorSize;

    void Start(){

        floorSize = InputValue.roomSize;

        GameObject floorObject = Instantiate(floorPrefab);
        floorObject.transform.localScale = new Vector3(floorSize, 0.01f, floorSize);
    }
}
