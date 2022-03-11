using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform follow;
    public Vector3 offset;
    public float smooth;
    public float rangeWidth;
    public float rangeHeight;

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 target = new Vector3(follow.transform.position.x, follow.transform.position.y, transform.position.z);
        target = target + offset;

        Vector3 smoothTransform = Vector3.Lerp(transform.position, target, smooth * Time.fixedDeltaTime);
        transform.position = smoothTransform;
    }
}
