using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerMovement : MonoBehaviour {
    #region serialized fields

    [Tooltip("ȸ�� �ð� == �ӵ��� �ݺ��")]
    [SerializeField] private float _duration = 1f;
    [Tooltip("ȸ�� ����")]
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
