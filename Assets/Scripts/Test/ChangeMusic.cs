using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusic : MonoBehaviour
{
    // 사운드 파일 배열 변수
    public AudioClip[] soundClips;

    AudioSource musicPlayer;

    void Start()
    {
        // 오디오 소스 컴포넌트를 변수에 캐싱하기
        musicPlayer = GetComponent<AudioSource>();

    }

    void Update()
    {
        // 만일, 키보드의 숫자 키 1번을 누르면
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        if(Input.GetButtonDown("Alpha1"))
        {
            // 사운드 클립 배열의 0번 음원 파일을 실행한다.
            ChangeSoundClip(0);
        }
        // 그렇지 않고 만일, 키보드의 숫자 키 2번을 누르면
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // 사운드 클립 배열의 1번 음원 파일을 실행한다.
            ChangeSoundClip(1);      
        }
        // 그렇지 않고 만일, 키보드의 ESC키를 누르면
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            // 오디오 소스를 중단한다.
            musicPlayer.Stop();
        }
    }

    void ChangeSoundClip(int clipNumber)
    {
        // 1. 실행 중인 오디오 소스를 정지한다.
        musicPlayer.Stop();

        // 2. 음원 배열에서 0번째를 오디오 소스에 넣는다.
        musicPlayer.clip = soundClips[clipNumber];

        // 3. 오디오 소스를 플레이한다.
        musicPlayer.Play();
    }
}
