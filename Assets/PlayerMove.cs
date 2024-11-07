using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerMove : MonoBehaviour
{
    public float speed = 10f;
    public Transform targetPosition;
    private Vector2 movement;
    private Rigidbody2D rb;
    public Tilemap map;
    public LayerMask UnwalkableLayer;
    public LayerMask MoveableLayer;

    public GameObject panel;

    public Tilemap map;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //targetPosition.position = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 targetPosition = rb.position + movement * speed * Time.fixedDeltaTime;
        Collider2D hitCollider = Physics2D.OverlapCircle(targetPosition, 0.1f, UnwalkableLayer);
        if (hitCollider == null)
        {
            rb.MovePosition(targetPosition);
        }
        /*if (Vector3.Distance(transform.position, targetPosition.position) < 0.1f &&
            !Physics2D.OverlapCircle(targetPosition.position + new Vector3(movement.x, movement.y, 0f), .1f, UnwalkableLayer))
        {
            Debug.Log("bojangles was here. aka I should be able to movein the direction I'm trying to");
            Debug.Log(targetPosition.position);
            //changed overlap circle from .1f to .2f because it's not registering when I'm one unit away from a moveable object
            Collider2D g = Physics2D.OverlapCircle(targetPosition.position + new Vector3(movement.x, movement.y, 0f), .2f, MoveableLayer);
            if (g) // there is a moveable gameObject next to me
            {
                //this might need to be an if-else statement eventually
                //for smooth movement (not blinking), give g its own target position and movetowards
                Debug.Log("there's a moveable object next to me, imma try to move it");
                g.gameObject.transform.position = new Vector3(g.gameObject.transform.position.x + movement.x, g.gameObject.transform.position.y + movement.y, 0f);
                //if the thing we're trying to move isn't also next to something that shouldn't move
                if (!Physics2D.OverlapCircle(targetPosition.position + new Vector3(2 * movement.x, 2 * movement.y, 0f), .1f, UnwalkableLayer))
                {
                    Debug.Log("I should be able to move it");
                    targetPosition.position = new Vector3(targetPosition.position.x + movement.x, targetPosition.position.y + movement.y, 0f);
                }
            }
            else
            {
                //should be able to move freely
                Debug.Log("no obstacles to movement");
                targetPosition.position = new Vector3(targetPosition.position.x + movement.x, targetPosition.position.y + movement.y, 0f);
            }
        }
        //targetPosition.position = new Vector3(targetPosition.position.x + movement.x, targetPosition.position.y + movement.y, 0f);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, speed * Time.deltaTime);
    }
    private void OnMove(InputValue value)
    {
        /*movement = value.Get<Vector2>()* new Vector2(.5f, .5f);


        if (movement.x != 0 && movement.y != 0)
        {
            movement = new Vector2(0, 0);
        }
        if (map.GetTile(map.WorldToCell(targetPosition.position)) == false)
        {
            targetPosition.position = new Vector3(targetPosition.position.x + movement.x, targetPosition.position.y + movement.y, 0f);
            //transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, speed * Time.deltaTime);
            transform.position = targetPosition.position;
        }
        else
        {
            targetPosition.position = new Vector3(transform.position.x + movement.x, transform.position.y + movement.y, 0f);
            transform.position = targetPosition.position;
        }*/
        Vector2 input = value.Get<Vector2>();
        movement = new Vector2(
            (input.x - input.y) * 0.5f,
            ((input.x + input.y) /2) * 0.5f
        );
        Debug.Log(map.GetTile(map.WorldToCell(transform.position)) != null);
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
