using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_ : MonoBehaviour
{
    [SerializeField] private Sprite lightOn;
    [SerializeField] private Sprite lightOff;
    [SerializeField] private SpriteRenderer sprite;

    public bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isActive == false)
        {
            sprite.sprite = lightOff;
        }
        else
        {
            sprite.sprite = lightOn;
        }
    }
}
