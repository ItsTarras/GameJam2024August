using System;
using UnityEngine;

public class HitDetect : MonoBehaviour
{
    private SoundManager soundManager;
    public Menu menu;
    [SerializeField] AudioSource hitConfirmSoundEffect;
    [SerializeField] AudioSource beatMissedSoundEffect;
    [SerializeField] public AudioSource soundtrackContribution;
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
    private bool supressPlayingError = false;
    public bool activated = false;

    private Animator animator;

    private void Start()
    {
        cameraToZoomOutOnHit = Camera.main.GetComponent<OutwardZoom>();
        // Technically bad practice to assume singletons like this but I'm not assigning for every single machine
        soundManager = FindAnyObjectByType<SoundManager>();
    }

    void Update()
    {
        // Toggle interaction prompt based on pause status
        keyToHitImage.GetComponent<InteractionPrompt>().anchor.SetActive(!menu.paused);
        keyToHitImage.SetActive(!menu.paused);

        if(activated && !menu.paused)
        {
            // For every `cooldown` beat, if you time it properly,
            // a sound effect will play. (The hitConfirmSoundEffect variable.)
            if (soundManager.songPositionInBeats % targetBeatToHit > targetBeatToHit - forgiveness ||
                soundManager.songPositionInBeats % targetBeatToHit < forgiveness)
            {
                // We're inside the window to hit the beat

                //Reset the error variable at the start of this new cycle.
                supressPlayingError = false;

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
            } else {
                // We're outside the window to hit the beat
                #region detect missed beats
                if (!hitThisRound)
                {
                    if (!supressPlayingError)
                    {
                        //We failed to hit the beat.
                        //THIS AREA IS WHERE WE WANT TO ALTER WHAT HAPPENS WHEN WE MISS A BEAT! Maybe reference the camera, and zoom it in slightly?
                        beatMissedSoundEffect.Play();
                        soundtrackContribution.mute = true;
                        if (TryGetComponent<Animator>(out animator)) {
                            animator.speed = 0;
                        }
                        supressPlayingError = true;
                    }
                } else {
                    //We hit the beat this round, so don't play the error. Just set it to true so it doesn't play in this cycle.
                    //Do not play the error sound.
                    supressPlayingError = true;
                    soundtrackContribution.mute = false;
                    if (TryGetComponent<Animator>(out animator)) {
                        print($"starting animation for {gameObject.name}");
                        animator.speed = 1;
                    }
                    //Reset the variables.
                    hitThisRound = false;
                }
                #endregion
            }
        }
    }

    internal float GetPercentageToNextBeat()
    {
        float beatsToNextBeat = targetBeatToHit - soundManager.songPositionInBeats % targetBeatToHit;
        return beatsToNextBeat / targetBeatToHit;
    }

    public void Activate() {
        activated = true;
        soundtrackContribution.mute = false;
    }
}
