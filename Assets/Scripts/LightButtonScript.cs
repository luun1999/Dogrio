using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightButtonScript : MonoBehaviour
{
    public Sprite push;
    public Sprite nonPush;
    public SpriteRenderer render;

    public GameObject[] lights;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            render.sprite = push;
            foreach (var light in lights)
            {
                light.GetComponent<Light>().isActive = !light.GetComponent<Light>().isActive;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            render.sprite = nonPush;
        }
    }
}
