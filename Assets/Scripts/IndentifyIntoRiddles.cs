using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndentifyIntoRiddles : MonoBehaviour
{
    [SerializeField] private GameObject[] woodDoors;

    public bool isInRiddle = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var wood_door in woodDoors)
            {
                var wood_door_amin = wood_door.GetComponent<Animator>();
                wood_door_amin.SetBool("isClose", true);
                isInRiddle = true;
            }
        }
    }
}
