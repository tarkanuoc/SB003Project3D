using UnityEngine;
using DG.Tweening;

public class HelicopterController : MonoBehaviour
{
    public Rigidbody rigi;
    float _targetRotationY;
    float _targetRotationX;

    private void Update()
    {
        if (Input.GetKey(KeyCode.W)) 
        {
            Forward();
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            Stop();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            RotateLeft();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            RotateRight();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeOff();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            Stop();
        }

        if (Input.GetKey(KeyCode.T))
        {
            Lean();
        }

        if (Input.GetKeyUp(KeyCode.T))
        {
            UnLean();
        }

        if (Input.GetKeyUp(KeyCode.L))
        {
            Landing();
        }
    }

    void TakeOff()
    {
        transform.DOMoveY(transform.position.y + 10f, 1f);
    }

    void Landing()
    {
        transform.DOMoveY(0f, 3f);
    }

    public void Forward()
    {
        Lean();
        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotationY, 0.0f) * Vector3.forward;
        rigi.velocity = targetDirection.normalized * 10f;
    }

    void Lean()
    {
        _targetRotationX = 15f;
        transform.DORotateQuaternion(Quaternion.Euler(_targetRotationX, _targetRotationY, 0f), 1.5f);
    }

    void UnLean()
    {
        _targetRotationX = 0f;
        transform.DORotateQuaternion(Quaternion.Euler(_targetRotationX, _targetRotationY, 0f), 1.5f);
    }

    public void Stop()
    {
        UnLean();
    }

    public void RotateLeft()
    {
        _targetRotationY -= 10f;
        transform.DORotateQuaternion(Quaternion.Euler(0f, _targetRotationY, 0f), 1.5f);

        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotationY, 0.0f) * Vector3.forward;
        rigi.velocity = targetDirection.normalized * 5f;
    }

    public void RotateRight()
    {
        _targetRotationY += 10f;
        transform.DORotateQuaternion(Quaternion.Euler(0f, _targetRotationY, 0f), 1.5f);
        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotationY, 0.0f) * Vector3.forward;
        rigi.velocity = targetDirection.normalized * 5f;
    }

    private void OnTriggerEnter(Collider other)
    {
        rigi.velocity = Vector3.zero;
        transform.DOKill();
    }
}
