/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float h = 0.0f;
    private float v = 0.0f;
    private float r = 0.0f;

    private Transform tr;
    public float moveSpeed = 10.0f;
    public float rotSpeed = 80.0f;
    public float jumpPower = 10.0f;

    void Start()
    {
        tr = GetComponent<Transform>();
    }

    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        r = Input.GetAxis("Mouse X");

        Debug.Log("h=" + h.ToString());
        Debug.Log("v=" + v.ToString());

        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
        tr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.Self);
        tr.Rotate(Vector3.up * rotSpeed * Time.deltaTime * r);
    }
}*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float h = 0.0f;
    private float v = 0.0f;
    private float r = 0.0f;
    private float s = 0.0f;
    private Transform tr;
    public float moveSpeed = 3.0f;
    public float rotSpeed = 80.0f;
    public float jumpPower = 10.0f;
    Animator animator;

    private GameObject closest_item = null;
    private GameObject carried_item = null;
    private ItemRoot item_root = null;

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
        //여기서 부터 추가한 부분------------다연//
        this.pickup();
    }

    void OnTriggerStay(Collider other)
    {
        GameObject other_go = other.gameObject;
        if (other_go.layer == LayerMask.NameToLayer("Item"))
        {
            if (this.closest_item == null)
            {
                if (this.is_other_in_view(other_go))
                {
                    this.closest_item = other_go;
                }
            }
            else if (this.closest_item == other_go)
            {
                if (!this.is_other_in_view(other_go))
                {
                    this.closest_item = null;
                }
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(this.closest_item == other.gameObject)
        {
            this.closest_item = null;
        }
    }

    private void pickup()
    {
        do
        {
            Debug.Log("I pick up");
            if (this.carried_item == null)
            {
                if (this.closest_item == null)
                {
                    Debug.Log("closest_item null");
                    break;
                }
                Debug.Log("주울수있을까");
                this.carried_item = this.closest_item;
                GameObject.Destroy(this.carried_item);
                this.closest_item = null;
            }
        } while (false);
    }
    
    private bool is_other_in_view(GameObject other)
    {
        bool ret = false;
        do
        {
            Vector3 heading = this.transform.TransformDirection(Vector3.forward);
            Vector3 to_other = other.transform.position - this.transform.position;
            heading.y = 0.0f;
            to_other.y = 0.0f;
            heading.Normalize();
            to_other.Normalize();
            float dp = Vector3.Dot(heading, to_other);
            if (dp < Mathf.Cos(45.0f))
            {
                break;
            }
            ret = true;
        } while (false);
        return ret;
    }
}
