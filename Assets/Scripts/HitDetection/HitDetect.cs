using UnityEngine;

public class HitDetect : MonoBehaviour
{
    [SerializeField]
    private SoundManager soundManager;

    [SerializeField] AudioSource hitConfirmSoundEffect;
    [SerializeField] public KeyCode key;

    // Beat matching config
    [SerializeField] int cooldown; // hit the key every x beats

    // radius around the exact timing where your input still counts
    // (eg within forgiveness beats before or after perfect still counts)
    [SerializeField] float forgiveness;


    void Update()
    {
        // For every `cooldown` beat, if you time it properly,
        // a sound effect will play. (The hitConfirmSoundEffect variable.)
        if (Input.GetKeyDown(key) && (
            soundManager.songPositionInBeats % cooldown > cooldown - forgiveness ||
            soundManager.songPositionInBeats % cooldown < forgiveness
        ))
        {
            hitConfirmSoundEffect.Play();
        }
    }
}
