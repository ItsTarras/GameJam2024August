using UnityEngine;

public class UIControl : MonoBehaviour
{
    [SerializeField] GameObject menu;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(!menu.activeSelf);
        }
    }
}
