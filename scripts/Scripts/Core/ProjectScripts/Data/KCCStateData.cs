using UnityEngine;

public struct KCCStateData
{
    public Vector3 Position;
    public Quaternion Rotation;
    public Vector3 Velocity;
    public Vector3 RawInputVector;
    public Vector3 SurfaceNormal;

    public float Speed;
    public float SlopeAngle;
    public float GroudDistance;

    public bool IsGrounded;
    public bool IsOnSlope;
    public bool IsJumping;

    
    public void Reset()
    {
        Position = Vector3.zero;
        Velocity = Vector3.zero;
        RawInputVector = Vector3.zero;
        SurfaceNormal = Vector3.up;

        Speed = 0f;
        SlopeAngle = 0f;
        GroudDistance = 0f;

        IsGrounded = false;
        IsOnSlope = false;
        IsJumping = false;
    }

}
