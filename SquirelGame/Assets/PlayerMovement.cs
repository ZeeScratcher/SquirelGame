using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Camera cam;
    Vector2 movement;
    Vector2 mousePos;

    public LayerMask coinLayer;

    public int coinCount;

    // Start is called before the first frame update
    void Start()
    {
        coinCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }
    private void FixedUpdate() 
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        Vector2 LookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(LookDir.y, LookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (coinLayer == (coinLayer | (1 << collision.gameObject.layer)))
        {
            coinCount++;
            Debug.Log("Collected a coin! Coin count: " + coinCount);
            Destroy(collision.gameObject);
        }
    }
}
