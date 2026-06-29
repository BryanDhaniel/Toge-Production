using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public LayerMask collisionLayer; // set ke layer Wall/House di Inspector

    private bool isMoving;
    private Vector2 input;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isMoving) return;

        input = Vector2.zero;

        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
        {
            input = Vector2.up;
        }
        else if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
        {
            input = Vector2.down;
        }
        else if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            input = Vector2.left;
        }
        else if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            input = Vector2.right;
        }

        if (input != Vector2.zero)
        {
            Vector3 targetPos = transform.position + (Vector3)input;

            if (!IsBlocked(targetPos))
            {
                StartCoroutine(Move(targetPos, input));
            }
            else
            {
                animator.Play("Idle"); // tetap idle kalau nabrak
            }
        }
        else
        {
            animator.Play("Idle");
        }
    }

    bool IsBlocked(Vector3 targetPos)
    {
        Collider2D hit = Physics2D.OverlapCircle(targetPos, 0.2f, collisionLayer);
        // Debug.Log("Cek posisi: " + targetPos + " | Kena: " + (hit != null ? hit.name : "TIDAK ADA"));
        return hit != null;
    }

    IEnumerator Move(Vector3 targetPos, Vector2 direction)
    {
        isMoving = true;
        animator.Play("IdleWalk");

        if (direction == Vector2.left)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (direction == Vector2.right)
            transform.localScale = new Vector3(1, 1, 1);

        while ((targetPos - transform.position).sqrMagnitude > 0.0001f)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPos,
                moveSpeed * Time.deltaTime
            );

            yield return null;
        }

        transform.position = targetPos;
        isMoving = false;
    }
}