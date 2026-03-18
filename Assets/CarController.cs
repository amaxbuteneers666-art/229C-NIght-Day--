using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CarController : MonoBehaviour
{
    public WheelColliders colliders;
    public WheelMeshes wheelMeshes;
    public float gasInput;
    public float steeringInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        ApplyWheelPositions();
    }

    void CheckInput()
    {
        gasInput = Input.GetAxis("Vertical");
        /*if (gasPedal.isPressed)
        {
            gasInput += gasPedal.dampenPress;
        }
        if (brakePedal.isPressed)
        {
            gasInput -= brakePedal.dampenPress;
        }*/
        steeringInput = Input.GetAxis("Horizontal");
        /*if (rightButton.isPressed)
        {
            steeringInput += rightButton.dampenPress;
        }
        if (leftButton.isPressed)
        {
            steeringInput -= leftButton.dampenPress;
        }
        slipAngle = Vector3.Angle(transform.forward, playerRB.velocity - transform.forward);

        //fixed code to brake even after going on reverse by Andrew Alex 
        float movingDirection = Vector3.Dot(transform.forward, playerRB.velocity);
        if (movingDirection < -0.5f && gasInput > 0)
        {
            brakeInput = Mathf.Abs(gasInput);
        }
        else if (movingDirection > 0.5f && gasInput < 0)
        {
            brakeInput = Mathf.Abs(gasInput);
        }
        else
        {
            brakeInput = 0;
        }*/
    }
    void ApplyWheelPositions()
    {
        UpdateWheels(colliders.FRWheel, wheelMeshes.FRWheel);
        UpdateWheels(colliders.FLWheel, wheelMeshes.FLWheel);
        UpdateWheels(colliders.RRWheel, wheelMeshes.RRWheel);
        UpdateWheels(colliders.RLWheel, wheelMeshes.RLWheel);
    }
    void UpdateWheels(WheelCollider coll, MeshRenderer wheelMesh)
    {
        Quaternion quat;
        Vector3 postition;
        coll.GetWorldPose(out postition, out quat);
        wheelMesh.transform.position = postition;
        wheelMesh.transform.rotation = quat;
    }
}
[System.Serializable]
public class WheelColliders
{
    public WheelCollider FRWheel;
    public WheelCollider FLWheel;
    public WheelCollider RRWheel;
    public WheelCollider RLWheel;
}
[System.Serializable]
public class WheelMeshes
{
    public MeshRenderer FRWheel;
    public MeshRenderer FLWheel;
    public MeshRenderer RRWheel;
    public MeshRenderer RLWheel;
}