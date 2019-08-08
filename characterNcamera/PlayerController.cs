using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
/*public class Playeranim
{
    public AnimationClip idle;
    public AnimationClip pick;
    public AnimationClip walk;
    public AnimationClip run;
    public AnimationClip attack_s1;
    public AnimationClip attack_s2;
    public AnimationClip jump;
    public AnimationClip death;
}*/


public class PlayerController : MonoBehaviour
{
    private float h = 0.0f;
    private float v = 0.0f;
    private float r = 0.0f;
    private float p = 0.0f;
    private Transform tr;
    public float moveSpeed = 10.0f;
    public float rotSpeed = 80.0f;
    public float jumpPower = 10.0f;

    /*public Playeranim playeranim;
    [HideInInspector]
    public Animation anim; */
 
    void Start()
    {
        tr = GetComponent<Transform>();
      // anim = GetComponent<Animation>();
      //  anim.clip = playeranim.idle;
       // anim.Play();
    }

    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        r = Input.GetAxis("Mouse X");
        p = Input.GetAxis("Mouse Y");

        Debug.Log("h=" + h.ToString());
        Debug.Log("v=" + v.ToString());

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        tr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.Self);
        tr.Rotate(Vector3.up * rotSpeed * Time.deltaTime * r);
    }
}
