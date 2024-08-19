using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionPrompt : MonoBehaviour
{
    public HitDetect parentMachine;
    [SerializeField] Transform worldSpaceAnchor;
    private Camera mainCamera;
    [SerializeField] SpriteRenderer redCircle;

    void Start()
    {
        mainCamera = Camera.main;

        // Update text to reflect machine settings
        transform.GetComponentInChildren<TMP_Text>().SetText(parentMachine.key.ToString());
    }

    void Update()
    {
        if (parentMachine.activated && !parentMachine.menu.paused) {
            // Shrink red circle
            float circleScale = parentMachine.GetPercentageToNextBeat();
            redCircle.material.SetFloat("_Scale", circleScale);
        }

        // Match button position to machine
        Vector3 screenPos = mainCamera.WorldToScreenPoint(worldSpaceAnchor.position);
        transform.position = screenPos;
    }
}
