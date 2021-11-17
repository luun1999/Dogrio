using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float m_nJumpSpeed;
    [SerializeField] private float m_nHigherJumpSpd;
    [SerializeField] private float m_nRunSpeed;
    [SerializeField] private float m_nGravityScale;
    private PlayerController m_PlayerController;
    private bool m_bIsGround;
    private bool m_bIsJumpHigher;
    private Rigidbody2D rig;
    private SpriteRenderer sprite;
    private Animator anim;
    
    void Start() {
        rig = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        m_PlayerController = GameObject.FindObjectOfType<PlayerController>();

        m_bIsJumpHigher = false;
    }
    void Update() {
        if (Input.GetKey(KeyCode.Space)) {
            m_bIsJumpHigher = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space)) {
            m_bIsJumpHigher = false;
        }

        if (Input.GetKey(KeyCode.D)) {
            m_PlayerController.RightButtonDown();
        }
        else if (Input.GetKeyUp(KeyCode.D)) {
            m_PlayerController.RightButtonUp();
        }

        if (Input.GetKey(KeyCode.A)) {
            m_PlayerController.LeftButtonDown();
        }
        else if (Input.GetKeyUp(KeyCode.A)) {
            m_PlayerController.LeftButtonUp();
        }

        anim.SetFloat("velocityY", rig.velocity.y);
        anim.SetBool("isJumpHigher", m_bIsJumpHigher);
        anim.SetBool("isGround", m_bIsGround);

        if (m_PlayerController.GetHorizontalInput() < 0) {
            sprite.flipX = true;
        }
        else if (m_PlayerController.GetHorizontalInput() > 0){
            sprite.flipX = false;
        }
    }

    void FixedUpdate() {
        //float translation =  Input.GetAxis("Horizontal") * m_nRunSpeed * Time.fixedDeltaTime;
        float translation =  m_PlayerController.GetHorizontalInput() * m_nRunSpeed * Time.fixedDeltaTime;
        transform.Translate(translation, 0, 0);

        if (rig.velocity.y < 0 && rig.velocity.y >= -9.8) {
            //Debug.Log("Gravity effect" + rig.velocity.y);
            float translate  = rig.velocity.y * m_nGravityScale * Time.fixedDeltaTime;
            transform.Translate(0, translate, 0);
        }

        if (m_bIsGround) {
            //fix player sometime jump higher than I want.
            rig.velocity = Vector2.zero;
            rig.angularVelocity = 0;

            if (m_bIsJumpHigher) {
                rig.AddForce(transform.up * m_nHigherJumpSpd, ForceMode2D.Force);
            }
            else {
                rig.AddForce(transform.up * m_nJumpSpeed, ForceMode2D.Force);
            }
            m_bIsGround = false;
        }
    }

    void OnTriggerStay2D(Collider2D collider) {
        //Debug.Log("Stay");

        if (collider.gameObject.tag == "Ground") {
            m_bIsGround = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        //Debug.Log("Exit");
    }

    #region local methods
    public void SetHigherJump(bool isHigher) {
        m_bIsJumpHigher = isHigher;
    }
    #endregion
}
