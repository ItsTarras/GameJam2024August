using System;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] OutwardZoom zoomManager;
    [SerializeField] GameObject ship;
    [SerializeField] HitDetect firstMachine;
    [HideInInspector] public bool paused = true;

    public void Play() {
        ship.SetActive(true); // Show the ship
        paused = false; // control camera zoom, red circle closing, key prompts
        firstMachine.activated = true; // activate the ship intelligence
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
