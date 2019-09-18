using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearManager : MonoBehaviour
{
    public Transform point;
    public GameObject BearPrefab;
    public float createTime;
    public int Maxbear = 1;
    public bool isGameOver = false;
    Animator animator;

    private void Start()
    {
        point = GameObject.Find("Bear_point").GetComponentInChildren<Transform>();
        animator = GetComponent<Animator>();

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
                Instantiate(BearPrefab, point.position, point.rotation);
            }
            else
            {
                yield return null;
            }
        }
    }
    public void Dead()
    {
        animator.SetBool("IsDead", true);
        Destroy(this.BearPrefab, 2f);
    }

}
