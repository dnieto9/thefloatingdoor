using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public float speed = 1;
    public Transform targetPosition;
    private Vector2 movement;

    public LayerMask UnwalkableLayer;
    public LayerMask MoveableLayer;

    public GameObject panel;

    private void Awake()
    {
        targetPosition.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Vector3.Distance(transform.position, targetPosition.position) < 0.1f &&
            !Physics2D.OverlapCircle(targetPosition.position + new Vector3(movement.x, movement.y, 0f), .1f, UnwalkableLayer))
        {
            if (Physics2D.OverlapCircle(targetPosition.position + new Vector3(movement.x, movement.y, 0f), .1f, MoveableLayer))
            {
                if (!Physics2D.OverlapCircle(targetPosition.position + new Vector3(2 * movement.x, 2 * movement.y, 0f), .1f, UnwalkableLayer))
                {
                    targetPosition.position = new Vector3(targetPosition.position.x + movement.x, targetPosition.position.y + movement.y, 0f);
                }
            }
            else
            {
                targetPosition.position = new Vector3(targetPosition.position.x + movement.x, targetPosition.position.y + movement.y, 0f);
            }
        }
        targetPosition.position = new Vector3(targetPosition.position.x + movement.x, targetPosition.position.y + movement.y, 0f);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, speed * Time.deltaTime);*/
    }
    private void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
        /*if (movement.x != 0 && movement.y != 0)
        {
            movement = new Vector2(0, 0);
        }*/
        if (Vector3.Distance(transform.position, targetPosition.position) < 0.1f && !Physics2D.OverlapCircle(targetPosition.position + new Vector3(movement.x, movement.y, 0f), .1f, UnwalkableLayer))
        {
            targetPosition.position = new Vector3(targetPosition.position.x + movement.x, targetPosition.position.y + movement.y, 0f);
            //transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, speed * Time.deltaTime);
            transform.position = targetPosition.position;
        }
        else
        {
            targetPosition.position = new Vector3(transform.position.x + movement.x, transform.position.y + movement.y, 0f);
            transform.position = targetPosition.position;
        }
    }
    /*
    public void Update()
    {
        transform.Translate(Vector3.Normalize(movement) * speed * Time.deltaTime);
    }

    void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
        Debug.Log("Key pressed");
    }
    */
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Staircase"))
        {

            Destroy(other.gameObject);
            panel.SetActive(true);
        }
    }
}
