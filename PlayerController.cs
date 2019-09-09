using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float h = 0.0f;
    private float v = 0.0f;
    private float r = 0.0f;
    private float s= 0.0f;
    private Transform tr;
    public float moveSpeed;
    public float rotSpeed = 80.0f;
    public float jumpPower = 10.0f;
    Animator animator;
    void Start()
    {
        tr = GetComponent<Transform>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        r = Input.GetAxis("Mouse X");
        s = Input.GetAxis("Fire3");
        tr.Rotate(Vector3.up * rotSpeed * Time.deltaTime * r);
       
        //움직이지 않을때
        if (h == 0 && v == 0) animator.SetBool("Ismove", false);
        
        //움직일때
        else
        {
            moveSpeed = 1.0f;
            if (Input.GetKey( KeyCode.W))
            {
                animator.SetBool("Ismove", true);

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    moveSpeed = 5.0f;
                    animator.SetBool("Isrun", true);

                }
                else animator.SetBool("Isrun", false);
            }
            else
            {
                moveSpeed = 7.0f;
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    moveSpeed = 5.0f;
                    animator.SetBool("Isrun", true);

                }
                else animator.SetBool("Isrun", false);
            }    
            Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
            tr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.Self);
        }
    }
}
