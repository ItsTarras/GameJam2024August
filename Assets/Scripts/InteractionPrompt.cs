using TMPro;
using UnityEngine;

public class InteractionPrompt : MonoBehaviour
{
    public HitDetect parentMachine;
    public GameObject anchor;
    private Camera mainCamera;
    [SerializeField] Transform ring;
    float ringSize;

    void Start()
    {
        mainCamera = Camera.main;

        // Update text to reflect machine settings
        transform.GetComponentInChildren<TMP_Text>().SetText(parentMachine.key.ToString());
        ringSize = ring.localScale.x;
    }

    void Update()
    {
        if (parentMachine.activated) {
            // Shrink red circle
            float circleScale = parentMachine.GetPercentageToNextBeat() * ringSize;
            ring.localScale = new Vector3(circleScale, circleScale, circleScale);
        }

        // Match button position to machine
        Vector3 screenPos = mainCamera.WorldToScreenPoint(anchor.transform.position);
        transform.position = screenPos;
    }
}
