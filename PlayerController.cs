using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private GameObject closest_item = null;
    private GameObject carried_item = null;
    private ItemRoot item_root = null;
    public GUIStyle guistyle;

    private struct Key
    {
        public bool pick;
        public bool action;
    }

    private Key key;

    void Start()
    {
        tr = GetComponent<Transform>();
        animator = GetComponent<Animator>();

        //다연
        this.item_root = GameObject.Find("Food").GetComponent<ItemRoot>();

        this.guistyle.fontSize = 14;
    }

    private void get_input()
    {
        this.key.pick = Input.GetKeyDown(KeyCode.Z);

    }

    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        r = Input.GetAxis("Mouse X");
        s = Input.GetAxis("Fire3");
        tr.Rotate(Vector3.up * rotSpeed * Time.deltaTime * r);
       
        //움직이지 않을 때
        if (h == 0 && v == 0) animator.SetBool("Ismove", false);
        
        //움직일 때
        else
        {
            moveSpeed = 1.0f;
            if (Input.GetKey(KeyCode.W))
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
        //여기서 부터 추가한 부분------------다연//
        this.get_input();
        this.pick_or_drop_control(); // 함수 이름 수정함, 2019.09.01, 다연
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
        if (this.closest_item == other.gameObject)
        {
            this.closest_item = null;
        }
    }

    //gui함수 9.1 추가, 다연 //
    void OnGUI()
    {

        if (this.carried_item != null)
        {
            GUI.Box(new Rect(360, 200, 100, 40), "Choose");
            GUI.Button(new Rect(360, 230, 100, 90), "Z : 버린다", guistyle);
        }
        else
        {
            if (this.closest_item != null)
            {
                GUI.Box(new Rect(340, 210, 100, 40), "Choose");
                GUI.Button(new Rect(360, 230, 100, 90), "Z : 줍는다", guistyle);
            }
        }
    }

    private void pick_or_drop_control() //함수 이름 수정, 2019.09.01
    {
        do
        {
            if (!this.key.pick)
            {
                break;
            }
            else
            {
                if (this.carried_item == null)
                {
                    if (this.closest_item == null)
                    {
                        break;
                    }

                    this.carried_item = this.closest_item;
                    GameObject.Destroy(this.carried_item);
                    this.closest_item = null;
                }

                else
                {
                    this.carried_item = null;
                }
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

