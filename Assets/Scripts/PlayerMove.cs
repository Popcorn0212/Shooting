using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // 플레이어를 원하는 방향으로 이동한다.
    // 방향, 속력 = 속도(Vector)

    public float moveSpeed = 0.1f;
    //Vector3 direction;

    // < 숙제 >
    // 적 생성용 프리팹
    //public GameObject enemyPrefab;
    // 적을 생성할 시간
    //public float spawnTime = 1.0f;

    // 처음 실행되었을 때 한번만 실행되는 함수
    void Start()
    {
        //transform.position += Vector3.right;

        // < 숙제 >
        // 적 생성
        //Invoke("SpawnEnemy", spawnTime);
    }

    // 매 프레임마다 반복해서 실행하는 함수
    void Update()
    {
        #region 이동 공식 적용 방법
        //Vector3 direction = new Vector3(1, 1, 0);
        //transform.position += Vector3.right * moveSpeed;
        //transform.position += direction * moveSpeed;

        //print(transform.position);
        #endregion

        // 사용자의 입력 받기
        //float h = Input.GetAxis("Horizontal");
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(h, v, 0);
        // 벡터의 길이를 무조건 1로 바꾼다(정규화).
        direction.Normalize();

        // 이동 공식: p = p0 + vt
        transform.position += direction * moveSpeed * Time.deltaTime;
        //transform.eulerAngles += direction * moveSpeed * Time.deltaTime;
        //transform.localScale += direction * moveSpeed * Time.deltaTime;

        #region 사용자의 키 입력 이벤트
        // 특정 키를 입력했을 때 사용하는 함수(Get, GetDown, GetUp)
        //bool value = Input.GetButton("Horizontal");

        //if(Input.GetKey(KeyCode.F1))
        //{
        //    print("F1 키를 눌렀습니다!");
        //}
        #endregion


    }


    // 적과 충돌 시 플레이어와 적을 같이 제거한다.
    private void OnTriggerEnter(Collider en)
    {
        EnemyMove enemy = en.gameObject.GetComponent<EnemyMove>();

        // enemy 변수에 값이 있다면...
        if (enemy != null)
        {
            // 충돌한 게임 오브젝트를 제거한다.
            Destroy(en.gameObject);

        }

        // 나(총알)를 제거한다.
        Destroy(gameObject);
    }

    // < 숙제 >
    // 적 생성용
    //void SpawnEnemy()
    //{
    //    // 무작위 위치 지정
    //    Vector3 spawnPosition = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));
    //    // 적 생성
    //    Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    //}
}
