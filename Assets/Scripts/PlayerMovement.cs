using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController Player;
    private float PlayerSpeed = 20;
    private float verticalVelocity = 0.0f;
    private float startTime;
    private float score = 0.0f;
    private Vector3 moveVector;
    private float animationDuration = 1.5f;
    private Vector3 CrashPos;
    private float xDir;
    public GameObject Smoke;

    private bool isDead = false;
    private bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        //Smoke.SetActive(false);
        Player = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
            return;
        //Stopping player movement when camera animation takes place
        if (Time.time - startTime < animationDuration)
        {
            Player.Move(Vector3.forward * PlayerSpeed * Time.deltaTime * 0.5f);
            return;
        }

        //Resetting the value
        moveVector = Vector3.zero;

        //Calculating the X value (Left/Right)
        if (canMove)
        {
            //moveVector.x = Input.GetAxisRaw("Horizontal") * PlayerSpeed;
            xDir = Input.GetAxisRaw("Horizontal") * PlayerSpeed;
            moveVector.x = Mathf.Clamp(xDir, -4.0f, 4.0f);

            if(Input.GetMouseButton(0))
            {
                if (Input.mousePosition.x > Screen.width / 2)
                    moveVector.x = PlayerSpeed * 0.5f;
                else
                    moveVector.x = -PlayerSpeed * 0.5f;
            }
        }
        //Calculating the Z value (Forward/Backward)
        moveVector.z = PlayerSpeed;

        Player.Move(moveVector * Time.deltaTime);
    }

    public void SetSpeed(float speedModifier)
    {
        PlayerSpeed += speedModifier;
    }

    //Called whenever player controller hits 
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Enemy")
        {
            canMove = false;
            Death();
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Collectable"))
        {
            //col.gameObject.SetActive(false);
            GetComponent<Score>().Collectable();
        }
    }

    private void Death()
    {
        CrashPos = transform.position;
        for (int i = 0; i < 1; i++)
        {
            Instantiate(Smoke, CrashPos, Quaternion.identity);
        }
        Debug.Log("Ending smoke on Death()");
       
        isDead = true;
        GetComponent<Score>().OnDeath();
    }
}