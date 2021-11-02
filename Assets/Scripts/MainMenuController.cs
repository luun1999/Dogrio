using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public bool isOptions = false;
    public GameObject mainMenu;
    public GameObject optionsMenu;
    // Start is called before the first frame update
    void Start()
    {
        ShowMainMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowMainMenu () {
        isOptions = false;
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void ShowOptionsMenu () {
        if (!isOptions) {
            isOptions = true;
            optionsMenu.SetActive(true);
            mainMenu.SetActive(false);
        }
    }
}
