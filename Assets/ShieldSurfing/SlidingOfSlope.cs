using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingOfSlope : MonoBehaviour
{
    [SerializeField] private GameObject _walkState;
    [SerializeField] private GameObject _surfState;

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

        rb.AddForce(transform.forward * _speed);
    }
    

    private void Update()
    {
        Sliding();
        SlideToSides();
    }

    public void Sliding()
    {
        Vector3 dir = (_slopeDetection._frontRay.transform.position - transform.position).normalized * _speed;

        if (Input.GetKeyDown(KeyCode.E))
        {
            rb.AddForce(dir);
        }

        if (grounded == true && transform.rotation.x > 0.01f)
        {
            rb.AddForce(dir);
            Debug.DrawRay(transform.position, dir, Color.green, Mathf.Infinity);
        }

        if (transform.rotation.x <= 0.1 && grounded == true)
        {
            if (_opposingForce < rb.velocity.z)
            {
                rb.AddForce((_slopeDetection._frontRay.transform.position - transform.position) * 0);
                if (_opposingForce < .7f)
                {
                    _opposingForce = 0;
                }
                StartCoroutine(_Wait());
            }

        }
    }

    IEnumerator _Wait()
    {
        yield return new WaitForSeconds(.1f);
        _opposingForce = _opposingForce / 1.01f;
    }


    public void SlideToSides()
    {
        Vector3 Ldir = (_slopeDetection._LeftRay.transform.position - transform.position).normalized * (_speed/2);
        Debug.DrawRay(transform.position, Ldir, Color.yellow, Mathf.Infinity);

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Ldir);
        }


        Vector3 Rdir = (_slopeDetection._RightRay.transform.position - transform.position).normalized * (_speed/2);
        Debug.DrawRay(transform.position, Rdir, Color.yellow, Mathf.Infinity);

        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Rdir);
        }
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
