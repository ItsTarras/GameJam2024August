using System;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject ship;
    [SerializeField] GameObject tutorial;
    [HideInInspector] public bool paused = true;

    public void Start() {
        ship.SetActive(false); // done in code in case we forget in the editor
    }

    public void Play() {
        ship.SetActive(true); // Show the ship
        tutorial.SetActive(true); // and the tutorial
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
