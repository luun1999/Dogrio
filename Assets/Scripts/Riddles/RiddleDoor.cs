using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiddleDoor : MonoBehaviour
{
    [SerializeField] private GameObject[] lights;
    private Animator amin;
    private bool isInto;

    public GameObject identifyInto;
    // Start is called before the first frame update
    void Start()
    {
        amin = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isInto = identifyInto.GetComponent<IndentifyIntoRiddles>().isInRiddle;
        if (isInto == false)
        {
            return;
        }
        bool bingoRiddle = true;
        foreach (var light in lights)
        {
            if (light.GetComponent<Light_>().isActive == false)
            {
                bingoRiddle = false;
                break;
            }
        }

        if (bingoRiddle)
        {
            amin.SetBool("isClose", false);
        }
        else
        {
            amin.SetBool("isClose", true);
        }
    }
}
