﻿using System.Collections;
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

    private Inventory inventory;
    private GameObject closest_item = null;
    private GameObject carried_item = null;
    private ItemRoot item_root = null;
    public GUIStyle guistyle;

    //현빈 0917
    private Player player;

    private GameObject closest_event = null;
    private EventRoot event_root = null;
    private GameObject ship_model = null;
    public float step_timer = 0.0f;

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
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();

        //현빈 0917
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        //다연
        this.item_root = GameObject.Find("Food").GetComponent<ItemRoot>();

        this.guistyle.fontSize = 14;

        this.event_root = GameObject.Find("GameRoot").GetComponent<EventRoot>();

        this.ship_model = GameObject.Find("Ship").transform.gameObject;
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

        if (Input.GetKey(KeyCode.LeftShift))
        {
            //현빈 0917
            player.isRun = true;
        }
        else {
            //현빈 0917
            //Debug.Log("집가고 싶다");
            player.isRun = false;
        }

        //움직이지 않을 때
        if (h == 0 && v == 0) {
            animator.SetBool("Ismove", false);

            //현빈 0920
            player.isMove = false;
        }

        //움직일 때
        else
        {
            moveSpeed = 1.0f;
            if (Input.GetKey(KeyCode.W))
            {
                animator.SetBool("Ismove", true);
                //현빈 0920
                player.isMove = true;

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    moveSpeed = 5.0f;
                    animator.SetBool("Isrun", true);
                }
                else
                {
                    animator.SetBool("Isrun", false);
                }
            }
            else
            {
                moveSpeed = 7.0f;
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    moveSpeed = 5.0f;
                    animator.SetBool("Isrun", true);
                }
                else
                {
                    animator.SetBool("Isrun", false);
                }
            }
            Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);
            tr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.Self);
        }
        //여기서 부터 추가한 부분------------다연//
        this.get_input();
        this.pick_or_drop_control(); // 함수 이름 수정함, 2019.09.01, 다연
        this.step_timer += Time.deltaTime;
        float repair_time = 2.0f;

        if (this.closest_event != null)
        {
            if (!this.is_event_ignitable())
            {
                // break;
            }

            Event.TYPE ignitable_event = this.event_root.getEventType(this.closest_event);
            switch (ignitable_event)
            {
                case Event.TYPE.SHIP:
                    if (this.step_timer > repair_time)
                    {
                        break;
                    }
                    break;
            }
            //break;
        }
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
        else if (other_go.layer == LayerMask.NameToLayer("Event"))
        {
            if (this.closest_event == null)
            {
                if (this.is_other_in_view(other_go))
                {
                    this.closest_event = other_go;
                }
            }
            else if (this.closest_event == other_go)
            {
                if (!this.is_other_in_view(other_go))
                {
                    this.closest_event = null;
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
                    PutInInventory(carried_item);
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

    void PutInInventory(GameObject item) {
        if (item.CompareTag("Potato")) {
            inventory.amount[0]++;
            inventory.amountText[0].text = inventory.amount[0] + "";
        }
        if (item.CompareTag("Crab"))
        {
            inventory.amount[1]++;
            inventory.amountText[1].text = inventory.amount[1] + "";
        }
        if (item.CompareTag("Tree"))
        {
            inventory.amount[2]++;
            inventory.amountText[2].text = inventory.amount[2] + "";
        }
        if (item.CompareTag("Corn"))
        {
            inventory.amount[3]++;
            inventory.amountText[3].text = inventory.amount[3] + "";
        }
    }

    private bool is_event_ignitable()
    {
        bool ret = false;
        do
        {
            if (!this.closest_event == null)
            {
                break;
            }

            if (inventory.amount[2] < 20) //tree개수 20보다 작을경우
            {
                break;
            }

            ret = true;
        } while (false);
        return (ret);

    }
}
