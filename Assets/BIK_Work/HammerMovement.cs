using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerMovement : MonoBehaviour {
    #region serialized fields

    [Tooltip("회전 시간 == 속도와 반비례")]
    [SerializeField] private float _duration = 1f;
    [Tooltip("회전 방향")]
    [SerializeField] private bool _isClockwise = true;

    #endregion // serialized fields





    #region mono funcs

    private void Start() {
        transform.DOLocalRotate(new Vector3(0, _isClockwise == true ? 360 : -360, 0), _duration, RotateMode.FastBeyond360)
                 .SetEase(Ease.Linear)
                 .SetLoops(-1, LoopType.Incremental);
    }

    #endregion // mono funcs
}
