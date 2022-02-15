using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hidro : Score
{
    private Animator animator;
    private Rigidbody2D rig;
    private Collider2D col;
    private void Start()
    {
        m_Score = 100;
        animator = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        rig.gravityScale = 0;
    }
    private void Update()
    {
        animator.SetBool("isDestroying", IsDestroying);
    }

    public override void OnPlayerEnter()
    {
        gameObject.layer = LayerMask.NameToLayer("Score Item");
        col.isTrigger = false;
        rig.gravityScale = 1;
        IsDestroying = true;
    }
}
