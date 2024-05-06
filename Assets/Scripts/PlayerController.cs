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


    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool canMove = true;

    CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
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

        Tirer();
    }

    void Tirer()
    {
        if (Input.GetButtonDown("Fire1"))
        {

            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider.tag);
                Debug.Log(hit.collider.name);

                Rigidbody rb = hit.rigidbody;
                if (rb != null)
                {
                    Debug.Log("rigidbody touche");


                    int degats = 10;
                    if (hit.collider.CompareTag("Tete"))
                    {
                        // Multiplier les dégâts par 4 si c'est un tir à la tête
                        degats *= 4;
                        //  Debug.Log(degats);

                    }
                    // Vérifier si le tir a touché un bras ou une jambe
                    else if (hit.collider.CompareTag("Bras") || hit.collider.CompareTag("Jambe") || hit.collider.CompareTag("Corps"))
                    {
                        // Infliger seulement 25% des dégâts de base si c'est un tir dans un membre
                        degats = Mathf.RoundToInt(degats * 0.25f);

                    }
                       // Destroy le gameobject quand il a perdu 100pv
                    rb.GetComponent<Ennemi>().SubirDegats(degats);
                }
            }
            else
            {
                Debug.Log("Rien touché");
            }
        }
    }
}
