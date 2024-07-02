using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{

    public float moveSpeed = 10;
    public GameObject player;
    public float lifeSpan = 3.0f;
    public GameObject explosionPrefab;

    PlayerFire pFire;

    void Start()
    {
        // Player 오브젝트에 PlayerFire 컴포넌트를 변수에 저장한다.
        if (player != null)
        {
            pFire = player.GetComponent<PlayerFire>();
        }
    }

    void Update()
    {
        // 위로 계속 이동하고 싶다.
        // 방향: 위로, 이동속력: float, public
        // 이동공식: p = p0 + vt, p += vt

        // 월드 방향 
        Vector3 worldDir = Vector3.up;

        // 로컬 방향
        Vector3 localDir = transform.up;

        transform.position += localDir * moveSpeed * Time.deltaTime;
        //transform.position += new Vector3(0, 1, 0) * moveSpeed * Time.deltaTime;

        //if(gameObject != null)
        //{
        //    lifeSpan += Time.deltaTime;
        //    if (lifeSpan > 3)
        //    {
        //        Destroy(gameObject);
        //    }
        //}

        lifeSpan -= Time.deltaTime;
        if(lifeSpan < 0)
        {
            if (pFire.useObjectPool || pFire.useArray)
            {
                Reload();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    // 물리적 충돌이 발생했을 때 실행되는 이벤트 함수.
    private void OnCollisionEnter(Collision collision)
    {
        // 충돌한 게임 오브젝트를 제거한다.
        Destroy(collision.gameObject);

        // 나(총알)를 제거한다.
        Destroy(gameObject);
    }

    // 물리적 충돌 없이 충돌 감지만 했을 때 실행되는 이벤트 함수.
    private void OnTriggerEnter(Collider other)
    {
        EnemyMove enemy = other.gameObject.GetComponent<EnemyMove>();

        // enemy 변수에 값이 있다면...
        if (enemy != null)
        {
            // 충돌한 에너미 오브젝트를 제거한다.
            Destroy(other.gameObject);

            // GameManager에 있는 currentScore 값을 1 추가한다.
            GameManager.gm.AddScore(1);

            // 폭발 이펙트 프리팹를 애너미가 있던 자리에 생성한다.
            GameObject fx = Instantiate(explosionPrefab, other.transform.position, other.transform.rotation);

            // 생성한 폭발 이펙트 오브젝트에서 파티클 시스템 컴포넌트를 가져와서 플레이한다.
            ParticleSystem ps = fx.GetComponent<ParticleSystem>();
            ps.Play();

            if (player != null)
            {
                // 플레이어 게임 오브젝트에 붙어있는 PlayerFire 컴포넌트를 가져온다.
                PlayerFire playerFire = player.GetComponent<PlayerFire>();

                // PlayerFire 컴포넌트에 있는 PlayExplosionSound 함수를 실행한다.
                playerFire.PlayExplosionSound();
            }
        }

        // 나를 제거한다.
        if(pFire.useObjectPool || pFire.useArray)
        {
            Reload();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Reload()
    {
        if (pFire.useObjectPool)
        {
            // 자기 자신을 bullets 리스트에 추가하고, 비활성화한다.
            pFire.bullets.Add(gameObject);
            lifeSpan = 3.0f;
            gameObject.SetActive(false);
            if (player != null)
            {
                gameObject.transform.parent = player.transform;
            }
        }
        else if(pFire.useArray)
        {
            // bulletArray 배열의 빈 값이 있는 곳을 찾는다.
            for(int i = 0; i < pFire.bulletArray.Length; i++)
            {
                // 만일, i번째 인덱스의 값이 null이라면...
                if (pFire.bulletArray[i] == null)
                {
                    pFire.bulletArray[i] = gameObject;
                    gameObject.SetActive(false);
                    lifeSpan = 3.0f;
                    if (player != null)
                    {
                        gameObject.transform.parent = player.transform;
                    }
                    break;
                }
            }
        }
    }
    
}
