using UnityEngine;

public class UIControl : MonoBehaviour
{
    [SerializeField] GameObject menu;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menu.activeSelf) {
                menu.GetComponent<Menu>().Play();
            } else {
                menu.SetActive(true);
            }
        }
    }
}
