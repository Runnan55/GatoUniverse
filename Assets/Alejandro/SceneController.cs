using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void LoadIntro()
    {
        SceneManager.LoadScene("IntroScene");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void LoadCredit()
    {
        SceneManager.LoadScene("CreditScene");
    }

    public void QuitGame()
    {
        Debug.Log("Saliste del juego ;D");
        Application.Quit();
    }

    public void Option()
    {
        SceneManager.LoadScene("Option");
    }
}

