using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // 플레이어를 원하는 방향으로 이동한다.
    // 방향, 속력 = 속도(Vector)

    public float moveSpeed = 0.1f;
    public float rotSpeed = 100;
    //Vector3 direction;

    float rotY;
    Animator myAnim;

    // < 숙제 >
    // 적 생성용 프리팹
    //public GameObject enemyPrefab;
    // 적을 생성할 시간
    //public float spawnTime = 1.0f;

    // 처음 실행되었을 때 한번만 실행되는 함수
    void Start()
    {
        // 나에게 붙어있는 Animator 컴포넌트 가져오기
        myAnim = GetComponent<Animator>();
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

        // 기체의 회전: r = r0 + vt
        rotY += (h * -1) * rotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, rotY, 0); 

        // 만일, 사용자의 좌우 입력이 없다면...
        float inputH = Mathf.Abs(h);
        if(inputH < 0.1f)
        {
            // 기체의 회전값을 다시 원래대로 돌려놓는다.
            //transform.eulerAngles = Vector3.zero;

            #region 1. 오일러 앵글을 직접 사용하는 방법
            //if (Mathf.Abs(transform.eulerAngles.y) > 1.0f)
            //{
            //    if (transform.eulerAngles.y < 0)
            //    {
            //        rotY += rotSpeed * Time.deltaTime;
            //    }
            //    else
            //    {
            //        rotY -= rotSpeed * Time.deltaTime;
            //    }

            //    transform.eulerAngles = new Vector3(0, rotY, 0);
            //}
            //else
            //{
            //    transform.eulerAngles = Vector3.zero;
            //}
            #endregion

            // 2. Lerp를 이용한 방법
            rotY = Mathf.Lerp(rotY, 0, Time.deltaTime * 2);
            transform.eulerAngles = new Vector3(0, rotY, 0);
        }

        #region 3. 애니메이터를 사용한 회전 애니메이션 주기
        //if(h > 0.2f)
        //{
        //    myAnim.SetTrigger("RightMove");
        //}
        //else if(h < -0.2f)
        //{
        //    myAnim.SetTrigger("LeftMove");
        //}
        //else
        //{
        //    myAnim.SetTrigger("Idle");
        //}
        #endregion

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

}
