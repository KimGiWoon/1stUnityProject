using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class FadeOutPanelClear : MonoBehaviour
{
    [Header("Drag&Drop")]
    [Tooltip("GameOverUICanvas의 하위 객체 드래그")] 
    [SerializeField] private Image fadeOutPanel;
    [SerializeField] private TextMeshProUGUI gameClearText;
    [SerializeField] private TextMeshProUGUI nextBtnText;
    [SerializeField] private Button nextBtn;
    
    [Header("Number")]
    [Tooltip("Fade-In 연출 시간")] 
    [SerializeField] private float fadeDuration = 1f;

    private void Awake()
    {
        Init();
        GameManager.Inst.TakeGameClearUI(gameObject);
    }
    
    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        GameClear();
    }
    
    /// <summary>
    /// public으로 설정하여 게임 매니저에서 직접 호출해도 되고, SetActive(true)를 통해 호출하는 것도 가능
    /// GameOver 조건이 충족되면 화면이 어두워지고 게임오버 UI 출력
    /// </summary>
    public void GameClear()
    {
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        Color fadeColor = fadeOutPanel.color;
        fadeColor.a = 0;
        fadeOutPanel.color = fadeColor;

        Color textColor = gameClearText.color;
        textColor.a = 0;
        gameClearText.color = textColor;
        
        Color btnColor = nextBtnText.color;
        btnColor.a = 0;
        nextBtnText.color = btnColor;
        
        float timer = 0f;
        while (timer < fadeDuration * 0.95f)
        {
            timer += Time.deltaTime;
            fadeColor.a = Mathf.Clamp01(timer / fadeDuration);
            fadeOutPanel.color = fadeColor;
            textColor.a = Mathf.Clamp01(timer * 0.95f / fadeDuration);
            gameClearText.color = textColor;
            btnColor.a = Mathf.Clamp01(timer * 0.95f / fadeDuration);
            nextBtnText.color = btnColor;
            yield return null;
        }
    }

    private void Init()
    {
        nextBtn.onClick.AddListener(()=>SceneManager.Inst.LoadClearScene());
    }
}
