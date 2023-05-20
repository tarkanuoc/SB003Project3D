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
    private bool _isMoving;
    private float _preMousePosX;
    public float speedRotate = 5f;
    public float timeRotate = 0.5f;
    private bool _isMoveBack = false;
    private float _walkDirection;
    private bool _isStopMoving;
    private float _stopTime = 1.33f;
    private float _VelocityStop;

    // Start is called before the first frame update
    void Start()
    {
        _animator.SetTrigger("Idle");
        _preMousePosX = Input.mousePosition.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isStopMoving)
        {
            _stopTime -= Time.deltaTime;
            _animator.SetFloat("StopSpeed", _stopTime / 1.33f);
            rigi.velocity *= (_stopTime / 1.33f);

            if (_stopTime <= 0f)
            {
                Debug.Log("======== stop");
                _animator.SetTrigger("Idle");
                rigi.velocity = Vector3.zero;
                _isStopMoving = false;
                _stopTime = 1.33f;
                speedWalk = 20f;
            }
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            StopWalk();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            Walk();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            WalkBack();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && _isMoving)
        {
            if (_isMoveBack)
            {
                _animator.SetFloat("MoveState", (float)MoveState.RunBack);
            }
            else
            {
                _animator.SetFloat("MoveState", (float)MoveState.Run);
            }
            speedWalk = 40f;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) && _isMoving)
        {
            if (_isMoveBack)
            {
                _animator.SetFloat("MoveState", (float)MoveState.WalkBack);
            }
            else
            {
                _animator.SetFloat("MoveState", (float)MoveState.Walk);
            }
            speedWalk = 20f;
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

        if (Input.GetMouseButtonUp(0))
        {
            StopWalk();
        }

    }

    public void Walk()
    {
        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotationY, 0.0f) * Vector3.forward;
        rigi.velocity = targetDirection.normalized * speedWalk;
        _animator.SetTrigger("Walk");
        _animator.SetFloat("MoveState", (float)MoveState.Walk);
        _animator.SetBool("IsMoving", true);
        _isMoveBack = false;
        _isMoving = true;
        _isStopMoving = false;
        _stopTime = 1.33f;
    }

    public void WalkBack()
    {
        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotationY, 0.0f) * Vector3.forward * -1f;
        rigi.velocity = targetDirection.normalized * speedWalk;
        _animator.SetTrigger("Walk");
        _animator.SetFloat("MoveState", (float)MoveState.WalkBack);
        _animator.SetBool("IsMoving", true);
        _isMoveBack = true;
        _isMoving = true;
        _isStopMoving = false;
        _stopTime = 1.33f;
    }

    private void StopWalk()
    {
        //rigi.velocity = Vector3.zero;

        _animator.SetFloat("MoveState", (float)MoveState.None);
       
        _animator.SetBool("IsMoving", false);

        _isMoveBack = false;
        _isMoving = false;
        _isStopMoving = true;
        _VelocityStop = rigi.velocity.magnitude;
        // speedWalk = 0f;
    }

    public void RotateLeft()
    {
        _targetRotationY -= speedRotate;
        transform.DORotateQuaternion(Quaternion.Euler(0f, _targetRotationY, 0f), timeRotate);

        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotationY, 0.0f) * Vector3.forward;
        if (_isMoveBack)
        {
            targetDirection *= -1f;
        }
        rigi.velocity = targetDirection.normalized * rigi.velocity.magnitude;
    }

    public void RotateRight()
    {
        _targetRotationY += speedRotate;
        transform.DORotateQuaternion(Quaternion.Euler(0f, _targetRotationY, 0f), timeRotate);
        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotationY, 0.0f) * Vector3.forward;

        if (_isMoveBack)
        {
            targetDirection *= -1f;
        }

        rigi.velocity = targetDirection.normalized * rigi.velocity.magnitude;
    }
}

public enum MoveState
{
    RunBack = -2,
    WalkBack = -1,
    None = 0,
    Walk = 1,
    Run = 2

}