using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineClipping : MonoBehaviour {
    
    public int convexNum;
    private float slope;
    private float intercept;
    private int flag;

    public LineClipping(){
    }

    private void calcSlope(Vector2 start, Vector2 end){
        slope = (end.y - start.y) / (end.x - start.x);
    }

    private void calcIntercept(Vector2 point){
        intercept = point.y - (slope * point.x);
    }
    
    private void initLinearFunction(Vector2 start, Vector2 end){
        calcSlope(start, end);
        calcIntercept(start);
    }

    private float calcLinearFunction(float x){
        return (slope * x) + intercept;
    }

    private void initField(int convexNum){
        this.convexNum = convexNum;
        flag = 0;
    }


    public bool judgeClipping(Vector2 start, Vector2 end, ConvexHullCalculator convexList){

        int num;
        float valueX;
        float valueY;
        Vector3 convexPoint;


        initLinearFunction(start, end);
        initField(convexList.convexHullList.Count);

        num = 0;

        for(int i=0; i < convexNum; i++){

            convexPoint = convexList.convexHullList[i].transform.position;
            valueX = convexPoint.x;

            if(valueX < end.x){

                valueY = calcLinearFunction(valueX);
                if(valueY > convexPoint.z){
                    flag++;
                }
                num++;
            }
        }

        if(flag == 0 || flag == num) return true;

        return false;
        
    }

    private Vector2 vec3To2(Vector3 vec3){
        return new Vector2(vec3.x, vec3.z);
    }

    private Vector2 calcMoveVec2(Vector2 point, float wallSize, int wallNumber){

        Vector2 moveVec;

        switch(wallNumber){

            case 0:
                moveVec = new Vector2((wallSize - point.x)*2.0f, 0.0f);
                break;
            case 1:
                moveVec = new Vector2((-wallSize - point.x)*2.0f, 0.0f);
                break;
            case 2:
                moveVec = new Vector2(0.0f, (wallSize - point.y)*2.0f);
                break;
            case 3:
                moveVec = new Vector2(0.0f, (-wallSize - point.y)*2.0f);
                break;
            default:
                moveVec = new Vector2(0, 0);
                break;
        }

        return moveVec;
    }

    private Vector2 calcRotateVec2(Vector2 point, int wallNumber){

        Vector2 moveVec;

        switch(wallNumber){

            case 0:
                moveVec = new Vector2(-point.x, point.y);
                break;
            case 1:
                moveVec = point;
                break;
            case 2:
                moveVec = new Vector2(-point.y, point.x);
                break;
            case 3:
                moveVec = new Vector2(point.y, -point.x);
                break;
            default:
                moveVec = point;
                break;
        }

        return moveVec;
    }



    public bool judgeImageClipping(Vector2 start, Vector2 end, List<GameObject> convexList, float wallSize, int wallNumber){

        int num;
        int flag;
        Vector2 moveVec;
        Vector2 convexPoint;
        Vector2 trStartPoint;
        Vector2 trEndPoint;
        Vector2 trConvPoint;

        initField(convexList.Count);
        num = 0;
        flag = 0;

        for(int i=0; i< convexNum; i++){

            convexPoint = vec3To2(convexList[i].transform.position);
            moveVec = calcMoveVec2(convexPoint, wallSize, wallNumber);
            
            trStartPoint = calcRotateVec2(start, wallNumber);
            trEndPoint = calcRotateVec2(end, wallNumber);
            trConvPoint = calcRotateVec2(convexPoint, wallNumber);

            initLinearFunction(trStartPoint, trEndPoint);

            if(trConvPoint.x < end.x){

                if(calcLinearFunction(trConvPoint.x) > trConvPoint.y){
                    flag++;
                }

                num++;

            }


            trConvPoint = calcRotateVec2(convexPoint + moveVec, wallNumber);

            if(trConvPoint.x < end.x){

                if(calcLinearFunction(trConvPoint.x) > trConvPoint.y){
                    flag++;
                }

                num++;

            }
        }

        if(flag == 0 || flag == num) return true;

        return false;
    }
}