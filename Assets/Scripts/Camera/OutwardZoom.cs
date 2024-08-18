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
    [Range(0.01f, 1f)][SerializeField] float shrinkSpeed;

    [Range(0.05f, 1f)][SerializeField] private float zoomOutStrength;


    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        StartCoroutine(nameof(CameraShrinkage));
    }

    // Update is called once per frame
    void Update()
    {
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, 1, 20);
    }

    private IEnumerator CameraShrinkage()
    {
        while (true)
        {

            cam.orthographicSize -= Time.deltaTime * Time.deltaTime * soundManager.songBpm * cam.orthographicSize * shrinkSpeed;
            yield return null;
        }
    }

    public void ZoomOut()
    {
        cam.orthographicSize += zoomOutStrength;
    }
}
