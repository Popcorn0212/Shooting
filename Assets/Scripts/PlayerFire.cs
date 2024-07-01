using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject firePosition;
    //public GameObject[] firePositions;
    public AudioClip[] sounds;
    public List<GameObject> bullets = new List<GameObject>();
    public GameObject[] bulletArray;
    public int bulletCount = 10;
    public bool useObjectPool = false;
    public bool useArray = false;

    AudioSource audioSource;
    int bulletNumber = 0;

    void Start()
    {
        // AudioSource 캄포넌트를 가져오는 방법
        audioSource = transform.GetComponent<AudioSource>();

        if (useObjectPool)
        {
            // 총알 10개를 미리 만들어서 bullets 리스트에 추가한다.
            for (int i = 0; i < bulletCount; i++)
            {
                GameObject go = Instantiate(bulletPrefab);
                bullets.Add(go);
                go.GetComponent<BulletMove>().player = gameObject;

                // 생성된 총알을 비활성화한다.
                go.SetActive(false);
            }
        }

        if(useArray)
        {
            // 배열 변수의 크기를 지엉된 값으로 확정한다.
            bulletArray = new GameObject[bulletCount]; 

            // 배열의 각 번호에 총알 인스턴스를 생성해서 할당한다.
            for(int i =0; i < bulletCount; i++)
            {
                GameObject go = Instantiate(bulletPrefab);
                bulletArray[i] = go;
                go.SetActive(false);
                go.GetComponent<BulletMove>().player = gameObject;
            }

        }
    }

    void Update()
    {
        // 사용자가 마우스 왼쪽 버튼을 누르면 총알이 총구에 생성되게 하고 싶다!
        // 1. 사용자가 마우스 왼쪽 버튼을 누르는지 확인한다.
        if (Input.GetMouseButtonDown(0))
        {
            if(useArray)
            {
                // 오브젝트 풀 방식(배열 방식)으로 총알 사용
                ObjectPoolArrayType();
            }

            else if(useObjectPool)
            {
                // 오브젝트 풀 방식(리스트 방식)으로 총알 사용
                ObjectPoolType();
            }

            if(!useObjectPool && !useArray)
            {
                // 기본 방식으로 총알을 생성
                InstantiateType();
            }


            // 총알 발사음을 실행한다.
            audioSource.clip = sounds[0];
            audioSource.volume = 0.2f;
            audioSource.Play();
            //audioSource.Stop();
            //audioSource.Pause();
        }

        void InstantiateType()
        {
            // 2. 총알을 생성한다.
            GameObject go = Instantiate(bulletPrefab);

            // 3. 생성된 총알을 총구로 옮긴다.
            // 3-1. 총구를 게임 오브젝트 변수로 직접 지정하는 방법
            go.transform.position = firePosition.transform.position;
            go.transform.rotation = firePosition.transform.rotation;

            // 3-2. 플레이어의 위치에서 위로 1.5미터 지점을 지정하는 방법
            //Vector3 firePos = transform.position + new Vector3(0, 1.5f, 0);
            //go.transform.position = firePos;

            // 4. 생성된 총알의 BulletMove 컴포넌트에 있는 Player 변수에 자기 자신을 넣는다.
            go.GetComponent<BulletMove>().player = gameObject;
        }

        void ObjectPoolType()
        {
            // 0번 인덱스의 총알 오브젝트를 활성화한다.
            bullets[0].SetActive(true);

            // 활성화된 총알의 위치 및 회전을 총구와 일치시킨다.
            bullets[0].transform.position = firePosition.transform.position;
            bullets[0].transform.rotation = firePosition.transform.rotation;

            // 0번 인덱스의 총알 오브젝트를 리스트에서 제거한다.
            bullets.RemoveAt(0);
        }


        #region 두 개 이상 총알울 발사할 경우
        //if (Input.GetMouseButtonDown(0))
        //{
        //    for (int i = 0; i < firePosition.Length; i++)
        //    {
        //        // 2. 총알을 생성한다.
        //        GameObject go = Instantiate(bulletPrefab);

        //        // 3. 생성된 총알을 총구로 옮긴다.
        //        go.transform.position = firePositions[i].transform.position;
        //    }
        //}
        #endregion
    }

    void ObjectPoolArrayType()
    {
        for(int i = 0; i < bulletArray.Length; i++)
        {
            if (bulletArray[i] != null)
            {
                // 해당 인덱스의 총알 오브젝트가 활성화 상태인지를 확인한다.
                if (!bulletArray[i].activeInHierarchy)
                {
                    // i번째 인덱스 값에 해당하는 인덱스의 오브젝트를 활성화한다.
                    bulletArray[i].SetActive(true);

                    // 활성화된 총알의 위치 및 회전 값을 총구와 일치시킨다.
                    bulletArray[i].transform.position = firePosition.transform.position;
                    bulletArray[i].transform.rotation = firePosition.transform.rotation;

                    // 배열에서 해당 총알을 제거한다.
                    bulletArray[i] = null;
                    break;
                }
            }
        }

    }


    // 폭발 효과음을 실행하는 함수.
    public void PlayExplosionSound()
    {
        audioSource.clip = sounds[1];
        audioSource.volume = 0.5f;
        audioSource.Play();
    }

}
