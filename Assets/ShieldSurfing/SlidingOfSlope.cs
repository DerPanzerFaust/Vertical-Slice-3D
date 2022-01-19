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
        if (grounded == true && transform.rotation.x > 0)
        {
            rb.AddForce(transform.forward * (_speed * GetSlopeSpeed()));

        }
        if (transform.rotation.x <= 0.1 && grounded == true)
        {
            Debug.Log(_opposingForce);
            if (_opposingForce < rb.velocity.z)
            {
                rb.velocity = Vector3.forward*5;
                rb.AddForce(Vector3.forward*-_opposingForce);
                StartCoroutine(_Wait());
            }
        }
    }

    IEnumerator _Wait()
    {
        yield return new WaitForSeconds(.1f);
        _opposingForce = _opposingForce * 1.01f * Time.deltaTime;
        yield break;
    }

    public float GetSlopeSpeed()
    {
        float _slopeAddedSpeed;
        _slopeAddedSpeed = (_slopeDetection._angleSlope * 1.2f) / 10;

        return _slopeAddedSpeed;
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("ENTERD/STAYED IN COLLISION");
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
