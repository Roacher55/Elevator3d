using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    private float speedMovement = 4f;
    private float speedAdditional = 2f;
    private float vertical = 0f;
    private float horizontal = 0f;
    private float currentHeight = 0f;
    private float jumpHeight = 2f;

    private float sensitivityMouse = 3f;
    private float horizontalMouse = 0f;
    private float verticalMouse = 0f;
    private float rangeMouse = 90f;



    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        KeyboardControler();
        MouseController();

    }

    void KeyboardControler()
    {
        vertical = Input.GetAxis("Vertical") * speedMovement;
        horizontal = Input.GetAxis("Horizontal") * speedMovement;

        if (Input.GetButton("Jump") && characterController.isGrounded)
        {
            currentHeight = jumpHeight;
        }
        else if (!characterController.isGrounded)
        {
            currentHeight += Physics.gravity.y * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speedMovement += speedAdditional;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speedMovement -= speedAdditional;
        }
        Vector3 move = new Vector3(horizontal, currentHeight, vertical);
        move = transform.rotation * move;
        characterController.Move(move * Time.deltaTime);
    }

    void MouseController()
    {
        verticalMouse -= Input.GetAxis("Mouse Y") * sensitivityMouse;
        horizontalMouse = Input.GetAxis("Mouse X") * sensitivityMouse;
        transform.Rotate(0f, horizontalMouse, 0f);
        verticalMouse = Mathf.Clamp(verticalMouse, -rangeMouse, rangeMouse);
        Camera.main.transform.localRotation = Quaternion.Euler(verticalMouse, 0f, 0f);
    }


}