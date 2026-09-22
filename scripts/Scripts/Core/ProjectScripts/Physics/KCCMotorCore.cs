using UnityEngine;

namespace Core.Physics
{
    /// <summary>
    /// KCC için yüksek performanslı, Zero-GC kinematik fizik hesaplama çekirdeği.
    /// </summary>
    public static class KCCMotorCore
    {
        public static Vector3 Accelerate(
            in Vector3 currentVelocity,
            in Vector3 targetDirection,
            float maxSpeed,
            float acceleration,
            float mass,
            bool useMass,
            float deltaTime)
        {
            // Kütle ayarı aktifse ivme kütleye bölünür (a = F / m)
            // Kütle pasifse doğrudan varsayılan ivme kullanılır (Arcade modu
            float effectiveAcceleration = useMass ? acceleration / mass : acceleration;

            Vector3 targetVelocity = targetDirection * maxSpeed;
            return Vector3.MoveTowards(currentVelocity, targetVelocity, effectiveAcceleration * deltaTime);
        }
        public static Vector3 ApplyFriction(
            in Vector3 currentVelocity,
            float groundFriction,
            float deltaTime)
        {
            return Vector3.MoveTowards(currentVelocity, Vector3.zero , groundFriction * deltaTime);
        }
        public static Vector3 CalculateSlideVector(
            in Vector3 Velocity,
            in Vector3 surfaceNormal,
            float wallFriction,
            bool useWallFriction,
            float deltaTime)
        {
            float dot = Vector3.Dot(Velocity, surfaceNormal);
            if(dot < 0f)
            {
                Vector3 slideVector = Velocity - (surfaceNormal *dot);
                if(!useWallFriction || wallFriction <= 0f)
                {
                    return slideVector;
                }

                float normalForceMagnitude = -dot;

                float frictionloss = wallFriction * normalForceMagnitude * deltaTime;

                return Vector3.MoveTowards(slideVector,Vector3.zero ,frictionloss);
            }
            return Velocity;
        }
        
}
}