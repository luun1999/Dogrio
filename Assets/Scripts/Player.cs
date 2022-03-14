using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float m_nJumpSpeed;
    [SerializeField] private float m_nHigherJumpSpd;
    [SerializeField] private float m_nRunSpeed;
    [SerializeField] private float m_nGravityScale;
    [SerializeField] private ParticleSystem deathParticle;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject completeLevelUI;
    [SerializeField] private int m_PlayerScore;

    private PlayerController m_PlayerController;
    private bool m_bIsGround;
    private bool m_bIsJumpHigher;
    private bool m_bIsDeath = false;
    private Rigidbody2D rig;
    private SpriteRenderer sprite;
    private Animator anim;

    public int PlayerScore { get => m_PlayerScore; set => m_PlayerScore = value; }

    void Start()
    {
        // Test game at 30 fps
        // Application.targetFrameRate = 30;

        rig = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        m_PlayerController = GameObject.FindObjectOfType<PlayerController>();

        m_bIsJumpHigher = false;

        PlayerData data = SaveSystem.LoadPlayer();
        if (data != null)
        {
            transform.position = new Vector3(data.position[0], data.position[1], data.position[2]);
        }
    }
    void Update()
    {
        if (m_bIsDeath)
        {
            return;
        }
        anim.SetFloat("velocityY", rig.velocity.y);
        anim.SetBool("isJumpHigher", m_bIsJumpHigher);
        anim.SetBool("isGround", m_bIsGround);

        if (Input.GetKey(KeyCode.Space))
        {
            m_bIsJumpHigher = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            m_bIsJumpHigher = false;
        }

        if (Input.GetKey(KeyCode.D))
        {
            m_PlayerController.RightButtonDown();
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            m_PlayerController.RightButtonUp();
        }

        if (Input.GetKey(KeyCode.A))
        {
            m_PlayerController.LeftButtonDown();
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            m_PlayerController.LeftButtonUp();
        }

        if (Input.GetKey(KeyCode.Delete))
        {
            SaveSystem.DeletePlayerData();
        }

        if (m_PlayerController.GetHorizontalInput() < 0)
        {
            sprite.flipX = true;
        }
        else if (m_PlayerController.GetHorizontalInput() > 0)
        {
            sprite.flipX = false;
        }
    }

    void FixedUpdate()
    {
        //float translation =  Input.GetAxis("Horizontal") * m_nRunSpeed * Time.fixedDeltaTime;
        if (m_bIsDeath)
        {
            return;
        }

        if (!completeLevelUI.activeSelf)
        {
            //rig.constraints = RigidbodyConstraints2D.FreezePositionX;
            float translation = m_PlayerController.GetHorizontalInput() * m_nRunSpeed * Time.fixedDeltaTime;
            transform.Translate(translation, 0, 0);
            // Debug.Log("FreezePosion");
        }

        if (rig.velocity.y < 0 && rig.velocity.y >= -9.8)
        {
            //Debug.Log("Gravity effect" + rig.velocity.y);
            float translate = rig.velocity.y * m_nGravityScale * Time.fixedDeltaTime;
            transform.Translate(0, translate, 0);
        }

        if (m_bIsGround)
        {
            //fix player sometime jump higher than I want.
            rig.velocity = Vector2.zero;
            rig.angularVelocity = 0;

            if (m_bIsJumpHigher)
            {
                rig.AddForce(transform.up * m_nHigherJumpSpd, ForceMode2D.Force);
            }
            else
            {
                rig.AddForce(transform.up * m_nJumpSpeed, ForceMode2D.Force);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger");
        if (other.gameObject.CompareTag("Ground"))
        {
            m_bIsGround = true;
            anim.SetBool("isGround", true); // fuck Unity
            if (m_bIsJumpHigher)
            {
                anim.Play("jump");
            }
            else
            {
                anim.Play("idleJump");
            }
        }

        if (other.gameObject.CompareTag("End Point"))
        {
            completeLevelUI.SetActive(true);
        }

        if (other.gameObject.CompareTag("SavePoint"))
        {
            Debug.Log("Hey hey save here!");
            SavePlayer();
        }

        if (other.gameObject.CompareTag("Score"))
        {
            Score scoreObject = other.gameObject.GetComponent<Score>();
            if (!scoreObject.IsDestroying)
            {
                PlayerScore += scoreObject.getScore();
                scoreObject.OnPlayerEnter();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            m_bIsGround = false;
        }
    }

    // Death
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("LightButton"))
        {
            m_bIsGround = true;
        }

        if (other.gameObject.CompareTag("NeedleTrap") && m_bIsDeath == false)
        {
            deathParticle.Play();
            m_bIsDeath = true;
            rig.constraints = RigidbodyConstraints2D.FreezeAll;
            sprite.color = new Color(1, 0, 0, 0);

            //game over
            //logic when game over
            gameOverUI.SetActive(true);

        }
    }

    #region local methods
    public void SetHigherJump(bool isHigher)
    {
        m_bIsJumpHigher = isHigher;
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];

        transform.position = position;
    }
    #endregion
}
