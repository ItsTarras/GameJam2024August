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
    [Range(0.05f, 5f)][SerializeField] private float zoomOutStrength;

    // Beat matching config
    [SerializeField] int firstBeat;
    [SerializeField] int beatsBetweenRepetions;

    // a tiny offset for if multiple machines hit the same beat and we want them played in sequence.etions;
    [SerializeField] float beatsOfDelay;

    // radius around the exact timing where your input still counts
    // (eg within forgiveness beats before or after perfect still counts)
    [SerializeField] float forgiveness;

    [SerializeField] float targetScale = 1.25f;
    private float normalScale;
    //Key image.
    [SerializeField] public GameObject keyToHitImage;


    //Boolean declarations.
    private bool hitThisRound = true; // For the initial beat, as to not startle the player upon activation.
    private bool supressPlayingError = false;
    public bool activated = false;

    private Animator animator;

    private void Start()
    {
        normalScale = keyToHitImage.transform.localScale.x;
        cameraToZoomOutOnHit = Camera.main.GetComponent<OutwardZoom>();
        // Technically bad practice to assume singletons like this but I'm not assigning for every single machine
        soundManager = FindAnyObjectByType<SoundManager>();
    }

    void Update()
    {
        if (!activated) return;

        keyToHitImage.GetComponent<InteractionPrompt>().anchor.SetActive(!menu.paused);
        keyToHitImage.SetActive(!menu.paused);

        if (menu.paused) return;

        // For every `cooldown` beat, if you time it properly,
        // a sound effect will play. (The hitConfirmSoundEffect variable.)
        bool beatCanBeHit = 
            GetNumBeatsToNextBeat() < forgiveness ||
            GetNumBeatsToNextBeat() > beatsBetweenRepetions - forgiveness;
        // print($"Beat can be hit: {beatCanBeHit}");

        if (beatCanBeHit)
        {
            // We're inside the window to hit the beat

            //Reset the error variable at the start of this new cycle.
            supressPlayingError = false;

            #region detect the beat hitting
            if (Input.GetKeyDown(key) && hitThisRound == false)
            {
                HitBeat();
            }
            #endregion

            #region animation
            //Animate the key that is being hit.
            // Name your magic numbers you neanderthal
            if (GetNumBeatsToNextBeat() < forgiveness)
            {
                // before mark
                keyToHitImage.transform.localScale = Vector3.Lerp(keyToHitImage.transform.localScale, new Vector3(targetScale, targetScale, targetScale), GetNumBeatsToNextBeat() / forgiveness);
                // keyToHitImage.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
            }
            else
            {
                // after mark
                keyToHitImage.transform.localScale = Vector3.Lerp(keyToHitImage.transform.localScale, new Vector3(normalScale, normalScale, normalScale), GetBeatsSinceLastBeat() / normalScale);
                // keyToHitImage.transform.localScale = new Vector3(1f, 1f, 1f);
            }
            #endregion
        } else {
            // We're outside the window to hit the beat
            #region detect missed beats
            if (!hitThisRound)
            {
                if (!supressPlayingError)
                {
                    MissBeat();
                }
            } else {
                //We hit the beat this round, so don't play the error. Just set it to true so it doesn't play in this cycle.
                //Do not play the error sound.
                supressPlayingError = true;
                soundtrackContribution.mute = false;
                if (TryGetComponent<Animator>(out animator)) {
                    // print($"starting animation for {gameObject.name}");
                    animator.speed = 1;
                }
                //Reset the variables.
                hitThisRound = false;
            }
            #endregion
        }
    }

    private void MissBeat()
    {
        //THIS AREA IS WHERE WE WANT TO ALTER WHAT HAPPENS WHEN WE MISS A BEAT! Maybe reference the camera, and zoom it in slightly?
        beatMissedSoundEffect.Play();
        soundtrackContribution.mute = true;
        if (TryGetComponent<Animator>(out animator))
        {
            animator.speed = 0;
        }
        supressPlayingError = true;
    }

    public void HitBeat()
    {
        hitConfirmSoundEffect.Play();
        hitThisRound = true;
        cameraToZoomOutOnHit.ZoomOut(zoomOutStrength);
    }

    internal float GetBeatsSinceLastBeat() {
        if (soundManager.songPositionInBeats < (firstBeat + beatsOfDelay)) {
            // Haven't gotten to first beat yet
            return soundManager.songPositionInBeats;
        }
        return (soundManager.songPositionInBeats - (firstBeat + beatsOfDelay)) % beatsBetweenRepetions;
    }

    internal float GetNumBeatsToNextBeat() {
        if (soundManager.songPositionInBeats < (firstBeat + beatsOfDelay)) {
            // Haven't gotten to first beat yet
            return firstBeat + beatsOfDelay - GetBeatsSinceLastBeat();
        }
        return beatsBetweenRepetions - GetBeatsSinceLastBeat();
    }

    internal float GetPercentageToNextBeat()
    {
        return GetNumBeatsToNextBeat() / beatsBetweenRepetions;
    }

    public void Activate() {
        activated = true;
        soundtrackContribution.mute = false;
    }
}
