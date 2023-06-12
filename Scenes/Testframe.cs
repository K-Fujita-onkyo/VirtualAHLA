using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testframe : MonoBehaviour
{

    int count = 0;
    public float mean = 0;
    int sum = 0;
    int c = 0;
    float s, f;
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        Debug.Log(Time.time); // 0秒時点の時間を表示。当然０が表示されるはず...
        s = Time.time;
        f = Time.time;
        
    }
 
    // Update is called once per frame
    void Update()
    {
        s = Time.time - f;
        count = count + 1;  // Updateが呼び出される毎にカウントアップする
        if (s > 1.0f) // 1秒を超えたら表示する
        {
            Debug.Log("FINISH!!!");
            Debug.Log("count = " + count);
            Debug.Log("Time.time = " + s);
            c++;
            sum += count;
            mean = (float)sum / c;
            Debug.Log("mean = " + mean);
            count = 0;
            f = Time.time;
            s = 0;
        }
 
    }
}
