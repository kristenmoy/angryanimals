using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectLevel : MonoBehaviour
{
    public Button level1;
    public Button level2;
    public Button level3;
    public Button level4;
    public Button level5;
    public Button level6;
    public Button level7;
    public Button level8;
    public Button level9;
    public Button level10;
    public Button back;

    void Start()
    {
        Time.timeScale = 1f;

        // button listeners
        level1.onClick.AddListener(LoadLevel1);
        level2.onClick.AddListener(LoadLevel2);
        level3.onClick.AddListener(LoadLevel3);
        level4.onClick.AddListener(LoadLevel4);
        level5.onClick.AddListener(LoadLevel5);
        level6.onClick.AddListener(LoadLevel6);
        level7.onClick.AddListener(LoadLevel7);
        level8.onClick.AddListener(LoadLevel8);
        level9.onClick.AddListener(LoadLevel9);
        level10.onClick.AddListener(LoadLevel10);
        back.onClick.AddListener(BackButton);
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadLevel3()
    {
        SceneManager.LoadScene(3);
    }

    public void LoadLevel4()
    {
        SceneManager.LoadScene(4);
    }

    public void LoadLevel5()
    {
        SceneManager.LoadScene(5);
    }

    public void LoadLevel6()
    {
        SceneManager.LoadScene(6);
    }

    public void LoadLevel7()
    {
        SceneManager.LoadScene(7);
    }

    public void LoadLevel8()
    {
        SceneManager.LoadScene(8);
    }

    public void LoadLevel9()
    {
        SceneManager.LoadScene(9);
    }

    public void LoadLevel10()
    {
        SceneManager.LoadScene(10);
    }

    public void BackButton()
    {
        SceneManager.LoadScene(0);
    }
}