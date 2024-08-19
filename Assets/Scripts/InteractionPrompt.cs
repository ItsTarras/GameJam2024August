using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionPrompt : MonoBehaviour
{
    public HitDetect parentMachine;
    public GameObject anchor;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;

        // Update text to reflect machine settings
        transform.GetComponentInChildren<TMP_Text>().SetText(parentMachine.key.ToString());
    }

    void Update()
    {
        if (parentMachine.activated) {
            // Shrink red circle
            float circleScale = parentMachine.GetPercentageToNextBeat();
            anchor.GetComponent<SpriteRenderer>().material.SetFloat("_Scale", circleScale);
        }

        // Match button position to machine
        Vector3 screenPos = mainCamera.WorldToScreenPoint(anchor.transform.position);
        transform.position = screenPos;
    }
}
