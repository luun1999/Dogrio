using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private Transform[] listLayer;
    [SerializeField] private float smoothing;
    private float[] parallaxLayerScale;
    private Transform cam;
    private Vector3 previousCameraPos;

    void Awake()
    {
        cam = Camera.main.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        previousCameraPos = cam.position;
        parallaxLayerScale = new float[listLayer.Length];

        for (int i = 0; i < listLayer.Length; i++)
        {
            //scale base on position z, further will be moved slower
            parallaxLayerScale[i] = listLayer[i].position.z * -1;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < listLayer.Length; i++)
        {
            //find parallax gap
            float parallaxX = (previousCameraPos.x - cam.position.x) * parallaxLayerScale[i];
            float parallaxY = (previousCameraPos.y - cam.position.y) * parallaxLayerScale[i];
            //set position for target background
            //set position
            float targetBackgroundPosX = listLayer[i].position.x + parallaxX;
            float targetBackgroundPosY = listLayer[i].position.y + parallaxY;
            //set vector3 position
            Vector3 targetBackgroundPos = new Vector3(targetBackgroundPosX, targetBackgroundPosY, listLayer[i].position.z);
            //use Lerp to move background to target point
            listLayer[i].position = Vector3.Lerp(listLayer[i].position, targetBackgroundPos, smoothing * Time.fixedDeltaTime);
        }

        //set previous camera when end of frame
        previousCameraPos = cam.position;
    }
}
