using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platformer_AnimatorController : MonoBehaviour
{
    public bool IsAttacking { get; private set; }

    Animator anim;
    SpriteRenderer sprite;


    private void Start()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        //start off facing to the right.
        anim.SetBool("IsWalking", false);
        anim.SetInteger("WalkDir", 1);
        sprite.flipX = true;

    }

    private void Update()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            anim.SetBool("IsWalking", true);

            if (Input.GetAxis("Horizontal") > 0)
            {
                sprite.flipX = true;
            }
            else
            {
                sprite.flipX = false;
            }
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }

        if (Input.GetMouseButton(0))
        {
            anim.SetTrigger("Attack");
            anim.SetBool("IsWalking", false);
        }

        IsAttacking = anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack");
    }
}
