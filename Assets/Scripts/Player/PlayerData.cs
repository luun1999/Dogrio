using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float[] position;

    public PlayerData (GameObject playerPosition) {
        position = new float[3];
        position[0] = playerPosition.transform.position.x;
        position[1] = playerPosition.transform.position.y;
        position[2] = playerPosition.transform.position.z;
    }
}
