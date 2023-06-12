using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoudspeakerState : MonoBehaviour
{
    public AudioSource audioSource;
    public Vector3 position;
    public bool convexHullOrNot;
    public int number;

    private AudioClip initClip;

    void Start(){
        audioSource = GetComponent<AudioSource>();
        convexHullOrNot = false;
    }
}
