using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalObjects : MonoBehaviour
{
    private void Awake()
    {
        GameObject obj = GameObject.Find("GlobalObjects");

        if (obj.GetComponent<GlobalObjects>() != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
