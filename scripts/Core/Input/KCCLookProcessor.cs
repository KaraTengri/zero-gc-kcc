     using UnityEngine;

namespace Core.Input
{
    public static class KCCLookProcessor
    {
        private const float DefaultDeadzone = 0.05f;

        public static Vector2 ProcessLookInput(
            in Vector2 rawLook, 
            float sensitivityX, 
            float sensitivityY,
            float deltaTime, 
            float deadzone = DefaultDeadzone)
        {
            float sqrMag = rawLook.sqrMagnitude;

            if (sqrMag < deadzone * deadzone)  
            {
                return Vector2.zero;
            }

            return new Vector2(
                rawLook.x * sensitivityX * deltaTime,
                rawLook.y * sensitivityY * deltaTime
            );
        }

        
        public static float ClampPitch(float currentPitch, float deltaPitch, float minPitch = -89f, float maxPitch = 89f)
        {
            float newPitch = currentPitch - deltaPitch;
            return Mathf.Clamp(newPitch, minPitch, maxPitch);
        }
    }
}