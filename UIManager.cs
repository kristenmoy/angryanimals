using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject winPanel;
    public GameObject losePanel;
    public Button nextLevelButton;
    public Button restartButtonWin;
    public Button restartButtonLose;
    public Button menuButton;
    public Button restart;
    public TMP_Text animalCounter;
    
    void Start()
    {
        // hide panels at start
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }
        if (losePanel != null)
        {
            losePanel.SetActive(false);
        }
        
        // button listeners for win/lose panels
        if (nextLevelButton != null)
        {
            nextLevelButton.onClick.AddListener(NextLevel);
        }
        if (restartButtonWin != null)
        {
            restartButtonWin.onClick.AddListener(Restart);
        }
        if (restartButtonLose != null)
        {
            restartButtonLose.onClick.AddListener(Restart);
        }
        
        // button listeners for menu and restart buttons
        if (menuButton != null)
        {
            menuButton.onClick.AddListener(Menu);
        }
        if (restart != null)
        {
            restart.onClick.AddListener(Restart);
        }
    }
    
    public void WinScreen()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);
            Time.timeScale = 0f;
        }

        // hide counter are restart button
        restart.gameObject.SetActive(false);
        animalCounter.gameObject.SetActive(false);
    }
    
    public void LoseScreen()
    {
        if (losePanel != null)
        {
            losePanel.SetActive(true);
            Time.timeScale = 0f;
        }

        // hide counter are restart button
        restart.gameObject.SetActive(false);
        animalCounter.gameObject.SetActive(false);
    }
    
    void NextLevel()
    {
        Time.timeScale = 1f;
        FindObjectOfType<GameManager>().LoadNextLevel();
    }

    void Restart()
    {
        Time.timeScale = 1f;
        FindObjectOfType<GameManager>().RestartLevel();
    }
    
    void Menu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void UpdateAnimalsLeft(int animalsRemaining)
    {
        animalCounter.text = $"Animals Remaining: {animalsRemaining}";
    }
}