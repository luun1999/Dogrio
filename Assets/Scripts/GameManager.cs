using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [SerializeField] private Slider loadingSlider;
    public static GameManager instance;
    [SerializeField] private GameObject loadingScreen;

    private void Awake()
    {
        instance = this;
    }

    public void LoadChoosingLevel()
    {
        SceneManager.LoadScene("ChooseLevel");
    }

    public void LoadLevel(int level)
    {
        //display loading screen
        loadingScreen.SetActive(true);
        //loading level in backgrounds
        AsyncOperation loadingLevel = SceneManager.LoadSceneAsync("Forest" + level);

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
