using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Public Variables
    [SerializeField] private float MoveSpeed = 4.0f;
    [SerializeField] private float DashSpeed = 10f; 
    [SerializeField] private float DashTime = 0.25f;
    #endregion

    #region Private Variables
    private Vector3 forward, right;

    private Animator anim;

    private Rigidbody rb;

    private float startTime;

    private bool Is_Moving = true;
    private bool Is_Dashing = false;

    #endregion


    void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);

        right = Quaternion.Euler(0, 90, 0) * forward;

        anim = GetComponent<Animator>();
        

        rb = GetComponent<Rigidbody>();
  
    }

   
    void Update()
    {
        if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0) 
        {
            Movement();
        }

        else
        {
            anim.SetBool("IS_Running", false);

        }

        if (Input.GetKey(KeyCode.Space))
        {
            SetDashing();
        }

        if(Is_Dashing == true)
        {
            DashForward();
          
        }

        if(startTime >= DashTime)
        {
            Is_Dashing = false;

            Is_Moving = true;

            startTime = 0f;

            anim.SetBool("IS_Dashing", false);

        }



    }

   

    private void Movement()
    {
       if(Is_Moving == true)
       {
            anim.SetBool("IS_Running", true);
            Vector3 rightMovement = right * MoveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
            Vector3 forwardMovement = forward * MoveSpeed * Time.deltaTime * Input.GetAxis("Vertical");

            Vector3 result = Vector3.Normalize(rightMovement + forwardMovement);

            transform.forward = result;
            transform.position += rightMovement;
            transform.position += forwardMovement;
       }
    }

    
    void SetDashing()
    {
        if(startTime == 0)
        Is_Dashing = true;


    }

    private void DashForward()
    {
        startTime += Time.deltaTime;

        transform.position += transform.forward * DashSpeed;

        Is_Moving = false;

        anim.SetBool("IS_Dashing", true);


    }
}
