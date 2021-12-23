using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopeDetection : MonoBehaviour
{
    [SerializeField] private Vector3 _origin;
    [SerializeField] private Vector3 _faceHitPosition;
    [SerializeField] private Vector3 _faceHitNormal;

    private float _angleSlope;

    private void Update()
    {
        GetSlope();
    }


    public void GetSlope()
    {
        RaycastHit hit;

        _origin = transform.position;

        if (Physics.Raycast(_origin, Vector3.down, out hit, Mathf.Infinity))
        {
            _faceHitPosition = hit.point;
            _faceHitNormal = hit.normal;

            _angleSlope = Vector3.Angle(_faceHitNormal, Vector3.up);
            Debug.Log(_angleSlope);
        }
    }
}
