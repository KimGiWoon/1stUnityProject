using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutPanel : MonoBehaviour
{
    [SerializeField] private Image fadeOutPanel;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private TextMeshProUGUI restartBtnText;
    [SerializeField] private TextMeshProUGUI exitBtnText;
    [SerializeField] private float fadeDuration = 1f;
    
    
    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        GameOver();
    }
    
    public void GameOver()
    {
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        Color fadeColor = fadeOutPanel.color;
        fadeColor.a = 0;
        fadeOutPanel.color = fadeColor;

        Color textColor = gameOverText.color;
        textColor.a = 0;
        gameOverText.color = textColor;
        
        Color btnColor = restartBtnText.color;
        btnColor.a = 0;
        restartBtnText.color = btnColor;
        
        float timer = 0f;
        while (timer < fadeDuration * 0.95f)
        {
            timer += Time.deltaTime;
            fadeColor.a = Mathf.Clamp01(timer / fadeDuration);
            fadeOutPanel.color = fadeColor;
            textColor.a = Mathf.Clamp01(timer * 0.95f / fadeDuration);
            gameOverText.color = textColor;
            btnColor.a = Mathf.Clamp01(timer * 0.95f / fadeDuration);
            restartBtnText.color = btnColor;
            exitBtnText.color = btnColor;
            yield return null;
        }
    }
}
