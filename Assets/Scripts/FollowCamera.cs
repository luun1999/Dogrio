using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform player;
    public int offset = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float spaceX = player.position.x - transform.position.x;
        float spaceY = player.position.y - transform.position.y;
        transform.Translate(new Vector2(spaceX + offset, spaceY + offset));
    }
}
