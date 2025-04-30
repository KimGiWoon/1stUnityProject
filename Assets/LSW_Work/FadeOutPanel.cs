using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutPanel : MonoBehaviour
{
    [SerializeField] private Image fadeOutPanel;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private float fadeDuration = 1f;

    private void Start()
    {
        gameOverUI.SetActive(false);
    }
    
    public void GameOver()
    {
        gameOverUI.SetActive(true);
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        Color color = fadeOutPanel.color;
        color.a = 0;
        fadeOutPanel.color = color;

        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Clamp01(timer / fadeDuration);
            fadeOutPanel.color = color;
            yield return null;
        }
    }
}
