using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    // 지정한 시간이 될 때마다 에너미를 생성한다.
    // 지정 시간, 에너미 프리팹, 경과된 시간

    public GameObject enemyPrefab;
    public float delayTime = 2.0f;

    float currentTime = 0;
    float printTime = 1.0f;
    int timeCount = 3;
    bool isTimerStart = true;

    void Start()
    {
        // Invoke 함수를 이용한 타이머 기능
        // 1회용 타이머
        //Invoke("InvokeTest", 2.5f);

        // 반복 타이머      (첫 실행 시간f, 반복할 주기 시간f)
        //InvokeRepeating("InvokeTest", 2.5f, 2.5f);
        // Invoke 함수는 매개변수가 없는 함수반 사용 가능하다.
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > delayTime)
        {
            // 에너미를 생성한다.
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.transform.position = transform.position;

            // 경과 시간을 다시 0으로 초기화한다.
            currentTime = 0;
        }

        #region 3초 카운트 기능 구현 연습
        // 3초 부터 카운트 다운을 시작한다.
        // 단, 매 1초마다 남은 시간을 출력한다.
        // 마지막에는 "Start"로 출력한다.
        // 남은 시간이 0초가 되면 카운트를 중단한다.

        //if (isTimerStart)
        //{
        //    printTime = 3;
        //    StartTimer();

        //}
        #endregion

    }

    void StartTimer()
    {
        if (isTimerStart)
        {
            currentTime += Time.deltaTime;
            if (currentTime > printTime)
            {
                if (timeCount == 0)
                {
                    print("Start");
                    isTimerStart = false;
                    //currentTime = 0;
                    //printTime = 3;
                }
                else
                {
                    print(timeCount);
                }
                timeCount--;

                currentTime = 0;
            }
        }
    }

    void InvokeTest()
    {
        print("인보크 기능 실시!");
    }

}
