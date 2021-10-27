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
    private Rigidbody2D rig;
    void Start() {
        rig = GetComponent<Rigidbody2D>();
        m_PlayerController = GameObject.FindObjectOfType<PlayerController>();
    }
    void Update() {
        if (Input.GetKey(KeyCode.LeftShift)) {
            m_PlayerController.DispatchHigherJump(true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift)) {
            m_PlayerController.DispatchHigherJump(false);
        }
    }

    void FixedUpdate() {
        if (m_bIsGround) {
            if (m_PlayerController.GetHigherJump()) {
                rig.AddForce(Vector2.up * m_nHigherJumpSpd, ForceMode2D.Impulse);
            }
            else {
                rig.AddForce(Vector2.up * m_nJumpSpeed, ForceMode2D.Impulse);
            }
            m_bIsGround = false;
        }

        //float translation =  Input.GetAxis("Horizontal") * m_nRunSpeed * Time.fixedDeltaTime;
        float translation =  m_PlayerController.GetHorizontalInput() * m_nRunSpeed * Time.fixedDeltaTime;
        transform.Translate(translation, 0, 0);

        if (rig.velocity.y < 0 && rig.velocity.y >= -9.8) {
            //Debug.Log("Gravity effect" + rig.velocity.y);
            float translate  = rig.velocity.y * m_nGravityScale * Time.fixedDeltaTime;
            transform.Translate(0, translate, 0);
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
    #endregion
}
