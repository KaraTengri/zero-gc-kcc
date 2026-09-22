using System.Runtime.CompilerServices;
using UnityEngine;
using Core.Settings;

namespace Core.Physics
{
    /// <summary>
    /// Zemin türlerini belirleyen enum.
    /// </summary>
    public enum SurfaceType : byte
    {
        Default = 0,
        DefaultWalkable = 1,
        Ice = 2,
        Mud = 3,
        Metal = 4,
        UnwalkableSlope = 5
    }

    /// <summary>
    /// Fiziksel yüzey özelliklerini tutan, stack üzerinde çalışan immutable veri yapısı. (Zero-GC)
    /// </summary>
    public readonly struct SurfaceProperties
    {
        public readonly Vector3 Normal;
        public readonly float Friction;
        public readonly float MaxWalkableAngle;
        public readonly bool IsWalkable;
        public readonly SurfaceType Type;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SurfaceProperties(Vector3 normal, float friction, float maxWalkableAngle, SurfaceType type = SurfaceType.DefaultWalkable)
        {
            Normal = normal;
            Friction = friction;
            MaxWalkableAngle = maxWalkableAngle;
            Type = type;

            float angle = Vector3.Angle(Vector3.up, normal);
            IsWalkable = angle <= maxWalkableAngle && type != SurfaceType.UnwalkableSlope;
        }

        public static readonly SurfaceProperties Air = new SurfaceProperties(Vector3.up, 0f, 45f, SurfaceType.Default);
    }

    
    public static class SurfaceResolver
    {
        // Static lookup table: Enum indeksinden doğrudan sürtünme değerine O(1) erişim.
        private static readonly float[] FrictionLookupTable = new float[]
        {
            1.0f, // Default
            0.8f, // DefaultWalkable
            0.05f, // Ice (Yüksek kayganlık)
            0.3f, // Mud (Yavaşlatma / Sürüklenme)
            0.9f, // Metal (Yüksek tutunma)
            0.0f  // UnwalkableSlope
        };

        private const float DefaultMaxAngle = 45f;

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float GetFriction(SurfaceType type)
        {
            int index = (int)type;
            if (index >= 0 && index < FrictionLookupTable.Length)
            {
                return FrictionLookupTable[index];
            }
            return 1.0f;
        }

        
        public static SurfaceProperties ResolveSurfaceState(
            in RaycastHit hitInfo, 
            bool hasHit,  
            ref KCCStateData state,
            SurfaceType surfaceType = SurfaceType.DefaultWalkable)
        {
            if (!hasHit)
            {
                state.IsGrounded = false;
                state.IsOnSlope = false;
                state.SurfaceNormal = Vector3.up;
                state.SlopeAngle = 0f;
                state.GroudDistance = float.MaxValue;
                return SurfaceProperties.Air;
            }

            state.SurfaceNormal = hitInfo.normal;
            state.SlopeAngle = Vector3.Angle(Vector3.up, hitInfo.normal);
            state.GroudDistance = hitInfo.distance;

            float friction = GetFriction(surfaceType);
            SurfaceProperties surface = new SurfaceProperties(hitInfo.normal, friction, DefaultMaxAngle, surfaceType);

            state.IsGrounded = surface.IsWalkable;
            state.IsOnSlope = state.IsGrounded && state.SlopeAngle > 0.01f;

            return surface;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 GetSlopeSlideDirection(in Vector3 surfaceNormal)
        {
            Vector3 cross = Vector3.Cross(Vector3.up, surfaceNormal);
            return Vector3.Cross(surfaceNormal, cross).normalized;
        }
    }
}