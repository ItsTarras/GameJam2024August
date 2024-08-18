using TMPro;
using UnityEngine;

public class InteractionPrompt : MonoBehaviour
{
    public HitDetect parentMachine;
    [SerializeField] Transform worldSpaceAnchor;
    private Camera MainCamera;
    [SerializeField] SpriteRenderer redCircle;
    private float circleScale = 1f;

    void Start()
    {
        MainCamera = Camera.main;

        // Update text to reflect machine settings
        transform.GetComponentInChildren<TMP_Text>().SetText(parentMachine.key.ToString());
    }

    void Update()
    {
        // Shrink red circle
        redCircle.material.SetFloat("_Scale", Mathf.PingPong(Time.time, circleScale));

        // Match button position to machine
        Vector3 screenPos = MainCamera.WorldToScreenPoint(worldSpaceAnchor.position);
        transform.position = screenPos;
    }
}
