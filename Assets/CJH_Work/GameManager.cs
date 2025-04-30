using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    private static GameManager instance; // 싱글톤 인스턴스
    public static GameManager Inst => instance; // 싱글톤 인스턴스

    public Player player { get; private set; } // 플레이어 관리

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        if (instance == null) // 만약 인스턴스가 없다면
        {
            instance = FindObjectOfType<GameManager>(true); // 씬 안에서 GameManager를 찾음.

            if (instance == null) // 탐색 후 존재하지 않으면
            {
                GameObject obj = new GameObject(nameof(GameManager));
                instance = obj.AddComponent<GameManager>(); // 인스턴스로 지정
            }

            DontDestroyOnLoad(instance.gameObject); // 씬이 바뀌어도 남아있음.
        }
    }

    private void Awake()
    {
        if (instance == null) // 인스턴스가 비어있으면
        {
            instance = this; // 인스턴스로 지정
            DontDestroyOnLoad(gameObject); 
        }
        else if (instance != this) // 인스턴스가 비어있지 않으면
        {
            Destroy(gameObject); // 중복 생성 방지
        }
    }

    // GameManager 초기화 (외부에서 호출)
    public void Initialize() //초기화 메서드 [GameManager.Inst.Initialize();]
    {
        Debug.Log("GameManager 초기화!"); // 게임 초기 세팅 등을 여기에 작성

        SetupPlayer();
    }

    // 플레이어를 세팅하는 메서드
    private void SetupPlayer()
    {
        player = FindObjectOfType<Player>(); // 씬에서 Player를 찾아본다

        if (player == null)
        {
            GameObject playerObj = new GameObject("Player"); // 없다면 새로운 Player를 생성한다
            player = playerObj.AddComponent<Player>();
        }

    }

}