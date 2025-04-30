using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{

    [SerializeField] private Transform respawnPoint; // ������ ����
    [SerializeField] private Transform goalPoint; // ��ǥ ���� 
    [SerializeField] private Transform savePoint; // ���̺� ����
    private bool Goal = false;

    private static GameManager instance; // �̱��� �ν��Ͻ�
    public static GameManager Inst => instance; // �̱��� �ν��Ͻ�


    public Player player { get; private set; } // �÷��̾� ����

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        if (instance == null) // ���� �ν��Ͻ��� ���ٸ�
        {
            instance = FindObjectOfType<GameManager>(true); // �� �ȿ��� GameManager�� ã��.

            if (instance == null) // Ž�� �� �������� ������
            {
                GameObject obj = new GameObject(nameof(GameManager));
                instance = obj.AddComponent<GameManager>(); // �ν��Ͻ��� ����
            }

            DontDestroyOnLoad(instance.gameObject); // ���� �ٲ� ��������.
        }
    }

    private void Awake()
    {
        if (instance == null) // �ν��Ͻ��� ���������
        {
            instance = this; // �ν��Ͻ��� ����
            DontDestroyOnLoad(gameObject); 
        }
        else if (instance != this) // �ν��Ͻ��� ������� ������
        {
            Destroy(gameObject); // �ߺ� ���� ����
        }
    }

    private void Update()
    {

    }

    // GameManager �ʱ�ȭ (�ܺο��� ȣ��)
    public void Initialize() //�ʱ�ȭ �޼��� [GameManager.Inst.Initialize();]
    {
        Debug.Log("GameManager �ʱ�ȭ!"); // ���� �ʱ� ���� ���� ���⿡ �ۼ�

        SetupPlayer();

        savePoint = respawnPoint; // �ʱ� ���̺� ������ �⺻ ������ ��ġ
    }

    // �÷��̾ �����ϴ� �޼���
    private void SetupPlayer()
    {
        player = FindObjectOfType<Player>(); // ������ Player�� ã�ƺ���

        if (player == null)
        {
            GameObject playerObj = new GameObject("Player"); // ���ٸ� ���ο� Player�� �����Ѵ�
            player = playerObj.AddComponent<Player>();
        }

    }
    public void OnPlayerDied()
    {
        Debug.Log("GameManager: �÷��̾ ����Ͽ���!");

        StartCoroutine(RespawnCoroutine());

        // ���⼭ ���� ���� ó��, UI ȣ��, �� ��ȯ ��
        // UIManager.Inst.ShowGameOver();
        // SceneManager.LoadScene("GameOverScene");

    }

    public void OnPlayerReachedGoal()
    {
        if (Goal) return; // �ߺ� ����
        Goal = true;

        Debug.Log("GameManager: ��ǥ ���� ó�� ����!");

        // ���⼭  ���� Ŭ���� UI ����, �� ��ȯ ��
        // UIManager.Inst.ShowStageClear();
        // SceneManager.LoadScene("NextStage");
    }

    public void OnPlayerReachedSave()
    {
        if (Goal) return; // �ߺ� ����
        Goal = true;

        Debug.Log("GameManager: ��ǥ ���� ó�� ����!");

        // ���⼭  ���� Ŭ���� UI ����, �� ��ȯ ��
        // UIManager.Inst.ShowStageClear();
        // SceneManager.LoadScene("NextStage");
    }

    public void UpdateSavePoint(Transform newSavePoint)
    {
        savePoint = newSavePoint;
        Debug.Log("���ο� ���̺� ����Ʈ�� ���ŵ�: " + savePoint.position);
    }

    private IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(1f); // 1�� �� ������ (���Ѵٸ� �� �ð� ���� ����)

        player.Respawn(savePoint.position); // ������ ����Ʈ�� ������
    }

}