using System;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] OutwardZoom zoomManager;
    [SerializeField] SoundManager soundManager;
    [SerializeField] GameObject ship;
    [SerializeField] HitDetect firstMachine;
    [HideInInspector] public bool paused = true;

    public void Start() {
        ship.SetActive(false); // done in code in case we forget in the editor
    }

    public void Play() {
        ship.SetActive(true); // Show the ship
        paused = false; // control camera zoom, red circle closing, key prompts
        zoomManager.CheckObjects(); // activate machines in camera zone
        soundManager.PlayMusicTracks(); // start music
        gameObject.SetActive(false); // hide the menu
    }

    public void OnEnable() {
        Pause();
    }

    internal void Pause()
    {
        paused = true;
    }

    public void Quit() {
        // Doesn't get added to the built program
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif

        // Only works when the program is built
        Application.Quit();
    }
}
