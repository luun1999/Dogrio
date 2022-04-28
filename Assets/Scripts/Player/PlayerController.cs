using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_nSpeed;
    [SerializeField] private float m_nBrake;
    private GameObject m_Player;
    private float m_nHorizontalInput;
    private float m_nDirection;
    // Start is called before the first frame update
    void Start()
    {
        m_nDirection = 0;
        m_nHorizontalInput = 0;
        m_Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (m_nDirection != 0)
            m_nHorizontalInput = Mathf.Clamp(m_nHorizontalInput + Time.deltaTime * m_nDirection * m_nSpeed, -1, 1);
        else if (m_nDirection == 0 && IsPositive(m_nHorizontalInput) == 1) {
            m_nHorizontalInput = Mathf.Clamp(m_nHorizontalInput - Time.deltaTime * m_nBrake, 0, 1);
        }
        else if (m_nDirection == 0 && IsPositive(m_nHorizontalInput) == -1) {
            m_nHorizontalInput = Mathf.Clamp(m_nHorizontalInput + Time.deltaTime * m_nBrake, -1, 0);
        }
    }

    public void RightButtonDown() {
        m_nDirection = 1;
    }

    public void RightButtonUp() {
        m_nDirection = 0;
    }

    public void LeftButtonDown() {
        m_nDirection = -1;
    }

    public void LeftButtonUp() {
        m_nDirection = 0;
    }

    public float GetHorizontalInput() {
        return m_nHorizontalInput;
    }

    public void ResetInput() {
        m_nHorizontalInput = 0;
        Debug.Log("Reset");
    }

    private int IsPositive(float number) {
        if (number > 0) return 1;
        else if (number < 0) return -1;
        else return 0;
    }
}
