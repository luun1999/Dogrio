using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class LoadNumberLevel : MonoBehaviour
{
    public GameObject sceneElement;
    Scene[] scenes;
    string ForestKey = "Forest";
    int levelTextIndex = 2;
    // Start is called before the first frame update
    void Start()
    {
        var numberScene = SceneManager.sceneCountInBuildSettings;
        List<string> arrayName = new List<string>();
        for (int n = 0; n < numberScene; n++)
        {
            var temp = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(n));
            var checkname = temp.IndexOf(ForestKey);
            if (checkname == 0) {
                arrayName.Add(temp);
            }
        }
        for (int i = 0; i < arrayName.Count; i++) {
            int l_keyForest = ForestKey.Length;
            var sceneBoard = Instantiate(sceneElement, transform.position, Quaternion.identity);
            sceneBoard.transform.SetParent(this.transform);

            int copyIndex = i;
            sceneBoard.GetComponent<Button>().onClick.AddListener(() => {
                // How to use Gamemanager ?
                SceneManager.LoadScene(arrayName[copyIndex]);
            });

            GameObject levelTextElement = sceneBoard.transform.GetChild(levelTextIndex).gameObject;
            var elementText = levelTextElement.GetComponent<TextMeshProUGUI>();
            string level = arrayName[i].Substring(l_keyForest, 1);

            elementText.SetText(ForestKey + " " + level);
        }
    } 
}
