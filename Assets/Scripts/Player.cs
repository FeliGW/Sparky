using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int hp;
    public int maxHp;
    [SerializeField] private float  _jumpForce;
    [SerializeField] private float _movSpeed;
    [SerializeField] private float _sprintSpeed;
    private float _currentSpeed;

    private Rigidbody _rb;
    private bool _isOnAir = false;

    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode _sprintKey = KeyCode.LeftShift;

    private float _xAxis = 0f, _zAxis = 0f;
    private Vector3 _dir = new();

    private void Start()
    {
        _currentSpeed = _movSpeed;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private void Update()
    {
        _xAxis = Input.GetAxis("Horizontal");
        _zAxis = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(_jumpKey) && !_isOnAir)
        {
            _isOnAir = true;
            Jump();
        }
    

    if (Input.GetKey(_sprintKey))
        {
            _currentSpeed = _sprintSpeed;
        }
        else
        {
            _currentSpeed = _movSpeed;
        }
    }

     private void FixedUpdate()
    {
        if(_xAxis != 0.0f || _zAxis != 0.0f)
        {
            Movement(_xAxis, _zAxis);
        }
    }

    private void Movement(float x, float z)
    {
        _dir = (transform.right * x + transform.forward * z).normalized;
        _rb.MovePosition(transform.position + _dir * _currentSpeed * Time.fixedDeltaTime);
    }

    private void Jump()
    {
        _rb.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 30) _isOnAir = false;
    }
}