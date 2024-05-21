using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera playerCamera;
    public float walkSpeed = 4f;
    public float runSpeed = 9f;
    public float jumpPower = 4f;
    public float gravity = 10f;
    public float sensivity = 2f;
    public float lookXLimit = 45f;
    private int pointsDeVie = 100;
    public HealthBar healthBarController;
    public GameOver gameOver;



    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;

    CharacterController characterController;

    public void SubirDegats(int degats)
    {
        pointsDeVie -= degats;
        healthBarController.UpdateHealthBar(pointsDeVie);

        if (pointsDeVie <= 0)
        {
            gameOver.ShowGameOver();
            canMove = false;
        }
        healthBarController.UpdateHealthBar(pointsDeVie);
    }


    void Start()
    {
   
        healthBarController = FindObjectOfType<HealthBar>();
        if (healthBarController == null)
        {
            Debug.LogError("Le script HealthBar n'a pas été trouvé dans la scène.");
        }
        else
        {
            healthBarController.SetMaxHealth(pointsDeVie);
            healthBarController.UpdateHealthBar(pointsDeVie);
        }

        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!canMove) { 
            return;
        }
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * sensivity;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * sensivity, 0);
        }
    }
}
