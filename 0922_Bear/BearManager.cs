﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearManager : MonoBehaviour
{
    public Transform point;
    public GameObject BearObject;
    public GameObject BearPrefab;
    public float createTime;
    public int Maxbear = 1;
    public bool isGameOver = false;
    Animator animator;

    private void Start()
    {
        point = GameObject.Find("Bear_point").GetComponentInChildren<Transform>();
        if (point != null)
        {
            StartCoroutine(this.CreateBear());
        }

    }
    IEnumerator CreateBear()
    {
        while (!isGameOver)
        {
            int BearCount = (int)GameObject.FindGameObjectsWithTag("Bear").Length;
            if (BearCount < Maxbear)
            {
                yield return new WaitForSeconds(createTime);
                BearObject = (GameObject) Instantiate(BearPrefab, point.position, point.rotation)as GameObject;
            }
            else
            {
                yield return null;
            }
        }
    }
    public void Dead()
    {
        animator = BearObject.GetComponent<Animator>();
        animator.SetBool("IsDead", true);
        Destroy(this.BearObject,3f);
    }
    public void Attack()
    {
        animator = BearObject.GetComponent<Animator>();
        animator.SetBool("IsAttack", true);
        Destroy(this.BearObject, 1.4f);
    }

}
