using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateAnimator : MonoBehaviour
{
    private Vector3 _originalLocalPosition;

    private void Start()
    {
        _originalLocalPosition = this.transform.localPosition;
    }

    private void Update()
    {
        float sineValue = Mathf.Sin(Time.fixedTime * 0.05f * Mathf.PI * 8);
        this.transform.localPosition = _originalLocalPosition + new Vector3(0, sineValue * 0.4f, 0);
    }

}
