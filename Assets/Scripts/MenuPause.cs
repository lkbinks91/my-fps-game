using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;
    public Camera mainCamera;

    private CursorLockMode previousLockState;
    private bool previousCursorVisibility;
    public 

    void Start()
    {
        pauseMenuUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        previousLockState = Cursor.lockState;
        previousCursorVisibility = Cursor.visible;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = previousLockState;
            Cursor.visible = previousCursorVisibility;
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        mainCamera.enabled = true;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        mainCamera.enabled = false;
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        mainCamera.enabled = false;
        SceneManager.LoadScene("MainMenu");
  
    }

    public void FermerPauseMenu()
    {
        Resume();
    }
}
