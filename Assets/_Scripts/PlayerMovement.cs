using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]     //Add character Controller automatic where we attach the script
[RequireComponent(typeof(Animator))]    //Add animator in that object where the script attach
public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    Vector3 direction;
    public float moveSpeed;
    private CharacterController characterController;
    private Animator anim;
    int desiredLane = 0;
    public float lane_Distance = 4;
    public float jumpForce;
    public float gravity= -20f;
    private float maxSpeed = 55f;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

   
    void Update()
    {
       
        if (!UiManager.isGameStarted)
            return;
        anim.SetBool("isGameStarted", true);
        if (maxSpeed > moveSpeed)
        {

       moveSpeed += 0.1f * Time.deltaTime;
        }
        direction.z = moveSpeed;
        UiManager.instance.ScoreManager();
        
       // if (Input.GetKeyDown(KeyCode.RightArrow))
       if(SwipeManager.swipeRight)
        {
            desiredLane++;
            if (desiredLane == 3)
                desiredLane = 1;
        }
        //if (Input.GetKeyDown(KeyCode.LeftArrow))
        if(SwipeManager.swipeLeft)
        {
            desiredLane--;
            if (desiredLane == -1)
                desiredLane = 0;
        }
        if (characterController.isGrounded)
        {
            //if (Input.GetKeyDown(KeyCode.UpArrow))
            if(SwipeManager.swipeUp)
            {
                StartCoroutine(Jump());
            }
            if (SwipeManager.swipeDown)
            {
               StartCoroutine(Slide());
                //anim.Play("Characetr_Slide");
            }
            
        }
        else
        {
            if (!UiManager.isGameStarted)
                return;

            direction.y += gravity * Time.deltaTime;
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (desiredLane == 0)
        {
            targetPosition += Vector3.left*lane_Distance;
        }
        else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * lane_Distance;
        }
        transform.position = targetPosition;
        characterController.center = characterController.center;
        transform.position = Vector3.Lerp(transform.position, targetPosition, 80 * Time.deltaTime);
    }
    private void FixedUpdate()
    {
        characterController.Move(direction * Time.fixedDeltaTime);
    }
  
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Obstaclas")
        {
            UiManager.gameOver = true;
            UiManager.instance.GameOver();
            Destroy(this.gameObject.GetComponent<PlayerMovement>());
        }
    }
    public IEnumerator Slide()
    {
        anim.SetBool("isSliding", true);
        characterController.center = new Vector3(0, 0.3f, 0.12f);
        characterController.height = 1f;
        yield return new WaitForSeconds(0.8f);
        anim.SetBool("isSliding", false);
        characterController.center = new Vector3(0, 0.7f, 0.12f);
        characterController.height = 1.5f;
    }
    private IEnumerator Jump()
    {
        direction.y = jumpForce;
        anim.SetBool("isJump", true);
        yield return new WaitForSeconds(1f);
        anim.SetBool("isJump", false);
    }
}
