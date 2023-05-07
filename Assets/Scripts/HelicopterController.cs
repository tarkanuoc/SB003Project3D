using UnityEngine;
using DG.Tweening;

public class HelicopterController : MonoBehaviour
{
    [SerializeField] private Transform camera;
    public Rigidbody rigi;
    float _targetRotationX;
    float _targetRotationY;
    float _targetRotationZ;


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

        if (Input.GetKey(KeyCode.Q))
        {
            RotateLeft();
        }

        if (Input.GetKey(KeyCode.R))
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

        if (Input.GetKeyUp(KeyCode.W))
        {
            StopForward();
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

        if (Input.GetKeyDown(KeyCode.A))
        {
            TiltLeft();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            TiltRight();
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            StopTilt();
        }
       // camera.localEulerAngles = new Vector3(4.3f, 0, 0);
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

    public void StopForward()
    {
        UnLean();
        rigi.velocity = Vector3.zero;
    }

    void TiltLeft()
    {
        PitchLeft();
        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotationY, 0.0f) * Vector3.left;
        rigi.velocity = targetDirection.normalized * 10f;
    }

    void TiltRight()
    {
        PitchRight();
        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotationY, 0.0f) * Vector3.left;
        rigi.velocity = targetDirection.normalized * 10f;
    }

    void StopTilt()
    {
        StopPitch();
        rigi.velocity = Vector3.zero;
    }

    void PitchLeft()
    {
        _targetRotationZ = 15f;
        transform.DORotateQuaternion(Quaternion.Euler(_targetRotationX, _targetRotationY, _targetRotationZ), 1.5f);
        camera.DORotateQuaternion(Quaternion.Euler(0, _targetRotationY, 0f), 1.5f);
    }

    void PitchRight()
    {
        _targetRotationZ = -15f;
        transform.DORotateQuaternion(Quaternion.Euler(_targetRotationX, _targetRotationY, _targetRotationZ), 1.5f);
        camera.DORotateQuaternion(Quaternion.Euler(0, _targetRotationY, 0f), 1.5f);
    }

    void StopPitch()
    {
        _targetRotationZ = 0f;
        transform.DORotateQuaternion(Quaternion.Euler(_targetRotationX, _targetRotationY, _targetRotationZ), 1.5f);
        camera.DORotateQuaternion(Quaternion.Euler(0, _targetRotationY, 0f), 1.5f);
    }

    void Lean()
    {
        _targetRotationX = 15f;
        transform.DORotateQuaternion(Quaternion.Euler(_targetRotationX, _targetRotationY, 0f), 1.5f);
       // camera.DORotateQuaternion(Quaternion.Euler(0, _targetRotationY, 0f), 1.5f);
    }

    void UnLean()
    {
        _targetRotationX = 0f;
        transform.DORotateQuaternion(Quaternion.Euler(_targetRotationX, _targetRotationY, 0f), 1.5f);
     //   camera.DORotateQuaternion(Quaternion.Euler(0, _targetRotationY, 0f), 1.5f);
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
        rigi.velocity = targetDirection.normalized * rigi.velocity.magnitude;
    }

    public void RotateRight()
    {
        _targetRotationY += 10f;
        transform.DORotateQuaternion(Quaternion.Euler(0f, _targetRotationY, 0f), 1.5f);
        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotationY, 0.0f) * Vector3.forward;
        rigi.velocity = targetDirection.normalized * rigi.velocity.magnitude;
    }

    private void OnTriggerEnter(Collider other)
    {
        rigi.velocity = Vector3.zero;
        transform.DOKill();
    }
}
