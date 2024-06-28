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
    
    AudioSource audioSource;

    void Start()
    {
        // AudioSource 캄포넌트를 가져오는 방법
        audioSource = transform.GetComponent<AudioSource>();

    }

    void Update()
    {
        // 사용자가 마우스 왼쪽 버튼을 누르면 총알이 총구에 생성되게 하고 싶다!
        // 1. 사용자가 마우스 왼쪽 버튼을 누르는지 확인한다.
        if (Input.GetMouseButtonDown(0))
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


            // 총알 발사음을 실행한다.
            audioSource.clip = sounds[0];
            audioSource.volume = 0.2f;
            audioSource.Play();
            //audioSource.Stop();
            //audioSource.Pause();
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

    // 폭발 효과음을 실행하는 함수.
    public void PlayExplosionSound()
    {
        audioSource.clip = sounds[1];
        audioSource.volume = 0.5f;
        audioSource.Play();
    }
}
