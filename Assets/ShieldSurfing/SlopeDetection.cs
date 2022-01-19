using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopeDetection : MonoBehaviour
{
    [SerializeField] private Vector3 _origin;
    [SerializeField] private Vector3 _down;
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

        if (Physics.Raycast(_origin + new Vector3(0,0,.2f), _down, out hit2, Mathf.Infinity))
        {
            Debug.DrawLine(_origin, _down, Color.blue, 2.5f);

            _faceHitPosition2 = hit2.point;
            _faceHitNormal2 = hit.normal;

        }
    }
}
