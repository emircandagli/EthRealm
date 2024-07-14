using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    private bool isMoving;

    private Vector2 input;
    
    private Animator animator;

    public LayerMask solidObjectsLayer;
    public LayerMask interactableLayer;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void HandleUpdate()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");


            animator.SetFloat("move_x", input.x);
            animator.SetFloat("move_y", input.y);    

            if (input != Vector2.zero)
            {
                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;
                if (isWalkable(targetPos)){
                    StartCoroutine(Move(targetPos));             
                }
            }
        }

        animator.SetBool("isMoving", isMoving);

        if (Input.GetKeyDown(KeyCode.X))
        {
            Interact();
        }
    }

    void Interact(){
        var facingDir = new Vector3(animator.GetFloat("move_x"), animator.GetFloat("move_y"));
        var interactPos = transform.position + facingDir;
        var collider = Physics2D.OverlapCircle(interactPos, 0.3f, interactableLayer);
        if (collider != null)
        {
            collider.GetComponent<Interactable>()?.Interact();
        }
    }
    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;
    }

    private bool isWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.1f, solidObjectsLayer | interactableLayer) != null)
        {
            return false;
        }
        return true;
    }
}