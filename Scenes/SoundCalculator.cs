using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SoundCalculator : MonoBehaviour {

    public float[] clipBuffer;
    public int bufHead;
    public int bufTail;
    public float distance1;
    public GameObject audioClipGetter;

    private static float halfRoomSize;
    private static int samplelate = 44100;
    private static int blockSize = 5000;
    private static int marginIndex = 1000;
    private static float c = 340.0f; //sound of speed
    private static float refDistance = 1.0f;
    private static float absorption = 0.5f;

    //public GameObject test;
    private GameObject sound;
    private Vector3 soundPoint;
    private List<GameObject> convList;
    private LoudspeakerState loudState;
    private AudioSource audioSource;
    private AudioClip audioClip;
    private float[] originalClip;
    private float[][] originalClipList;
    private int timeSamples;
    private int sampleLength;
    private int loudNumber;
    private float HRTFAttenuation;
    private float popopo;
    private LineClipping lineClipper;
    private DateTime dt_a;
    private DateTime dt_n;
    private int listIndex;
    public TimeSpan a;

    void Start(){

        halfRoomSize = InputValue.roomSize / 2.0f;
        loudNumber = GetComponent<LoudspeakerState>().number;

        bufHead = 0;
        bufTail = 0;

        audioSource = GetComponent<AudioSource>();
        audioClip = GetComponent <AudioClip>();
        originalClip = GetComponent<AudioClipGetter>().getAudioClip(0);
        originalClipList = new float[4][];
        // for(int i=0; i<4; i++){
        //      originalClipList[i] = GetComponent<AudioClipGetter>().getAudioClip(i);
        // }

        originalClipList =  GetComponent<AudioClipGetter>().getAudioClip();
        
        audioClip = AudioClip.Create("audioSample", originalClip.Length, 1, samplelate, false);
        audioClip.SetData(originalClip, 0);
        audioSource.clip = audioClip;
        audioSource.loop = true;
        audioSource.Play();
        sampleLength = audioSource.clip.samples;
        // originalClip = new float[sampleLength];

        //blockSize = sampleLength;

        clipBuffer = new float[blockSize];
        loudState = GetComponent<LoudspeakerState>();

        lineClipper = new LineClipping();

        absorption = InputValue.absorptionValue;

        HRTFAttenuation = 1.0f;

    }

    float getDistance(){
        Vector3 loudPos3 = GetComponent<Transform>().position;
        Vector3 soundPos3 = sound.transform.position;

        Vector2 loudPos2 = new Vector2(loudPos3.x, loudPos3.z);
        Vector2 soundPos2 = new Vector2(soundPos3.x, soundPos3.z);

        Vector2.Distance(loudPos2, soundPos2);
        return Vector2.Distance(loudPos2, soundPos2);
    }
    float getDistance(Vector3 position){
        return Vector3.Distance(GetComponent<Transform>().position, position);
    }
    
    Vector3 getImageSourcePoint(Vector3 soundPoint, Vector3 symmetry){
        return soundPoint + symmetry;
    }
    float getAttenuation(float distance) {
        if(distance > 1.0f) return refDistance / distance;
        else return 1.0f;
    }

    float getPower(int num){
        return 1.0f / Mathf.Sqrt(num);
    }


    float getHRTFAttenuation() {
        float distance;
        distance = getDistance(new Vector3(0, 1, 0));
        if(distance > 1.6f) return 1.6f / distance;
        else return 1.0f;
    }

    int getDelayIndex(float distance){
        return (int)((distance/c) * samplelate);
    }

    float getRefRatio(){
        return Mathf.Sqrt(1.0f-absorption);
    }

    private Vector2 vec3To2(Vector3 vec3){
        return new Vector2(vec3.x, vec3.z);
    }

    Vector3 calcMoveVec3(Vector3 point, float wallSize, int wallNumber){

        Vector3 moveVec;

        switch(wallNumber){

            case 0:
                moveVec = new Vector3((wallSize - point.x)*2.0f, 0.0f, 0.0f);
                break;
            case 1:
                moveVec = new Vector3((-wallSize - point.x)*2.0f, 0.0f, 0.0f);
                break;
            case 2:
                moveVec = new Vector3(0.0f, 0.0f, (wallSize - point.z)*2.0f);
                break;
            case 3:
                moveVec = new Vector3(0.0f, 0.0f, (-wallSize - point.z)*2.0f);
                break;
            default:
                moveVec = new Vector3(0.0f, 0.0f, 0.0f);
                break;
        }

        return moveVec;
    }

    
    void calcDirect(){

        float distance;
        float attenuation;
        int delayIndex;

        distance = getDistance();
        distance1 = distance;
        attenuation = getAttenuation(distance);
        delayIndex = getDelayIndex(distance);

        //delayIndex = 0;
        for(int j = 0; j < clipBuffer.Length; j++){
            // if(j > delayIndex && j < delayIndex + blockSize){
            //         clipBuffer[j] += originalClip[(sampleLength + timeSamples + j - delayIndex)%sampleLength] * attenuation;
            // }else{
            //         clipBuffer[j] += 0;
            // }
            if(j < blockSize){
                clipBuffer[j] += originalClipList[listIndex][(sampleLength + timeSamples + j - marginIndex - delayIndex)%sampleLength] * attenuation * HRTFAttenuation * popopo;
                
            }
        }
    }

    void calcReflect(){
        
        float distance;
        float attenuation;
        int delayIndex;
        Vector3[] imagePointList = new Vector3[4];
        Vector2 start;
        Vector2 end;

        for(int i=0; i<imagePointList.Length; i++){
            imagePointList[i] = soundPoint + calcMoveVec3(soundPoint, halfRoomSize, i);
        }

        for(int i=0; i<imagePointList.Length; i++){

            start = vec3To2(imagePointList[i]);
            end = vec3To2(GetComponent<Transform>().position);

            if(lineClipper.judgeImageClipping(start, end, convList, halfRoomSize, i)){

                distance = getDistance(imagePointList[i]);
                attenuation = getAttenuation(distance);
                delayIndex = getDelayIndex(distance);

                for(int j = 0; j < clipBuffer.Length; j++){
  
                    if(j < blockSize){
                        clipBuffer[j] += originalClipList[listIndex][(sampleLength + timeSamples + j -marginIndex - delayIndex)%sampleLength] * attenuation * HRTFAttenuation * popopo;
                    }
                }

            }
        }
    }

    public void output(List<GameObject> convList, List<GameObject> soundList){

        this.convList = convList;
        timeSamples = audioSource.timeSamples;
        float[] outputClip = new float[sampleLength];

        dt_a = DateTime.Now;

        HRTFAttenuation = getHRTFAttenuation();
        popopo = getPower(convList.Count);
        
        listIndex = 0;
        for(int i=0; i<soundList.Count; i++){
            sound = soundList[i];
            soundPoint = sound.transform.position;
            calcDirect();
            calcReflect();
            listIndex++;
        }

        dt_n = DateTime.Now;
        a = dt_n - dt_a;

        //Debug.Log(dt_n.ToString() + " " + dt_a.ToString());
        //Debug.Log(a);
        
        for (int i = 0; i < clipBuffer.Length; i++){
            outputClip[(sampleLength + timeSamples + i -marginIndex)%sampleLength] = clipBuffer[i];
            clipBuffer[i] = 0.0f;
        }
        audioSource.clip.SetData(outputClip, loudNumber);
    }


    public void stop(GameObject sound){
        timeSamples = audioSource.timeSamples;
        float[] outputClip = new float[sampleLength];

        for (int i = 0; i < clipBuffer.Length; i++){
            outputClip[(sampleLength + timeSamples + i -marginIndex)%sampleLength] = 0;
        }
        audioSource.clip.SetData(outputClip, loudNumber);
    }
}
