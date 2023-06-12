using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingSound : MonoBehaviour {
    
    private Vector3 position;
    private Vector3 velocity;

    private float radius;
    private float angle;
    private float testRadius = 4.0f;
    private float testAngle = 0.0f;
    public GameObject text;

    private float outRoom;
    private float loudRoom;
    private float listenerRoom;

   private float span;
   private float delta;

   public 

    // Start is called before the first frame update
    void Start(){
        radius = 0.05f;
        span = 1.0f;
        delta = 1.0f;

        outRoom = InputValue.roomSize/2.0f;
        loudRoom = outRoom/2.0f;

        position = transform.position;
        velocity = new Vector3(0.0f, 0.0f, 0.0f);
        Application.targetFrameRate = 30; 
    }

    // // Update is called once per frame
    void Update(){
        
        delta += Time.deltaTime;



        if(delta > span){
            angle = Random.Range(0.0f, 2.0f * Mathf.PI);
            if(Random.Range(0.0f, 1.0f) > 0.0f){
                velocity = new Vector3(radius * Mathf.Cos(angle), 0, radius * Mathf.Sin(angle));
            }else {
                velocity = new Vector3(0.0f, 0.0f, 0.0f);
            }
            delta = 0;
        }

        if(Vector3.Distance(position + velocity, new Vector3(0, 1, 0)) < loudRoom){

            if(Mathf.Abs(position.x) <= loudRoom){
                velocity.z = -velocity.z;
            }
            if(Mathf.Abs(position.z) <= loudRoom){
                velocity.x = -velocity.x;
            }
        }

        if(Mathf.Abs((position+velocity).x)>outRoom){
            velocity.x = -velocity.x;
        }

        if(Mathf.Abs((position+velocity).z)>outRoom){
            velocity.z = -velocity.z;
        }



        position += velocity;
        transform.position = position;
    }

    public void Click() {
        testAngle = (testAngle+30) % 360;
        float theta = Mathf.PI * (testAngle / 360);
        transform.position = new Vector3(testRadius * Mathf.Cos(theta), 1, testRadius * Mathf.Sin(theta));
        text.GetComponent<Text>().text = testAngle + "°";
    }
}
