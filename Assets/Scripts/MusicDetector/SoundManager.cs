using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public GameObject musicManager;

    //Each object that has an audio source attached to it. Whenever we complete something, turn it up?
    public List<AudioSource> audioSources = new List<AudioSource>();

    //The offset to the first beat of the song in seconds
    [SerializeField]
    float firstBeatOffset;

    //Song beats per minute
    //This is determined by the song you're trying to sync up to
    public float songBpm;

    //Current song position, in beats
    public float songPositionInBeats;

    //The number of seconds for each song beat
    float secPerBeat;

    //Current song position, in seconds
    float songPosition;

    //How many seconds have passed since the song started
    float dspSongTime = 0;

    private bool startBpmCounter = false;

    // Start is called before the first frame update
    void Start()
    {
        //Gets the bpm of a song that is placed inside the returnBPM component of an object. Only if we have it set to 0.
        if(songBpm == 0)
        {
            songBpm = musicManager.GetComponent<returnBPM>().getBPM();
        }

        //Calculate the number of seconds in each beat
        secPerBeat = 60f / songBpm;
    }

    private void Awake()
    {
        //Gets the bpm of a song that is placed inside the returnBPM component of an object. Only if we have it set to 0.
        if (songBpm == 0)
        {
            songBpm = musicManager.GetComponent<returnBPM>().getBPM();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (startBpmCounter)
        {
            //determine how many seconds since the song started
            songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);

            //determine how many beats since the song started
            songPositionInBeats = songPosition / secPerBeat;
        }
    }

    public void PlayMusicTracks()
    {
        // BUG: Playing a slash sound instead of one of the audio tracks
        for (int i = 0; i < audioSources.Count; i++)
        {
            audioSources[i].Play();
        }
        StartMusicCounter();
    }

    private void StartMusicCounter()
    {
        //Record the time when the music starts
        dspSongTime = (float)AudioSettings.dspTime;

        startBpmCounter = true;
    }
}
