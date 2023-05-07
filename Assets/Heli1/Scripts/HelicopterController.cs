using UnityEngine;
using DG.Tweening;

public class HelicopterController : MonoBehaviour
{
    public Rigidbody rigi;
    float _targetRotationY;
    float _targetRotationX;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) 
        {
            Forward();
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
    }

    void TakeOff()
    {
        rigi.velocity = Vector3.up * 5f;
    }



    public void Forward()
    {
        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotationY, 0.0f) * Vector3.forward;
        rigi.velocity = targetDirection.normalized * 50f;
        //rigi.AddForce(transform.rotation.eulerAngles * 5f, ForceMode.Impulse);
    }

    void Lean()
    {
        _targetRotationX = 45f;
        transform.DORotateQuaternion(Quaternion.Euler(_targetRotationX, _targetRotationY, 0f), 1.5f);

        //Vector3 targetDirection = Quaternion.Euler(_targetRotationX, _targetRotationY, 0.0f) * Vector3.forward;
    }

    void UnLean()
    {
        _targetRotationX = 0f;
        transform.DORotateQuaternion(Quaternion.Euler(_targetRotationX, _targetRotationY, 0f), 1.5f);

        //Vector3 targetDirection = Quaternion.Euler(_targetRotationX, _targetRotationY, 0.0f) * Vector3.forward;
    }

    public void Stop()
    {
        rigi.velocity = Vector3.zero;
        Physics.gravity = Vector3.zero;
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
}
