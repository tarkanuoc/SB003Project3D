using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody rigi;
    float _targetRotationX;
    float _targetRotationY;
    float _targetRotationZ;
    public float speedWalk = 20f;
    private bool _isIdle;
    private float _preMousePosX;
    public float speedRotate = 5f;
    public float timeRotate = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        _animator.SetTrigger("Idle");
        _preMousePosX = Input.mousePosition.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.W))
        {
            StopWalk();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Walk();
        }

        if (Input.GetMouseButton(1))
        {
            var _currentMousePosX = Input.mousePosition.x;
            if (_currentMousePosX != _preMousePosX)
            {

                if (_currentMousePosX > _preMousePosX)
                {
                    Debug.Log("======= Sang Phai");
                    RotateRight();
                }
                else
                {
                    Debug.Log("======= Sang Trai");
                    RotateLeft();
                }
                _preMousePosX = _currentMousePosX;
            }
        }

        
    }

    public void Walk()
    {
        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotationY, 0.0f) * Vector3.forward;
        rigi.velocity = targetDirection.normalized * speedWalk;
        _animator.SetTrigger("Walk");
        _animator.SetBool("IsMoving", true);
    }

    private void StopWalk()
    {
        _animator.SetTrigger("Idle");
        rigi.velocity = Vector3.zero;
        _animator.SetBool("IsMoving", false);
    }

    public void RotateLeft()
    {
        _targetRotationY -= speedRotate;
        transform.DORotateQuaternion(Quaternion.Euler(0f, _targetRotationY, 0f), timeRotate);

        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotationY, 0.0f) * Vector3.forward;
        rigi.velocity = targetDirection.normalized * rigi.velocity.magnitude;
    }

    public void RotateRight()
    {
        _targetRotationY += speedRotate;
        transform.DORotateQuaternion(Quaternion.Euler(0f, _targetRotationY, 0f), timeRotate);
        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotationY, 0.0f) * Vector3.forward;
        rigi.velocity = targetDirection.normalized * rigi.velocity.magnitude;
    }
}
