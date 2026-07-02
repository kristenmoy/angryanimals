using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Buttons")]
    public Button startGameButton;
    public Button levelSelectButton;

    void Start()
    {
        // make sure time is running
        Time.timeScale = 1f;

        // button listeners
        if (startGameButton != null)
        {
            startGameButton.onClick.AddListener(StartGame);
        }

        if (levelSelectButton != null)
        {
            levelSelectButton.onClick.AddListener(SelectLevel);
        }
    }

    public void StartGame()
    {
        // load level 1
        SceneManager.LoadScene(1);
    }

    public void SelectLevel()
    {
        // load level selection screen
        SceneManager.LoadScene(11);
    }
}