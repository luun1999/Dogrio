using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour
{
    private Player m_Player;
    private TextMeshProUGUI m_ScoreText;
    private string m_ScoreFmt = "000000";

    // Start is called before the first frame update
    void Start()
    {
        m_Player = FindObjectOfType<Player>();
        m_ScoreText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        m_ScoreText.text = "Score: " + m_Player.GetComponent<Player>().PlayerScore.ToString(m_ScoreFmt);
    }
}
