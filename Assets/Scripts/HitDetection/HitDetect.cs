using System;
using UnityEngine;
using UnityEngine.UI;

public class HitDetect : MonoBehaviour
{
    private SoundManager soundManager;

    [SerializeField] AudioSource hitConfirmSoundEffect;
    [SerializeField] AudioSource beatMissedSoundEffect;
    [SerializeField] public KeyCode key;
    OutwardZoom cameraToZoomOutOnHit;
    [Range(0.05f, 1f)][SerializeField] private float zoomOutStrength;

    // Beat matching config
    public int targetBeatToHit; // hit the key every x beats

    // radius around the exact timing where your input still counts
    // (eg within forgiveness beats before or after perfect still counts)
    [SerializeField] float forgiveness;

    //This is the original scale size we want to return to.
    public float targetScale;

    //Key image.
    [SerializeField] private GameObject keyToHitImage;


    //Boolean declarations.
    private bool hitThisRound = true; // For the initial beat, as to not startle the player upon activation.
    private bool playedError = false;
    public bool activated = false;

    private void Start()
    {
        cameraToZoomOutOnHit = Camera.main.GetComponent<OutwardZoom>();
        soundManager = FindAnyObjectByType<SoundManager>();
    }

    void Update()
    {
        if(activated)
        {
            // For every `cooldown` beat, if you time it properly,
            // a sound effect will play. (The hitConfirmSoundEffect variable.)
            if (soundManager.songPositionInBeats % targetBeatToHit > targetBeatToHit - forgiveness ||
                soundManager.songPositionInBeats % targetBeatToHit < forgiveness)
            {
                //Reset the error variable at the start of this new cycle.
                playedError = false;

                #region detect the beat hitting
                //They managed to hit the beat.
                if (Input.GetKeyDown(key) && hitThisRound == false)
                {
                    hitConfirmSoundEffect.Play();
                    hitThisRound = true;

                    //Apply the camera zoom out effect if we hit this.

                    cameraToZoomOutOnHit.ZoomOut(zoomOutStrength);
                }
                #endregion

                #region animation
                //Animate the key that is being hit.
                if (soundManager.songPositionInBeats % targetBeatToHit >= targetBeatToHit - forgiveness)
                {
                    keyToHitImage.transform.localScale = Vector3.Lerp(keyToHitImage.transform.localScale, new Vector3(targetScale, targetScale, targetScale), 50f);
                }
                else
                {
                    keyToHitImage.transform.localScale = Vector3.Lerp(keyToHitImage.transform.localScale, new Vector3(1, 1, 1), 25f * forgiveness);
                }
                #endregion
            }

            //We've just passed the opportunity to hit the beat.
            else
            {
                #region detect missed beats
                //We failed to hit the beat.
                if (!hitThisRound)
                {
                    if (!playedError)
                    {
                        //THIS AREA IS WHERE WE WANT TO ALTER WHAT HAPPENS WHEN WE MISS A BEAT! Maybe reference the camera, and zoom it in slightly?
                        beatMissedSoundEffect.Play();
                        playedError = true;
                    }
                }
                else
                {
                    //We hit the beat this round, so don't play the error. Just set it to true so it doesn't play in this cycle.
                    //Do not play the error sound.
                    playedError = true;

                    //Reset the variables.
                    hitThisRound = false;
                }
                #endregion
            }
        }
    }

    internal float getPercentageToNextBeat()
    {
        float beatsToNextBeat = targetBeatToHit - soundManager.songPositionInBeats % targetBeatToHit;
        return beatsToNextBeat / targetBeatToHit;
    }
}
