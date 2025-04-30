using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderMovement : MonoBehaviour {
    #region serialized fields

    [Tooltip("이동범위")]
    [SerializeField] private float _distance = 1f;
    [Tooltip("이동시간 = 속도와 반비례")]
    [SerializeField] private float _duration = 1f;
    [Tooltip("이동 방식")]
    [SerializeField] private Ease _ease = Ease.Linear;

    #endregion // serialized fields





    #region mono funcs

    private void Start() {
        transform.DOLocalMoveX(_distance, _duration)
            .SetRelative()
            .SetEase(_ease)
            .SetLoops(-1, LoopType.Yoyo);
    }

    #endregion // mono funcs
}
