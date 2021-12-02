using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    private Animator anim;

    public Animator GetAnim()
    {
        return anim;
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject[] savePoints = GameObject.FindGameObjectsWithTag("SavePoint");
        foreach (var gameObj in savePoints)
        {
            if (gameObj.name == this.name)
            {
                Debug.Log(anim.GetBool("isActive"));
                continue;
            }
            var animObj = gameObj.GetComponent<Animator>();
            animObj.SetBool("isActive", false);
        }
        anim.SetBool("isActive", true);
    }
}
