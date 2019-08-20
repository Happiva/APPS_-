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
    public float moveSpeed = 3.0f;
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
        
        //움직일떄
        else
        {
            //쉬프트를 누르지 않았을 떄
            if (s == 0)
            {
                moveSpeed = 3.0f;
                animator.SetBool("Isrun", false);
            }
            //쉬프트를 눌렀을 때
            else
            {
                moveSpeed = 5.0f;
                animator.SetBool("Isrun", true);
            }
            animator.SetBool("Ismove", true);
            Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
            tr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.Self);
        }
    }
}
