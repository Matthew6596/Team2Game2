using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 8.0f;
    public float jumpStrength = -10.0f;
    public float gravity = -9.8f;
    public float gravityMultiplier = 3.0f;
    float velocity = 1;

    CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveZ = Input.GetAxis("Horizontal") * speed;
        //float moveX = Input.GetAxis("Vertical") * speed;
        //Vector3 movement = new Vector3(moveX, 0, moveZ);
        Vector3 movement = new Vector3(0, 0, moveZ);

        movement = Vector3.ClampMagnitude(movement, speed);

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (IsGrounded() && velocity < 0)
        {
            velocity = -1;
        }
        else
        {
            velocity += gravity * gravityMultiplier * Time.deltaTime;
        }

        movement.y = velocity;

        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        characterController.Move(movement);
    }

    bool IsGrounded()
    {
        return characterController.isGrounded;
    }

    public void Jump()
    {
        velocity *= jumpStrength;
    }
}
