using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    bool m_bGameIsPause = false;
    public void ReplayLevel() {
        //todo logic before reload this scene again
        Time.timeScale = 1f;
        m_bGameIsPause = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseGame() {
        Time.timeScale = 0f;
        m_bGameIsPause = true;
    }

    public void ResumeGame() {
        if (m_bGameIsPause) {
            Time.timeScale = 1f;
            m_bGameIsPause = false;
        }
    }

    public void BackToMenu() {
        //todo logic before back to menu
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu1");
    }
}
