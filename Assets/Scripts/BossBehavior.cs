using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    // ���� �Ŀ� ������ ��ġ���� �Ʒ��� �̵��Ѵ�.
    // �ʷ� ���: �ӷ�, ����, ��ǥ ��ġ
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
        // 1. Lerp�� �̿��� ���
        //transform.position = Vector3.Lerp(transform.position, startPos.position, Time.deltaTime);
        // 1-1.
        //late += Time.deltaTime;
        //transform.position = Vector3.Lerp(starting, startPos.position, late * 0.3f);

        // 2. p = p0 + vt ����� �̿��� ���

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

        // ���� �� ���
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