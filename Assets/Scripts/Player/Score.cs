using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private bool isDestroying = false;
    protected int m_Score;
    public bool IsDestroying { get => isDestroying; set => isDestroying = value; }

    public int getScore()
    {
        return m_Score;
    }
    public void setScore(int score)
    {
        m_Score = score;
    }

    public virtual void OnPlayerEnter() { }
}
