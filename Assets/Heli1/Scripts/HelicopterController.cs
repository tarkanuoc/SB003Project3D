using UnityEngine;
using DG.Tweening;

public class HelicopterController : MonoBehaviour
{
    public Rigidbody rigi;
    float _targetRotation;

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
    }

    void TakeOff()
    {
        rigi.velocity = Vector3.up * 50f;
    }

    public void Forward()
    {
        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;
        rigi.velocity = targetDirection.normalized * 50f;
        //rigi.AddForce(transform.rotation.eulerAngles * 5f, ForceMode.Impulse);
    }

    public void Stop()
    {
        rigi.velocity = Vector3.zero;
    }

    public void RotateLeft()
    {
        _targetRotation -= 10f;
        transform.DORotateQuaternion(Quaternion.Euler(0f, _targetRotation, 0f), 1.5f);

        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;
        rigi.velocity = targetDirection.normalized * 5f;
    }

    public void RotateRight()
    {
        _targetRotation += 10f;
        // transform.rotation = Quaternion.Euler(0f, _targetRotation, 0f);
        transform.DORotateQuaternion(Quaternion.Euler(0f, _targetRotation, 0f), 1.5f);
        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;
        rigi.velocity = targetDirection.normalized * 5f;
    }
}
