using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
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

    // GameManager �ʱ�ȭ (�ܺο��� ȣ��)
    public void Initialize() //�ʱ�ȭ �޼��� [GameManager.Inst.Initialize();]
    {
        Debug.Log("GameManager �ʱ�ȭ!"); // ���� �ʱ� ���� ���� ���⿡ �ۼ�

        SetupPlayer();
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

}