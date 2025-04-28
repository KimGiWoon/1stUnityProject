using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderMovement : MonoBehaviour {
    #region serialized fields

    [SerializeField] private float _distance = 1f;
    [SerializeField] private float _duration = 1f;
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
