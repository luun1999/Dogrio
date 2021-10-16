using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float normalSpeed = 2.0f;
    public float rushSpeed = 4.0f;
    public float jumpSpeed = 5.0f;
    public float gravityScale = 0.2f;
    private Rigidbody2D rig;
    private Animator animator;
    bool isJump = false;
    bool isRush = false;
    bool isDied = false;
    bool allowJump = false;

    void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        isRush = animator.GetBool("isRush");
        isJump = animator.GetBool("isJump");
        isDied = animator.GetBool("isDied");
    }

    // Update is called once per frame
    void Update()
    {
        isDied = animator.GetBool("isDied");
        if (isDied == false)
        {
            UpdateDied();
            UpdateJump();
            UpdateRush();
            UpdateWalk();
            animator.SetFloat("velocityY", rig.velocity.y);
        }
    }

    void UpdateDied()
    {
        if (Input.GetKey(KeyCode.X))
        {
            animator.SetBool("isDied", true);
        }
    }

    void UpdateJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJump = true;
            animator.SetBool("isJump", true);
            animator.SetBool("isWalk", false);
        }
    }

    void UpdateRush()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("isWalk", false);
            animator.SetBool("isRush", true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            animator.SetBool("isRush", false);
        }
    }

    void UpdateWalk()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            isRush = animator.GetBool("isRush");
            transform.localScale = new Vector3(1, 1, 1);
            if (isRush == false && isJump == false)
            {
                animator.SetBool("isWalk", true);
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            isRush = animator.GetBool("isRush");
            transform.localScale = new Vector3(-1, 1, 1);
            if (isRush == false && isJump == false)
            {
                animator.SetBool("isWalk", true);
            }
        }

        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || isRush == true)
        {
            animator.SetBool("isWalk", false);
        }
    }

    void FixedUpdate()
    {
        float speed = 0;
        isDied = animator.GetBool("isDied");

        if (isDied == true)
        {
            speed = 0;
        }
        else if (isRush == true)
        {
            speed = rushSpeed;
        }
        else
        {
            speed = normalSpeed;
        }

        float translation = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        transform.Translate(translation, 0, 0);

        if (isJump == true && allowJump == true)
        {
            rig.AddForce(transform.up * jumpSpeed, ForceMode2D.Impulse);
            isJump = false;
            allowJump = false;
        }

        if (rig.velocity.y < 0)
        {
            rig.velocity -= Vector2.up * gravityScale * Time.fixedDeltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            Debug.Log("ggg");
            allowJump = true;
            animator.SetBool("isJump", false);
        }
    }
}
