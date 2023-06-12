using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipGetter : MonoBehaviour {
    // Start is called before the first frame update
    public AudioClip audioClip1;
    public AudioClip audioClip2;
    public AudioClip audioClip3;
    public AudioClip audioClip4;
    public float[][] audioClipArray;
    private float[] clipArray;

    void Awake(){
        
        audioClipArray = new float[100][];
        for(int i=0; i<100; i++){
            audioClipArray[i] = new float[audioClip1.samples];
        }
 
        audioClip1.GetData(audioClipArray[0], 0);
        audioClip2.GetData(audioClipArray[1], 0);
        audioClip3.GetData(audioClipArray[2], 0);
        audioClip4.GetData(audioClipArray[3], 0);

        clipArray = new float[audioClip1.samples];
    }
    public float[] getAudioClip(int index){

        switch(index){
            case 0:
                audioClip1.GetData(clipArray, 0);
                break;
            case 1:
                audioClip2.GetData(clipArray, 0);
                break;
            case 2:
                audioClip3.GetData(clipArray, 0);
                break;
            case 3:
                audioClip4.GetData(clipArray, 0);
                break;
            default:
                audioClip1.GetData(clipArray, 0);
                break;

        }
        return clipArray;
    }
    public float[][] getAudioClip(){
        return audioClipArray;
    }
}
