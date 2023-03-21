using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttonamager : MonoBehaviour
{
    public void GotoInGameScene()
    {
        GameObject.Find("SceneChange").GetComponent<SceneChange>().Change("Stage1");

    }
    public void QuitGame()
    {
        Application.Quit();   
    }
    public void GotoWinScene()
    {
        SceneManager.LoadScene("ResultScene");
    }

    public void ReturnToTitle()
    {
        SceneManager.LoadScene("Title");
    }

    public void GameOverScene()
    {
        SceneManager.LoadScene("GameOver");
    }
    
}
