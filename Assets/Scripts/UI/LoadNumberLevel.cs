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
            string level = arrayName[i].Substring(l_keyForest, 1);

            var sceneBoard = Instantiate(sceneElement, transform.position, Quaternion.identity);
            sceneBoard.transform.SetParent(this.transform);
            sceneBoard.transform.localScale = new Vector3(1, 1, 1);

            int copyIndex = i;
            sceneBoard.GetComponent<Button>().onClick.AddListener(() => {
                GameManager.Instance.LoadLevel(int.Parse(level));

                // SceneManager.LoadScene(arrayName[copyIndex]);
            });

            GameObject levelTextElement = sceneBoard.transform.GetChild(levelTextIndex).gameObject;
            var elementText = levelTextElement.GetComponent<TextMeshProUGUI>();

            elementText.SetText(ForestKey + " " + level);
        }
    } 
}
