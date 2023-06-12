using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestSound : MonoBehaviour
{
    private float testRadius = 4.0f;
    private float testAngle = 0.0f;
    public GameObject text;
    // Start is called before the first frame update
    public void Click() {
        testAngle = (testAngle+30) % 360;
        float theta = Mathf.PI * ((360 - testAngle) / 180);
        SoundGenelator.soundList[0].transform.position = new Vector3(testRadius * Mathf.Cos(theta), 1, testRadius * Mathf.Sin(theta));
        text.GetComponent<Text>().text = testAngle + "°";
    }
}
