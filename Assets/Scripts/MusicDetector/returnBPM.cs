using UnityEngine;

public class returnBPM : MonoBehaviour
{
    public AudioClip targetClip;
    private float bpm = 0;

    void Start()
    {
        bpm = UniBpmAnalyzer.AnalyzeBpm(targetClip);
        // Debug.Log("BPM of song: " + bpm);
    }

    public float getBPM()
    {
        return bpm;
    }
}

