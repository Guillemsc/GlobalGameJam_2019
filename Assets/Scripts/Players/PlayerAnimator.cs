using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] Animator2D animator;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        animator.PlayAnimation("Idle", 0.1F);
        rb = GetComponentInParent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        if (rb.velocity != new Vector2(0, 0))
        {
            if (rb.velocity.x < 0)
                animator.SetFilpX(false);
            if (rb.velocity.x > 0)
                animator.SetFilpX(true);

            animator.PlayAnimation("Walk", 0.1F);
        }
        else
            animator.PlayAnimation("Idle", 0.1F);

    }
}
