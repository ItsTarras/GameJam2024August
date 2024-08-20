using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutwardZoom : MonoBehaviour
{
    //Get the sound manager. We need to get the bpm so we can move the camera out slowly.
    public SoundManager soundManager;

    //Get all the game objects that we will be trying to view within the camera viewport. If they are within the camera viewport, activate it using its public method (should get one).
    public List<HitDetect> machines = new();

    private Camera cam;
    [SerializeField] Animator[] lights;
    [SerializeField] Menu menu;
    [SerializeField] GameObject credits;
    [SerializeField] float shrinkSpeed;
    [Range(0.5f, 50f)][SerializeField] float minZoom;
    [Range(0.5f, 50f)][SerializeField] float winRadius;

    private int numberMachinesActivated = 1;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!menu.paused) {
            cam.orthographicSize *= shrinkSpeed;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, winRadius + 10f);
        }
    }

    public void ZoomOut(float zoomOutStrength)
    {
        //Instead of moving the camera out a flat value, have it only consistently grow if you can hit all the keys properly.
        // Divide the strength by the number of currently activated machines.
        //Divide by 4 beats per bar. Don't want to hinder the growth by having a lot of machines
        // that arent pressed often enough to keep growing.

        float modifiedDelta = zoomOutStrength / numberMachinesActivated;
        // print($"Zooming out by {modifiedDelta}");
        cam.orthographicSize += modifiedDelta; 
        CheckObjects();
        CheckWinCondition();
    }

    private void CheckWinCondition()
    {
        if (cam.orthographicSize > winRadius) {
            menu.paused = true;
            foreach(HitDetect machine in machines)
            {
                machine.activated = false;
            }
            credits.SetActive(true);
            foreach(Animator light in lights) {
                light.Play("Base Layer.light"); // emerge
            }
        }
    }

    public void CheckObjects()
    {
        #region Check what objects are within the camera frustum, and if they need to have their keys activated.
        foreach(HitDetect machine in machines)
        {
            if (Vector2.Distance(transform.position, machine.keyToHitImage.GetComponent<InteractionPrompt>().anchor.transform.position) < cam.orthographicSize && !machine.activated)
            {
                //Set the machine to be active.
                machine.Activate();
                numberMachinesActivated++;
            }
        }
        #endregion
    }
}
