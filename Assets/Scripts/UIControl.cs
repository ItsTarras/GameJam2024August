using UnityEngine;

public class UIControl : MonoBehaviour
{
    [SerializeField] GameObject menu;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menu.activeSelf) {
                // BUG: Play() needs to be called through the button press, not the
                // keyboard press, the first time it runs.
                menu.GetComponent<Menu>().Play();
            } else {
                menu.SetActive(true);
            }
        }
    }
}
