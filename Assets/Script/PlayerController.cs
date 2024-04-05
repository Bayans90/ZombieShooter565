using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public Vector2 turn;
    public CharacterController characterController;

    public Weapons weapons;
    private int currentMelee;
    private int currentGun;

    public GameObject myHead;
    public GameObject myCamera;
    public GameObject upperBody;

    float velocityZ = 0.0f;
    float velocityX = 0.0f;
    public float acceleration = 10.0f;
    public float deceleration = 10.0f;
    public float maxWalkVelocity = 1.0f;
    public float maxRunVelocity = 2.0f;
    public float jumpSpeed;
    public float ySpeed;
    public float speed;


    public Rig rigAim;
    public float targetRigWeight;
    
 

    public Vector3 prevSpeed;

    bool isGrounded;
    bool isJumping;
    bool isMoving;
    bool isCombatMode = false;


    public GameObject target;

    void Awake()
    {
        animator = GetComponent<Animator>();

        characterController = GetComponent<CharacterController>();
        currentMelee = -1;
        currentGun = -1;
        targetRigWeight = 0f;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }



    void Update()
    {
        //Key Inputs
        bool forwardPressed = Input.GetKey("w");
        bool leftPressed = Input.GetKey("a");
        bool rightPressed = Input.GetKey("d");
        bool backwardPressed = Input.GetKey("s");
        bool runPressed = Input.GetKey("left shift");

        bool rightMouse = Input.GetKey(KeyCode.Mouse1);


        //if runPressed is true then use maxRunVelocity, if not then use maxWalkVelocity
        float currentMaxVelocity = runPressed ? maxRunVelocity : maxWalkVelocity;


        //is the character moving (walking or running) ????
        //useful when landing to know whether the animation should go to a moving animation or idle
        if (forwardPressed || leftPressed || rightPressed || backwardPressed)
        {
            animator.SetBool("isMoving", true);
            isMoving = true;
        }
        else
        {
            animator.SetBool("isMoving", false);
            isMoving = false;
        }



        //If any of the "wasd" keys are pressed, it will increase the magnitude of velocityX and VelocityZ
        if (forwardPressed && velocityZ < currentMaxVelocity)
        {
            velocityZ += Time.deltaTime * acceleration;
        }
        if (leftPressed && velocityX > -currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * acceleration;
        }
        if (rightPressed && velocityX < currentMaxVelocity)
        {
            velocityX += Time.deltaTime * acceleration;
        }
        if (backwardPressed && velocityZ > -currentMaxVelocity)
        {
            velocityZ -= Time.deltaTime * acceleration;
        }

        //If any of the "wasd" keys ARE not pressed, it will decrease the magnitude of velocityX and velocityZ
        if (!forwardPressed && velocityZ >= 0.0f)
        {
            velocityZ -= Time.deltaTime * acceleration;
        }
        if (!leftPressed && velocityX <= 0.0f)
        {
            velocityX += Time.deltaTime * acceleration;
        }
        if (!rightPressed && velocityX >= 0.0f)
        {
            velocityX -= Time.deltaTime * acceleration;
        }
        if (!backwardPressed && velocityZ <= 0.0f)
        {
            velocityZ += Time.deltaTime * acceleration;
        }


        //IF a directional key and running key are pressed while the current velocity is greater
        //than the currentMaxVelocity, set the velocity to the currentMax Velocity
        //
        //ELSE a directional key is pressed and a running key is not pressed, descelerate velocity to the walking velocity
        if (forwardPressed && runPressed && velocityZ > currentMaxVelocity)
        {
            velocityZ = currentMaxVelocity;
        }
        else if (forwardPressed && velocityZ > currentMaxVelocity)
        {
            velocityZ -= Time.deltaTime * deceleration;
        }
        if (backwardPressed && runPressed && velocityZ < -currentMaxVelocity)
        {
            velocityZ = -currentMaxVelocity;
        }
        else if (backwardPressed && velocityZ < -currentMaxVelocity)
        {
            velocityZ += Time.deltaTime * deceleration;
        }
        if (rightPressed && runPressed && velocityX > currentMaxVelocity)
        {
            velocityX = currentMaxVelocity;
        }
        else if (rightPressed && velocityX > currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * deceleration;
        }
        if (leftPressed && runPressed && velocityX < -currentMaxVelocity)
        {
            velocityX = -currentMaxVelocity;
        }
        else if (leftPressed && velocityX < -currentMaxVelocity)
        {
            velocityX += Time.deltaTime * deceleration;
        }
        if (velocityX > currentMaxVelocity)
        {
            velocityX = currentMaxVelocity;
        }




        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("isJumping", true);
            isJumping = true;
            ySpeed = jumpSpeed;
            isGrounded = false;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool("isJumping", false);
        }



        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (isJumping && !isGrounded)
        {
            Vector3 velocity = new Vector3(horizontalInput * currentMaxVelocity, ySpeed, verticalInput * currentMaxVelocity);
            Vector3 worldVelocity = transform.TransformDirection(velocity);
            ySpeed += Physics.gravity.y * Time.deltaTime;
            characterController.Move(worldVelocity * Time.deltaTime);
            if (isGrounded)
            {
                isJumping = false;
            }
        }


        if (characterController.velocity.x != 0f || characterController.velocity.y != 0f || characterController.velocity.z != 0f)
        {
            isGrounded = characterController.isGrounded;
            animator.SetBool("isGrounded", isGrounded);
        }
        if(characterController.velocity == new Vector3(0f, 0f, 0f))
        {
            animator.SetBool("isGrounded", true);
            isGrounded = true;
        }



        animator.SetFloat("VelocityZ", velocityZ);
        animator.SetFloat("VelocityX", velocityX);
        //Debug.Log("animator speed: " + animator.velocity);

        turn.x = Input.GetAxis("Mouse X");
        turn.y = Input.GetAxis("Mouse Y");
        RotatePlayer();
        changeMelee();


        if(currentMelee == -1)
        {
            animator.SetBool("isMelee", false);
        } else {
            animator.SetBool("isMelee", true);
        }


        if(currentGun ==-1)
        {
            animator.SetBool("isHoldingPistol", false);
        }
        else
        {
            animator.SetBool("isHoldingPistol", true);
        }


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetBool("leftMouse", true);
        }
        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            animator.SetBool("leftMouse", false);
        }




        if(currentGun > -1 && rightMouse)
        {
            targetRigWeight = 1f;
        }
        else
        {
            targetRigWeight = 0f;
        }


        rigAim.weight = Mathf.Lerp(rigAim.weight, targetRigWeight, Time.deltaTime * 20f);



    }




    void RotatePlayer()
    {
        // Rotate the player based on mouse input horizontally
        float horizontalRotation = turn.x * 5f;
        transform.Rotate(Vector3.up, horizontalRotation);

        // Rotate the camera and head based on mouse input vertically
        float verticalRotation = -turn.y * 5f; // Inverted vertical rotation
        float currentRotation = myCamera.transform.localEulerAngles.x; // Get current vertical rotation

        // Calculate the new vertical rotation
        float newRotation = currentRotation + verticalRotation;

        if (newRotation > 330f || newRotation < 30f)
        {
            // Apply the new rotation to the camera
            myCamera.transform.localRotation = Quaternion.Euler(newRotation, 0f, 0f);

            // Ensure head's Y and Z rotations remain fixed
            Quaternion fixedHeadRotation = Quaternion.Euler(-newRotation, -90f, 180f);
            if (myHead != null)
            {
                myHead.transform.localRotation = fixedHeadRotation;
            }
        }
    }


    void changeMelee()
    {
        int totalNumMelee = weapons.melee.Length;
        int totalNumGuns = weapons.guns.Length;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if(currentMelee == -1)
            {
                currentMelee++;
                weapons.melee[currentMelee].SetActive(true);
            }
            else if(currentMelee == totalNumMelee-1)
            {
                weapons.melee[currentMelee].SetActive(false);
                currentMelee = -1;
            }
            else
            {
                weapons.melee[currentMelee].SetActive(false);
                currentMelee++;
                weapons.melee[currentMelee].SetActive(true);
            }

          
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            if (currentGun == -1)
            {
                currentGun++;
                weapons.guns[currentGun].SetActive(true);
            }
            else if (currentGun == totalNumGuns - 1)
            {
                weapons.guns[currentGun].SetActive(false);
                currentGun = -1;
            }
            else
            {
                weapons.guns[currentGun].SetActive(false);
                currentGun++;
                weapons.guns[currentGun].SetActive(true);
            }
        }
    }
}





