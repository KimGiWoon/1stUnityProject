using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public void OnClickButton(string sceneName)
    {
        SceneManager.Inst.LoadScene(sceneName);
    }
}
