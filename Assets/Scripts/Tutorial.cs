using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] HitDetect firstMachine;
    [SerializeField] OutwardZoom zoomManager;
    [SerializeField] Menu menu;
    [SerializeField] TMP_Text text;
    [SerializeField] SoundManager soundManager;
    void Start() {
        text.SetText(firstMachine.key.ToString());
    }

    void Update() {
        if (Input.GetKeyDown(firstMachine.key))
            {
                firstMachine.hitThisRound = true;

                EndTutorial();
            }
    }

    private void EndTutorial() {
        menu.paused = false; // control camera zoom, red circle closing, key prompts
        zoomManager.CheckObjects(); // activate machines in camera zone and
        soundManager.PlayMusicTracks(); // start game music
        gameObject.SetActive(false); // hide tutorial
    }
}
