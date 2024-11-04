using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterControlRigidbody : MonoBehaviour
{
    [SerializeField]
    private float _walkSpeed = 5f;

    [SerializeField]
    private float _runSpeed = 5f;
    
    [SerializeField]
    private float _rotationSpeed = 5f;

    private Vector3 _moveDir;
    private Rigidbody rb;
    private float _speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));


        if (_moveDir != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(_moveDir, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _rotationSpeed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        _speed = _walkSpeed;


        //Run
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _speed = _runSpeed;
        }

        rb.velocity = _moveDir * _speed * Time.fixedDeltaTime;
    }
}
