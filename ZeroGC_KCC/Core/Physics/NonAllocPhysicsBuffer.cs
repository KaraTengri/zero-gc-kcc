using UnityEngine;

public static class NonAllocPhysicsBuffer
{
    private const int DEFAULT_BUFFER_SIZE = 16;

    // Statik bellek tamponları: Oyun açıldığında bir kez belleğe yazılır, bir daha GC'ye girmez.
    public static readonly RaycastHit[] HitBuffer = new RaycastHit[DEFAULT_BUFFER_SIZE];
    public static readonly Collider[] ColliderBuffer = new Collider[DEFAULT_BUFFER_SIZE];
    
}