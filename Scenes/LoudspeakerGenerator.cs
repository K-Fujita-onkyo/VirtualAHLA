using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoudspeakerGenerator : MonoBehaviour{
    
    public GameObject loudspeakerPrefab;
    public static List<GameObject> loudspeakerList = new List<GameObject>();
    public int LoudspeakerNum;


    float radius = 1.5f;
    static float angle;

    private float roomSize;


    void Start(){

        LoudspeakerNum = InputValue.loudspeakerNum;
        roomSize = InputValue.roomSize;
        radius = InputValue.roomSize / 6.0f;
        
        for(int i=0; i<LoudspeakerNum; i+=1){
            angle = 2*Mathf.PI*i/LoudspeakerNum;
            GameObject loudspeakerObject = Instantiate(loudspeakerPrefab);
            loudspeakerObject.GetComponent<MovingLoudspeakers>().initAngle = angle;
            loudspeakerObject.GetComponent<MovingLoudspeakers>().initRadius = radius;
            Debug.Log("radius: " + loudspeakerObject.GetComponent<MovingLoudspeakers>().initRadius);
            loudspeakerList.Add(loudspeakerObject);
            loudspeakerObject.GetComponent<LoudspeakerState>().number = i;
            loudspeakerObject.transform.position = new Vector3(radius * Mathf.Cos(angle), 1, radius * Mathf.Sin(angle));
        }
    }
}
