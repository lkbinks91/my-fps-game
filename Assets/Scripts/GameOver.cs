using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverUI;
    public Button retryButton;
    public Ennemi ennemi;
    public bool isGameOverDisplayed = false;
    public TextMeshProUGUI tempsEcouleText;
    private float tempsEcoule;


    // Start is called before the first frame update
    void Start()
    {
        gameOverUI.SetActive(false);
        retryButton.onClick.AddListener(RestartMission);
        ennemi = GameObject.FindObjectOfType<Ennemi>();
        tempsEcouleText.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOverDisplayed)
        {
            tempsEcoule += Time.deltaTime;
            int minutes = Mathf.FloorToInt(tempsEcoule / 60);
            int secondes = Mathf.FloorToInt(tempsEcoule % 60);
            tempsEcouleText.text = string.Format("Temps: {0:00}:{1:00}", minutes, secondes);
        }
        else {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    public void ShowGameOver()
    {
        gameOverUI.SetActive(true);
        isGameOverDisplayed = true;
        Time.timeScale = 0f;


        Ennemi[] ennemis = GameObject.FindObjectsOfType<Ennemi>();  
        foreach (Ennemi ennemi in ennemis) { 
            Destroy(ennemi.gameObject);
        }

        if (tempsEcouleText != null) {
            tempsEcouleText.enabled = true;
        }


    }

    void RestartMission()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
