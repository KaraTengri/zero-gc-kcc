using UnityEngine;

public static class NonAllocPhysicsBuffer
{
    private const int DEFAULT_BUFFER_SIZE = 16;

    
    public static readonly RaycastHit[] HitBuffer = new RaycastHit[DEFAULT_BUFFER_SIZE];
    public static readonly Collider[] ColliderBuffer = new Collider[DEFAULT_BUFFER_SIZE];
    
}