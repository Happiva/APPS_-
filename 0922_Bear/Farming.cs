using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Farming : MonoBehaviour
{
    public GameObject obj = null;
    public GameObject bear = null;
    public Text bear_text = null;
    private bool IsAttack = false;
    private void Start()
    {
        GameObject obj = GameObject.Find("Bear_text");
        bear_text = obj.GetComponent<Text>();
        bear_text.enabled = false;
    }
    private void Update()
    {
        int BearChance = Random.Range(1, 10);
        if (IsAttack == true && Input.GetKeyDown(KeyCode.Z))
        {
            if(BearChance > 5)
            {
                bear_text.text = "곰을 죽였습니다.";
                GameObject.Find("GameManager").GetComponent<BearManager>().Dead();
            }
            else
            {
                bear_text.text = "곰을 죽이는 데 실패하였습니다.";
                GameObject.Find("GameManager").GetComponent<BearManager>().Attack();
                GameObject.Find("Player").GetComponent<Player>().Damage(50);
            }

        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Bear")
        {
            bear_text.enabled = true;
            IsAttack = true;
        }
    }
    void OnTriggerExit (Collider other)
    {
        if (other.gameObject.tag == "Bear")
        {
            bear_text.enabled = false;
        }
    }
}
