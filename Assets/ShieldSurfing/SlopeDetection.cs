using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopeDetection : MonoBehaviour
{
    [SerializeField] private Vector3 _origin;
    [SerializeField] private Vector3 _down;

    public GameObject _frontRay;
    private Vector3 _down2;
    public Vector3 _faceHitPosition { get; private set; }
    public Vector3 _faceHitNormal { get; private set; }
    public Vector3 _faceHitPosition2 { get; private set; }
    public Vector3 _faceHitNormal2 { get; private set; }

    public float _angleSlope { get; private set; }

    private void Update()
    {
        GetSlope();
    }


    public void GetSlope()
    {
        RaycastHit hit;

        _origin = gameObject.transform.position;
        _down = new Vector3(transform.position.x,(transform.position.y - 10), transform.position.z);

        if (Physics.Raycast(_origin, _down, out hit, Mathf.Infinity))
        {
            Debug.DrawLine(_origin, _down, Color.red, 2.5f);

            _faceHitPosition = hit.point;
            _faceHitNormal = hit.normal;

            _angleSlope = Vector3.Angle(_faceHitNormal, Vector3.up);
        }

        RaycastHit hit2;

        _down2 = new Vector3(_frontRay.transform.position.x, (_frontRay.transform.position.y - 10), _frontRay.transform.position.z);
        if (Physics.Raycast(_frontRay.transform.position, _down2, out hit2, Mathf.Infinity))
        {
            Debug.DrawLine(_frontRay.transform.position, _down2, Color.blue, 2.5f);

            _faceHitPosition2 = hit2.point;
            Debug.Log(_faceHitPosition2);
            _faceHitNormal2 = hit.normal;

        }
    }
}
