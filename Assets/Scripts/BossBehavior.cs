using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    // 등장 후에 지정된 위치까지 아래로 이동한다.
    // 필료 요소: 속력, 방향, 목표 위치
    public float appearSpeed = 3;
    public Transform startPos;
    Vector3 starting;
    float late = 0;

    void Start()
    {
        starting = transform.position;

    }

    void Update()
    {
        // 1. Lerp를 이용한 방법
        //transform.position = Vector3.Lerp(transform.position, startPos.position, Time.deltaTime);
        // 1-1.
        //late += Time.deltaTime;
        //transform.position = Vector3.Lerp(starting, startPos.position, late * 0.3f);

        // 2. p = p0 + vt 방식을 이용한 방법

        //if (transform.position.y < startPos.position.y)
        Vector3 dir = startPos.position - transform.position;
        if (dir.magnitude < 0.1f)
        {
            transform.position = startPos.position;
        }
        else
        {
            dir.Normalize();
            transform.position += dir * appearSpeed * Time.deltaTime;
        }

        // 내가 쓴 방법
        //transform.position += Vector3.down * appearSpeed * Time.deltaTime;
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.name == "BossStartPos")
    //    {
    //        appearSpeed = 0;
    //    }
    //}
}
