using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollbarController : MonoBehaviour
{
    [SerializeField] Scrollbar scrollbar;
    int minScrollValue = -160;  // 스크롤바의 최소이동 거리
    int maxScrollValue = 2;     // 스크롤바의 최대이동 거리

    private void FixedUpdate()
    {
        if(scrollbar.value >= maxScrollValue)   // 스크롤바의 최대이동 거리 제약
        {
            scrollbar.value = maxScrollValue;
        }
        else if (scrollbar.value <= minScrollValue)     // 스크롤바의 최소이동 거리 제약
        {
            scrollbar.value = minScrollValue;
        }
    }
}
