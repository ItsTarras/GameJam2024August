using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class returnBPM : MonoBehaviour
{

    public AudioClip targetClip;
    private float bpm = 0;


    // Start is called before the first frame update
    void Start()
    {
        bpm = UniBpmAnalyzer.AnalyzeBpm(targetClip);
        // Debug.Log("BPM of song: " + bpm);
    }

    public float getBPM()
    {
        return bpm;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

