using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tails : MonoBehaviour
{
    public GameObject target;
    public int traceSpeed = 15;

    void Start()
    {
        
    }

    void Update()
    {
        // 타겟을 쫒아가고 싶다.
        // 1. 타겟(GameObject)을 설정한다.
        // 2. 타겟을 향한 방향을 계산한다.
        Vector3 dir = target.transform.position - transform.position;
        dir.Normalize();

        // 3. 계산된 방향과 지정된 속력으로 이동한다.
        transform.position += dir * traceSpeed * Time.deltaTime;
    }
}
