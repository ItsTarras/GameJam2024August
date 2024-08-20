using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutwardZoom : MonoBehaviour
{
    //Get the sound manager. We need to get the bpm so we can move the camera out slowly.
    public SoundManager soundManager;

    //Get all the game objects that we will be trying to view within the camera viewport. If they are within the camera viewport, activate it using its public method (should get one).
    public List<HitDetect> machines = new List<HitDetect>();

    private Camera cam;
    [SerializeField] Menu menu;
    [Range(0f, 1f)][SerializeField] float shrinkSpeed;
    [Range(0.5f, 5f)][SerializeField] float minZoom;
    [Range(5f, 100f)][SerializeField] float maxZoom;

    private int numberMachinesActivated = 1;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!menu.paused) {
            cam.orthographicSize -= Time.deltaTime * Time.deltaTime * soundManager.songBpm * cam.orthographicSize * shrinkSpeed;
            cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, minZoom, maxZoom);
        }
    }

    public void ZoomOut(float zoomOutStrength)
    {
        //Instead of moving the camera out a flat value, have it only consistently grow if you can hit all the keys properly.
        // Divide the strength by the number of currently activated machines.
        //Divide by 4 beats per bar. Don't want to hinder the growth by having a lot of machines
        // that arent pressed often enough to keep growing.

        float modifiedDelta = zoomOutStrength / (numberMachinesActivated * 0.25f);
        // print($"Zooming out by {modifiedDelta}");
        cam.orthographicSize += Mathf.Clamp(modifiedDelta, 0.1f, 0.5f); 
        CheckObjects();
    }

    public void CheckObjects()
    {
        #region Check what objects are within the camera frustum, and if they need to have their keys activated.
        foreach(HitDetect machine in machines)
        {
            if (CameraExtensions.IsObjectVisible(cam, machine.transform.position, -0.05f) && !machine.activated)
            {
                //Set the machine to be active.
                machine.Activate();
                numberMachinesActivated++;
            }
        }
        #endregion
    }
}
