using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutwardZoom : MonoBehaviour
{
    //Get the sound manager. We need to get the bpm so we can move the camera out slowly.
    public SoundManager soundManager;

    //Get all the game objects that we will be trying to view within the camera viewport. If they are within the camera viewport, activate it using its public method (should get one).
    public List<GameObject> machines = new List<GameObject>();


    private Camera cam;



    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        StartCoroutine("cameraShrinkage");
    }

    // Update is called once per frame
    void Update()
    {
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, 1, 20);
    }

    private IEnumerator cameraShrinkage()
    {
        while (true)
        {
            
            cam.orthographicSize -= Time.deltaTime * Time.deltaTime * soundManager.songBpm * cam.orthographicSize / 3;
            yield return null;
        }
    }

    public void zoomOut(float value)
    {
        cam.orthographicSize += value;
    }
}
