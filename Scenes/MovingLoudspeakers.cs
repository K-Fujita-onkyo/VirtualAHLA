using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLoudspeakers : MonoBehaviour {

    public float initAngle;
    public float initRadius = 3;
    private Vector3 position;
    private Vector3 velocity;
    private float radius;
    private float angle;

    private float outRoom;
    private float loudRoom;
    private float listenerRoom;

   private float span;
   private float delta;


    private float speed = 0.02f;
    // Start is called before the first frame update
    void Start(){
        
        radius = 0.02f;
        listenerRoom = 0.6f;
        span = 1.0f;
        delta = 1.0f;
        position = transform.position;
        outRoom = InputValue.roomSize/2.0f;
        loudRoom = outRoom / 2.0f;
        velocity = new Vector3(0.0f, 0.0f, 0.0f);
        Application.targetFrameRate = 30;
        angle = initAngle;
    }

    // Update is called once per frame
    void Update(){
        //circularMotion();
        brownianMotion();
    }

    void brownianMotion(){
        
        delta += Time.deltaTime;

        if(delta > span){
            angle = Random.Range(0.0f, 2.0f * Mathf.PI);
            if(Random.Range(0.0f, 1.0f) > 0.3f){
                velocity = new Vector3(radius * Mathf.Cos(angle) * Random.Range(0.5f, 2.0f), 0, radius * Mathf.Sin(angle) * Random.Range(0.5f, 2.0f));
            }else {
                velocity = new Vector3(0.0f, 0.0f, 0.0f);
            }
            delta = 0;
        }

        if(Vector3.Distance(position + velocity, new Vector3(0, 1, 0)) < listenerRoom){

            if(Mathf.Abs(position.x) <= listenerRoom){
                velocity.z = -velocity.z;
            }
            if(Mathf.Abs(position.z) <= listenerRoom){
                velocity.x = -velocity.x;
            }
        }


        if(Vector3.Distance(position + velocity, new Vector3(0, 1, 0)) >loudRoom){
            velocity = - velocity;
        }

        position += velocity;
        transform.position = position;
    }

    void circularMotion(){
        angle = (angle + speed) % (2.0f * Mathf.PI);
        position = new Vector3(initRadius * Mathf.Cos(angle), 1, initRadius * Mathf.Sin(angle));
        transform.position = position;
    }
}
