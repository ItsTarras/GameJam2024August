using TMPro;
using UnityEngine;

public class InteractionPrompt : MonoBehaviour
{
    public HitDetect parentMachine;
    private Camera MainCamera;
    [SerializeField] float buttonHeight;

    void Start()
    {
        MainCamera = Camera.main;

        // Update text to reflect machine settings
        transform.GetComponentInChildren<TMP_Text>().SetText(parentMachine.key.ToString());
    }

    void Update()
    {
        // Match button position to machine
        // (Doesn't work in start for some reason)
        Vector3 screenPos = MainCamera.WorldToScreenPoint(parentMachine.transform.position);
        Vector3 offset = new(0, buttonHeight, 0);
        transform.position = screenPos + offset;
    }
}
