using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingOfSlope : MonoBehaviour
{
    [SerializeField] private float _speed = 3;
    private float _slopeSpeed;

    private SlopeDetection _slopeDetection;
    private Rigidbody rb;


    private bool grounded = false;
    
    public float _opposingForce = 1;

    private void Start()
    {

        _slopeDetection = FindObjectOfType<SlopeDetection>();
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Sliding();
    }

    public void Sliding()
    {
        Vector3 dir = (_slopeDetection._frontRay.transform.position - transform.position).normalized * 5;

        if (grounded == true && transform.rotation.x > 0)
        {
            rb.AddForce(dir);
            Debug.DrawRay(transform.position, dir, Color.green, Mathf.Infinity);

        }

        
    }

    IEnumerator _Wait()
    {
        yield return new WaitForSeconds(.1f);
        _opposingForce = _opposingForce / 1.01f;
    }

    public float GetSlopeSpeed()
    {
        float _slopeAddedSpeed;
        _slopeAddedSpeed = (_slopeDetection._angleSlope * 1.2f) / 10;

        return _slopeAddedSpeed;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "ground")
        {
            rb.constraints = RigidbodyConstraints.None;
            grounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        grounded = false;
        rb.constraints = RigidbodyConstraints.FreezeRotationX;
        rb.constraints = RigidbodyConstraints.FreezeRotationY;
        rb.constraints = RigidbodyConstraints.FreezeRotationZ;
    }
}
