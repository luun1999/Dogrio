using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//must have import prefabs of GlobalObjects 
public class LevelSceneManager : MonoBehaviour
{
    private GameManager m_GameManager;

    private void Awake()
    {
        m_GameManager = FindObjectOfType<GameManager>();
    }
    public void GoToLevel(int level)
    {
        m_GameManager.LoadLevel(level);
    }
}
