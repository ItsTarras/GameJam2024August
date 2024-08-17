using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDetect : MonoBehaviour
{
    [SerializeField]
    private SoundManager soundManager;

    [SerializeField] AudioSource hitConfirmSoundEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Test hit detection. For every second beat, if you time it properly, a sound effect will play. (The hitConfirmSoundEffect variable.
        if (Input.GetKeyDown(KeyCode.Space) && (soundManager.songPositionInBeats % 2 > 1.8f || soundManager.songPositionInBeats % 2 < 0.2f))
        {
            hitConfirmSoundEffect.Play();
        }
    }
}
