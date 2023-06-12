using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
public class InputValue : MonoBehaviour
{

    public static int roomSize;
    public static int loudspeakerNum;
    public static int soundNum;
    public static float absorptionValue;

    public GameObject inputLoudspeakerNum;
    public GameObject resultLoudspeakerNum;
    public GameObject inputRoomSize;
    public GameObject resultRoomSize;
    public GameObject inputSoundNum;
    public GameObject resultSoundNum;
    [SerializeField] public Dropdown dropdown;
    public GameObject resultAbsorptionNum;
    [DllImport("hello")] public static extern int add_function(int a, int b);


      //Dropdownを格納する変数

    void Start(){
        roomSize = 10;
        loudspeakerNum = 1;
        soundNum = 10;
        absorptionValue = 0.5f;
        Debug.Log(add_function(1, 1));
    }

    public void getLoudspeakerNum(){
        int input = (int)inputLoudspeakerNum.GetComponent<Slider>().value;
        resultLoudspeakerNum.GetComponent<Text>().text = input.ToString();
        loudspeakerNum = input;
    }

    public void getRoomSize(){
        
        int input = (int)inputRoomSize.GetComponent<Slider>().value;
        resultRoomSize.GetComponent<Text>().text = input.ToString() + "m";
        roomSize = input;
    }

    public void getSoundNum(){
        int input = (int)inputSoundNum.GetComponent<Slider>().value;
        resultSoundNum.GetComponent<Text>().text = input.ToString();
        soundNum = input;
    }

    public void getAbsorptionValue(){

        switch(dropdown.value){
            case 0:
                absorptionValue = 0.5f;
                break;
            case 1:
                absorptionValue = 0.65f;
                break;
            case 2:
                absorptionValue = 0.015f;
                break;
            case 3:
                absorptionValue = 0.35f;
                break;
            case 4:
                absorptionValue = 0.3f;
                break;
            case 5:
                absorptionValue = 0.02f;
                break;
            case 6:
                absorptionValue = 0.15f;
                break;
            default:
                absorptionValue = 0.0f;
                break;
        }

        resultAbsorptionNum.GetComponent<Text>().text = "α = " + absorptionValue;

    }
}
