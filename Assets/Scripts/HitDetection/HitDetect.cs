using UnityEngine;

public class HitDetect : MonoBehaviour
{
    [SerializeField]
    private SoundManager soundManager;

    [SerializeField] AudioSource hitConfirmSoundEffect;
    [SerializeField] public KeyCode key;

    void Update()
    {
        //Test hit detection. For every second beat, if you time it properly, a sound effect will play. (The hitConfirmSoundEffect variable.
        if (Input.GetKeyDown(key) && (soundManager.songPositionInBeats % 2 > 1.8f || soundManager.songPositionInBeats % 2 < 0.2f))
        {
            hitConfirmSoundEffect.Play();
        }
    }
}
