using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangingFlont : MonoBehaviour {

    private Transform myPos;
    private Vector3 taPos =  new Vector3(0.0f, 1.0f, 0.0f);
    // 前方の基準となるローカル空間ベクトル
    [SerializeField] private Vector3 forwardVector = Vector3.forward;

    void Start(){
        myPos = GetComponent<Transform>();
    }
    

    void Update(){
        // ターゲットへの向きベクトル計算
        var dir =  taPos - myPos.position;

        // ターゲットの方向への回転
        var lookAtRotation = Quaternion.LookRotation(dir, Vector3.up);
        // 回転補正
        var offsetRotation = Quaternion.FromToRotation(forwardVector, Vector3.forward);

        // 回転補正→ターゲット方向への回転の順に、自身の向きを操作する
        myPos.rotation = lookAtRotation * offsetRotation;
    }
}
