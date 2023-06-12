using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGenelator : MonoBehaviour{
    public GameObject wallPrefab;
    public int wallSize;
    // Start is called before the first frame update
    void Start(){
        wallSize = InputValue.roomSize;
        for(int i=0; i<4; i++){
            GameObject wallObject = Instantiate(wallPrefab);
            float angle = (2.0f * Mathf.PI * i) / 4.0f;
            wallObject.transform.localScale = new Vector3(wallSize * Mathf.Abs(Mathf.Sin(angle)) + 0.1f, 5.0f, wallSize * Mathf.Abs(Mathf.Cos(angle)) + 0.1f);
            wallObject.transform.position = new Vector3(wallSize * Mathf.Cos(angle) / 2, 2.5f, wallSize * Mathf.Sin(angle) / 2);
        }
    }

}
