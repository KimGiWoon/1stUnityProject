using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerMovement : MonoBehaviour {
    #region serialized fields

    [SerializeField] private float _duration = 1f;

    #endregion // serialized fields





    #region mono funcs

    private void Start() {
        transform.DOLocalRotate(new Vector3(0, 360, 0), _duration, RotateMode.FastBeyond360)
                 .SetEase(Ease.Linear)
                 .SetLoops(-1, LoopType.Incremental);
    }

    #endregion // mono funcs
}
