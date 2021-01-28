using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyMovement : MonoBehaviour
{

    // Movement Variables  
    float moveVertical;
    float moveHorizontal;
    float moveUp;

    public float speed = 5f;
    public float jumpForce = 7f;

    // Misc
    private Rigidbody rb;

    public SphereCollider col;

    public LayerMask groundLayer;

    Transform target;

    Vector2 movementInput;

    void Start()
    {
        // Accessing Components
        rb = GetComponent<Rigidbody>();
        col = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        // Input
        moveVertical = Input.GetAxis("Verticle");
        moveHorizontal = Input.GetAxis("Horizontal");

        // Moving Player
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
        }

        transform.Translate(movement * speed * Time.deltaTime, Space.World);
   
        // Input for jumping
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }
    }

    void Jump()
    {
        // Jumping
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private bool IsGrounded()
    {
        // Check if grounded
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x,
            col.bounds.min.y, col.bounds.center.z), col.radius * .9f, groundLayer);
    }
}
