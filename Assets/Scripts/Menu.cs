using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject ship;
    [SerializeField] HitDetect firstMachine;

    public void Play() {
        // Start the game and hide the menu
        ship.SetActive(true);
        firstMachine.activated = true;
        gameObject.SetActive(false);
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
