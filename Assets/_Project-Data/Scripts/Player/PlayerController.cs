using JetBrains.Annotations;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed; // Player movement speed
    public float groundDistance; // Snapping distance to the ground

    // Creates fields in the Inspector panel
    public LayerMask groundLayer;
    public Rigidbody rigidbody;
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get the Rigidbody component on the player at the start of the game.
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        /* On every update, we shoot a raycast line downwards from the player that only detects the Ground layer.
         If that raycast line hits, we move the player a set height above the point that it hit. */
        RaycastHit hit;
        Vector3 castPos = transform.position;
        castPos.y += 1;
        if (Physics.Raycast(castPos, -transform.up, out hit, Mathf.Infinity, groundLayer))
        {
            if (hit.collider != null) // If the collider DOES hit...
            {
                Vector3 movePos = transform.position;
                movePos.y = hit.point.y + groundDistance;
                transform.position = movePos;
            }
        }

        // Use the Rigidbody to move the player
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if (x != 0) // Play sprite animations during movement
        {
            animator.SetBool("Move", true);
        }
        else if (y != 0)
        {
            animator.SetBool("Move", true);
        }
        else
        {
            animator.SetBool("Move", false);
        }

            Vector3 moveDir = new Vector3(x, 0, y);
        rigidbody.linearVelocity = moveDir * speed;

        // Flip the sprite in the sprite renderer depending on which way the player is moving.
        if (x != 0 && x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (x != 0 && x > 0)
        {
            spriteRenderer.flipX = false;
        }

        // Play attack animation on left click
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("Attack", true);
        }
        
    }

    /* public void Attack()
    {
        Collider[] enemy = Physics.OverlapCircleAll();
    }
    */ 

    public void endAttack()
    {
        animator.SetBool("Attack", false);
    }
}