//void MoveForward(float moveSpeed)
//{
//    transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
//}
//void MoveRight(float moveSpeed)
//{
//    transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
//}
//void MoveLeft(float moveSpeed)
//{
//    transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
//}
//void MoveBackward(float moveSpeed)
//{
//    transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
//}
//void JumpUp(float jumpForce)
//{
//    GetComponent<Rigidbody>().velocity = new Vector3(0, jumpForce, 0);
//}





/* Old 
     if (Input.GetKey(KeyCode.W))
    {
        animator.SetBool("isWalking", true);
    }
    if(!Input.GetKey(KeyCode.W))
    {
        animator.SetBool("isWalking", false);
    }
    if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
    {
        animator.SetBool("isRunning", true);
    }
    if (!Input.GetKey(KeyCode.LeftShift))
    {
        animator.SetBool("isRunning", false);
    }
    if(Input.GetKeyUp(KeyCode.F))
    {
        if (!isCombatMode)
        {
            animator.SetBool("isCombatMode", true);
            isCombatMode = true;
        }
        else
        {
            animator.SetBool("isCombatMode", false);
            isCombatMode = false;
        }
    }
    if(Input.GetKeyDown(KeyCode.Mouse0))
    {
        animator.SetBool("isAttacking", true);
    }
    if (Input.GetKeyUp(KeyCode.Mouse0))
    {
        animator.SetBool("isAttacking", false);
    }
    if (Input.GetKey(KeyCode.A))
    {
        animator.SetBool("isWalkingLeft", true);
    }
    if (!Input.GetKey(KeyCode.A))
    {
        animator.SetBool("isWalkingLeft", false);
    }  
 */