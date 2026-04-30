using UnityEngine;

public class SignInspect : MonoBehaviour
{
    public GameObject signPanel;
    private bool signOpen = false;

    void Update()
    {
        if (signOpen && Input.GetKeyDown(KeyCode.E))
        {
            CloseSign();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OpenSign();
        }
    }

    private void OpenSign()
    {
        signOpen = true;
        signPanel.SetActive(true);

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void CloseSign()
    {
        signOpen = false;
        signPanel.SetActive(false);

        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}