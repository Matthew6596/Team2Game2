using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerController : MonoBehaviour
{
    public float speed = 8.0f;
    public float jumpStrength = -10.0f;
    public float gravity = -9.8f;
    public float gravityMultiplier = 3.0f;

    public GameObject model;
    bool isFlipped = false;

    float velocity = 1;

    Animator anim;

    CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveZ = Input.GetAxis("Horizontal") * speed;
        //Flip Player
        if(moveZ < 0)
        {
            //Flip player
            Debug.Log(moveZ);
            StartCoroutine(FlipPlayer());
        }
        if (moveZ > 0 && isFlipped)
        {
            //Do not flip player
            Debug.Log(moveZ);
            model.transform.Rotate(0, 180, 0);
            isFlipped = false;
        }

        //float moveX = Input.GetAxis("Vertical") * speed;
        //Vector3 movement = new Vector3(moveX, 0, moveZ);
        Vector3 movement = new Vector3(0, 0, moveZ);

        if(movement != new Vector3(0,0,0))
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        movement = Vector3.ClampMagnitude(movement, speed);

        if (IsGrounded() && velocity < 0)
        {
            velocity = -1;
        }
        else
        {
            velocity += gravity * gravityMultiplier * Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
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

    IEnumerator FlipPlayer()
    {
        //model.transform.rotation = Quaternion.Euler(0,180,0);
        isFlipped = true;
        model.transform.Rotate(0, 180, 0);
        yield return new WaitForSeconds(1.0f);
    }
}
