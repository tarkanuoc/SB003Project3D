using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterController : MonoBehaviour
{
    [SerializeField] private Rigidbody rigi;
    public float speedTakeOff = 20f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("=== Take Off");
            rigi.AddForce(Vector3.up * speedTakeOff, ForceMode.Impulse);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            Debug.Log("=== Left");
            rigi.AddForce(Vector3.left * speedTakeOff, ForceMode.Impulse);
        }
    }
}
