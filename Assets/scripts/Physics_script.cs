using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics_scripts : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _gravity = 1f;
    [SerializeField] private float _jumpHeight;
    private float _gravImpulse;

    private CharacterController _charController;

    void Start()
    {
        _charController = GetComponent<CharacterController>();
    }

    void Update()
    {
        float horInput = Input.GetAxis("Horizontal");
        Vector3 dir = new Vector3(0, 0, 0);
        Vector3 velocity = dir * _speed;

        velocity.y = _gravImpulse;

        _charController.Move(velocity * Time.deltaTime);

        if (_charController.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _gravImpulse = _jumpHeight;
            }
        }
        else
        {
            _gravImpulse -= _gravity;
        }
    }
}
