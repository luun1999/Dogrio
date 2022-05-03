using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [SerializeField] private Slider loadingSlider;
    [SerializeField] private GameObject loadingScreen;

    //Singleton pattern
    private static GameManager instance;
    public static GameManager Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        var gameManager = GameObject.FindObjectOfType<GameManager>();
        if (!gameManager)
        {
            Instance = this;
        }
        else
        {
            Instance = gameManager;
            Debug.Log("set instance for game manager");
        }
    }

    public void LoadChoosingLevel()
    {
        SceneManager.LoadScene("ChooseLevel");
    }


    //todo : Move to SceneManager
    public void LoadLevel(int level)
    {
        //display loading screen
        loadingScreen.SetActive(true);
        //loading level in backgrounds
        AsyncOperation loadingLevel = SceneManager.LoadSceneAsync("Level" + level);

        //get progress data and undisplay loading screen when progress is finished
        StartCoroutine(GetSceneLoadingProgress(loadingLevel));
    }

    private IEnumerator GetSceneLoadingProgress(AsyncOperation loading)
    {
        while (!loading.isDone)
        {
            float progress = Mathf.Clamp01(loading.progress / 0.9f);
            loadingSlider.value = progress;
            yield return null;
        }

        loadingScreen.SetActive(false);
    }
}
