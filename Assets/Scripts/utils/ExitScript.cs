using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScript : MonoBehaviour
{
    public void Exit () {
        Debug.Log("Exit!!!");
        Application.Quit();
    }
}
