using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CharacterController CC;

    float Gravity = -3.5f;

    [SerializeField] float WalkingSpeed = 2f;
    [SerializeField] float JumpPower = 2f;

    Vector3 velocity;

    Vector3 move;

    bool isGrounded;

    bool isJumping;

    [SerializeField] Transform CamPivot;

    float xRot;

    void Update()
    {
        //Applying gravity
        if (!isGrounded)
        {
            if(isJumping)
            {
                isJumping = false;
            }

            velocity.y += Gravity * Time.deltaTime;

            CC.Move(velocity * Time.deltaTime);
        }
        else if(isJumping)
        {
            isGrounded = false;
            velocity.y = 0;
            velocity.y += JumpPower;

            CC.Move(velocity * Time.deltaTime);            
        }

        //Jump
        if(Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
        }

        //Movement
        move = Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward;

        CC.Move(move * WalkingSpeed * Time.deltaTime);

        //Cam Rotation
        transform.Rotate(0, Input.GetAxis("Mouse X"), 0);

        //X Rotation of camera
        xRot -= Input.GetAxis("Mouse Y");

        xRot = Mathf.Clamp(xRot, -30f, 50f);
        CamPivot.eulerAngles = new Vector3(xRot, transform.eulerAngles.y, 0);

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.transform.CompareTag("Ground") &&  !isGrounded)
        {
            isGrounded = true;
        }
    }
}
