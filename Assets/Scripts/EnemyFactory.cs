using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    // ������ �ð��� �� ������ ���ʹ̸� �����Ѵ�.
    // ���� �ð�, ���ʹ� ������, ����� �ð�

    public GameObject enemyPrefab; //v �ֳʹ� ������ ���� //
    public float delayTime = 2.0f; //v ���� �ֱ�ð� ���� //

    float currentTime = 0; //v ����ð� ���� //
    float printTime = 1.0f;
    int timeCount = 3;
    bool isTimerStart = true;

    void Start()
    {
        // Invoke �Լ��� �̿��� Ÿ�̸� ���
        // 1ȸ�� Ÿ�̸�
        //Invoke("InvokeTest", 2.5f);

        // �ݺ� Ÿ�̸�      (ù ���� �ð�f, �ݺ��� �ֱ� �ð�f)
        //InvokeRepeating("InvokeTest", 2.5f, 2.5f);
        // Invoke �Լ��� �Ű������� ���� �Լ��� ��� �����ϴ�.
    }

    void Update()
    {
        currentTime += Time.deltaTime; //v ���� //

        if (currentTime > delayTime) //v ����, �ֱ�ð����� ����ð��� Ŭ �� //
        {
            // ���ʹ̸� �����Ѵ�.
            GameObject enemy = Instantiate(enemyPrefab); //v ������ �ֳʹ� ������ ���� //
            enemy.transform.position = transform.position; //v �ֳʹ� ���丮�� ���� ��ġ ���� //
            enemy.transform.rotation = transform.rotation;


            // ��� �ð��� �ٽ� 0���� �ʱ�ȭ�Ѵ�.
            currentTime = 0; //v ��� �����ϱ� ���� ����ð� �ʱ�ȭ //
        }

        #region 3�� ī��Ʈ ��� ���� ����
        // 3�� ���� ī��Ʈ �ٿ��� �����Ѵ�.
        // ��, �� 1�ʸ��� ���� �ð��� ����Ѵ�.
        // ���������� "Start"�� ����Ѵ�.
        // ���� �ð��� 0�ʰ� �Ǹ� ī��Ʈ�� �ߴ��Ѵ�.

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
        print("�κ�ũ ��� �ǽ�!");
    }

}
