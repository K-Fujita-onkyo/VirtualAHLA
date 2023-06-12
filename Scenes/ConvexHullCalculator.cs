using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Clockwise {
    public int index;
    public float angle;
}

public class ConvexHullCalculator : MonoBehaviour {

    public List<GameObject> convexHullList = new List<GameObject>();
    
    public List<GameObject> loudspeakerList = new List<GameObject>();
    public List<GameObject> soundList = new List<GameObject>();
    public GameObject listener;
    public int convexNum = 0;
    public LineRenderer line;

    private int maxIndex = -1;
    private int roomSize;
    private Vector3 lowerLeftCorner;
    private Vector3 originPoint;

    public List<Clockwise> clockList = new List<Clockwise>();

    public ConvexHullCalculator(){
        loudspeakerList = LoudspeakerGenerator.loudspeakerList;
        soundList = SoundGenelator.soundList;
        roomSize = InputValue.roomSize;
        Debug.Log(roomSize);
        lowerLeftCorner = new Vector3(-(float)roomSize/1.0f, 1.0f, -(float)roomSize/1.0f);
        Debug.Log(lowerLeftCorner.x + " " + lowerLeftCorner.y + " " + lowerLeftCorner.z);
    }


    public void clearConvexHull(){
        clockList.Clear();
        convexHullList.Clear();
    }

    public void calcConvexHull(){

        int i = 1;
        findMaxDistance();
        clockwiseSort();

        for(int j=0; j<convexHullList.Count; j++){
            convexHullList[j].GetComponent<Renderer>().material.color = Color.black;
            convexHullList[j].GetComponent<LoudspeakerState>().convexHullOrNot = false;
        }

        if(convexHullList.Count>3){

            while(i<convexHullList.Count){
                Vector3 p1 = convexHullList[(i - 1)].transform.position;
                Vector3 p2 = convexHullList[(i)].transform.position;
                Vector3 p3 = convexHullList[(i + 1) % convexHullList.Count].transform.position;
                if(calc3PointAngle(p1, p2, p3) < 0.0f){
                    i++;

                } else {
                    convexHullList.RemoveAt(i);
                    if(i>1) i--;
                }
            }
        }

        for(int j=0; j<convexHullList.Count; j++){
            convexHullList[j].GetComponent<Renderer>().material.color = Color.red;
            convexHullList[j].GetComponent<LoudspeakerState>().convexHullOrNot = true;
        }
    }

    void findMaxDistance(){

        maxIndex = -1;
        float maxDist = Mathf.Infinity;

        for(int i=0; i<loudspeakerList.Count; i++){
            if(maxDist > Vector3.Distance(loudspeakerList[i].transform.position, lowerLeftCorner)){
                maxDist = Vector3.Distance(loudspeakerList[i].transform.position, lowerLeftCorner);
                maxIndex = i;
            }
        }

        convexHullList.Add(loudspeakerList[maxIndex]);
        originPoint = loudspeakerList[maxIndex].transform.position;
    }

    void clockwiseSort(){

        Clockwise clockData;

        for(int i=0; i<loudspeakerList.Count; i++){
            if(i!=maxIndex){
                clockData.index = i;
                Vector3 point = loudspeakerList[i].transform.position - convexHullList[0].transform.position;
                clockData.angle = Mathf.Atan2(point.x, point.z);
                clockList.Add(clockData);
            }
        }

        clockList.Sort((a, b) => (int)Mathf.Sign(a.angle - b.angle));

        for(int i=0; i<clockList.Count; i++){
            convexHullList.Add(loudspeakerList[clockList[i].index]);
        }
    }

    float calc3PointAngle(Vector3 p1, Vector3 p2, Vector3 p3){
        return (p2.x - p1.x)*(p3.z - p2.z) - (p2.z - p1.z)*(p3.x - p2.x);
    }


    
}
