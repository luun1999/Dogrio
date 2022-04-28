using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniPlayer : MonoBehaviour
{
    private Rigidbody rig;
    [SerializeField] private float m_nSpeed;
    [SerializeField] private float m_nMaxSpeed;

    // Start is called before the first frame update
    void Start() {
        rig = GetComponent<Rigidbody>();

        transform.rotation = Quaternion.Euler(0, 0, Random.Range(10, 360));
        rig.AddForce(transform.up * m_nSpeed, ForceMode.Impulse);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float nVelX = MyClamp(rig.velocity.x, 0.8f, 5.5f);
        float nVelY = MyClamp(rig.velocity.y, 0.8f, 5.5f);
        rig.velocity = new Vector3(nVelX, nVelY, 0);
    }

    float MyClamp(float value, float minValue, float maxValue) {
        int isPositive = value >= 0 ? 1 : -1;
        float nValue =  Mathf.Clamp(Mathf.Abs(value), minValue, maxValue);
        return nValue * isPositive;
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Bounder") {
            rig.AddForce(transform.up * m_nSpeed, ForceMode.Impulse);
        }
    }
}
