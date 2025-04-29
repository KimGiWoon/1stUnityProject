using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandulumMovement : MonoBehaviour {
    #region serialized fields

    [Tooltip("ȸ�� �ð� == �ӵ��� �ݺ��")]
    [SerializeField] private float _duration = 1f;
    [Tooltip("������ ���")]
    [SerializeField] private Ease _ease = Ease.Linear;

    #endregion // serialized fields





    #region mono funcs

    private void Start() {
        transform.DOLocalRotate(new Vector3(0, 180, 0), _duration, RotateMode.Fast)
                 .SetEase(_ease)
                 .SetLoops(-1, LoopType.Yoyo);
    }

    #endregion // mono funcs
}
