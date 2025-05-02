using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{

    [SerializeField] private Transform respawnPoint; // 리스폰 지점
    [SerializeField] private Transform goalPoint; // 목표 지점 
    [SerializeField] private Transform savePoint; // 세이브 지점
    private bool Goal = false;

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

    private void Update()
    {

    }

    // GameManager 초기화 (외부에서 호출)
    public void Initialize() //초기화 메서드 [GameManager.Inst.Initialize();]
    {
        Debug.Log("GameManager 초기화!"); // 게임 초기 세팅 등을 여기에 작성

        SetupPlayer();

        savePoint = respawnPoint; // 초기 세이브 지점은 기본 리스폰 위치
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
    public void OnPlayerDied()
    {
        Debug.Log("GameManager: 플레이어가 사망하였다!");

        

        StartCoroutine(RespawnCoroutine());

        // 여기서 게임 오버 처리, UI 호출, 씬 전환 등
        // UIManager.Inst.ShowGameOver();
        // SceneManager.LoadScene("GameOverScene");

    }

    public void OnPlayerReachedGoal()
    {
        if (Goal) return; // 중복 방지
        Goal = true;

        Debug.Log("GameManager: 목표 도달 처리 실행!");

        // 여기서  게임 클리어 UI 띄우기, 씬 전환 등
        // UIManager.Inst.ShowStageClear();
        // SceneManager.LoadScene("NextStage");
    }

    public void OnPlayerReachedSave()
    {
        if (Goal) return; // 중복 방지
        Goal = true;

        Debug.Log("GameManager: 목표 도달 처리 실행!");

        // 여기서  게임 클리어 UI 띄우기, 씬 전환 등
        // UIManager.Inst.ShowStageClear();
        // SceneManager.LoadScene("NextStage");
    }

    public void UpdateSavePoint(Transform newSavePoint)
    {
        savePoint = newSavePoint;
        Debug.Log("새로운 세이브 포인트로 갱신됨: " + savePoint.position);
    }

    private IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(1f); // 1초 후 리스폰 (원한다면 이 시간 조절 가능)

        player.Respawn(savePoint.position); // 리스폰 포인트로 리스폰
    }

    
}