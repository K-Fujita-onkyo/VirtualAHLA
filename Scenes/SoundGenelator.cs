using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundGenelator : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject soundPrefab;
    public static List<GameObject> soundList = new List<GameObject>();
    public int soundNum;

    float radius;
    static float angle;


    void Start(){
        
        radius = InputValue.roomSize / 3.0f;
        soundNum = InputValue.soundNum;
        for(int i=0; i<soundNum; i+=1){
            angle = 2*Mathf.PI*i/soundNum;
            GameObject soundObject = Instantiate(soundPrefab);
            soundList.Add(soundObject);
            soundObject.transform.position = new Vector3(radius * Mathf.Cos(angle), 1, radius * Mathf.Sin(angle));
        }
    }
}
