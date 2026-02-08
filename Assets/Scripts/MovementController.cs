using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _cameraTransform;
        
    [Header("Settings")]
    [SerializeField] private float _moveSpeed = 10;

    [SerializeField] private float _rotateSpeed = 180;
    [SerializeField] private float _xMinAngle = -60;
    [SerializeField] private float _xMaxAngle = 60;
    [SerializeField] private float _jumpForce = 50;
    [SerializeField] private float _gravityForce = -9.81f;
    
    private CharacterController _characterController;
    private float _cameraPitch;
    private Vector3 _move;
    private float _verticalVelocity;
    private bool _isGrounded;
    
    private float _xLook;
    private float _yLook;
    

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {

        // Horizontal movement
        Vector3 moveDirection = transform.TransformDirection(_move);
        moveDirection *= _moveSpeed;
        
        // Check grounded
        
        _isGrounded = _characterController.isGrounded;
        if (_isGrounded && _verticalVelocity < 0)
        {
            _verticalVelocity = -2f;
        }
        
        // Gravity
        _verticalVelocity += _gravityForce * Time.deltaTime;
        moveDirection.y -= _verticalVelocity;

        // Apply movement and gravity
        _characterController.Move (moveDirection * Time.deltaTime);
        
        // Calculate body rotation
        Vector3 bodyRotation = new Vector3(0,_xLook , 0) * (_rotateSpeed * Time.deltaTime);
        
        //Apply body rotation
        transform.Rotate(bodyRotation);
        
        // Rotation camera (pitch)
        _cameraPitch -= _yLook * _rotateSpeed * Time.deltaTime;
        _cameraPitch = Mathf.Clamp(_cameraPitch, _xMinAngle, _xMaxAngle);
        _cameraTransform.localRotation = Quaternion.Euler(_cameraPitch, 0f, 0f);
        // calculate camera rotation
        // Vector3 cameraRotation = new Vector3(-_yLook, 0, 0);
        // cameraRotation = _cameraTransform.rotation.eulerAngles + cameraRotation;
        // cameraRotation.x = ClampAngle(cameraRotation.x, _xMinAngle, _xMaxAngle);
        
        // Apply camera rotation
        // _cameraTransform.eulerAngles = cameraRotation;
    }

    private float ClampAngle(float angle, float from, float to)
    {
        if (angle < 0f) angle = 360 + angle;
        if (angle > 180f) return Mathf.Max(angle, 360+from);
        return Mathf.Min(angle, to);
    }

    private void OnMove(InputValue input)
    {
        Vector2 movement = input.Get<Vector2>();
        _move.x = movement.x;
        _move.z = movement.y;
    }

    private void OnLook(InputValue input)
    {
        Vector2 look = input.Get<Vector2>();
        _xLook = look.x;
        _yLook = look.y;
    }

    private void OnJump(InputValue input)
    {
        if (_isGrounded)
        {
            _verticalVelocity = Mathf.Sqrt(_jumpForce * -2f * _gravityForce);
            Debug.Log(_verticalVelocity);
        }
    }
    
}