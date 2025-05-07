using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject _optionPanel;
    [SerializeField] private GameObject _rankPanel;

    public void OnClickButton(string sceneName)
    {
        SceneManager.Inst.LoadScene(sceneName);
    }

    public void OnClick_OptionButton()
    {
        _optionPanel.SetActive(true);
    }

    public void OnClick_RankButton()
    {
        _rankPanel.SetActive(true);
    }
}
