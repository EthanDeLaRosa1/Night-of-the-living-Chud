using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int pagesCollected = 0;
    public int totalPages = 6;

    public TextMeshProUGUI pickupMessageText;

    public GameObject inspectionPanel;
    public Image inspectionPhoto;

    public GameObject pausePanel;
    public GameObject winPanel;
    public GameObject gameOverPanel;
    public GameObject enemy;

    private bool inspecting = false;
    private bool isPaused = false;
    private bool gameEnded = false;

    private void Awake()
    {
        instance = this;
        Time.timeScale = 1f;

        pickupMessageText.text = "";
        inspectionPanel.SetActive(false);
        pausePanel.SetActive(false);
        winPanel.SetActive(false);
        gameOverPanel.SetActive(false);

        LockMouse();
    }

    private void Update()
    {
        if (gameEnded) return;

        if (inspecting && Input.GetKeyDown(KeyCode.E))
        {
            CloseInspection();
            return;
        }

        if (!inspecting && Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) ResumeGame();
            else PauseGame();
        }
    }

    public void CollectPage(int pageNumber, Sprite photo)
    {
        if (gameEnded) return;

        pagesCollected++;
        if (pagesCollected == 1 && enemy != null)
        {
            enemy.SetActive(true);
        }

        StartCoroutine(ShowPickupMessage("Photo " + pagesCollected + "/" + totalPages + " has been obtained"));
        OpenInspection(photo);

        if (pagesCollected >= totalPages)
        {
            StartCoroutine(WinAfterInspection());
        }
    }

    private IEnumerator ShowPickupMessage(string message)
    {
        pickupMessageText.text = message;
        yield return new WaitForSecondsRealtime(2f);
        pickupMessageText.text = "";
    }

    private IEnumerator WinAfterInspection()
    {
        yield return new WaitUntil(() => inspecting == false);
        WinGame();
    }

    private void OpenInspection(Sprite photo)
    {
        inspecting = true;
        inspectionPhoto.sprite = photo;
        inspectionPanel.SetActive(true);

        Time.timeScale = 0f;
        UnlockMouse();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void CloseInspection()
    {
        inspecting = false;
        inspectionPanel.SetActive(false);

        Time.timeScale = 1f;
        LockMouse();
    }

    public void PauseGame()
    {
        isPaused = true;
        pausePanel.SetActive(true);

        Time.timeScale = 0f;
        UnlockMouse();
    }

    public void ResumeGame()
    {
        isPaused = false;
        pausePanel.SetActive(false);

        Time.timeScale = 1f;
        LockMouse();
    }

    public void WinGame()
    {
        gameEnded = true;
        winPanel.SetActive(true);

        Time.timeScale = 0f;
        UnlockMouse();
    }

    public void GameOver()
    {
        gameEnded = true;
        gameOverPanel.SetActive(true);

        Time.timeScale = 0f;
        UnlockMouse();
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level1");
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    private void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}