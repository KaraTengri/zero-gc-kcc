using UnityEngine;

namespace Core.Input
{
    public static class KCCInputProcessor
    {
        private const float DefaultDeadzone = 0.05f;

        /// <summary>
        /// Ham 2D hareket girdisini temizler, deadzone uygular ve genliği 1.0 ile sınırlar. (Zero-GC)
        /// </summary>
        public static Vector2 ProcessMoveInput(in Vector2 rawInput, float deadzone = DefaultDeadzone)
        {
            float sqrMag = rawInput.sqrMagnitude;

            // Deadzone kontrolü (Analog çubuk kaymalarını engeller)
            if (sqrMag < deadzone * deadzone)
            {
                return Vector2.zero;
            }

            // Çapraz basışlarda hızı sınırlar, analog hassasiyetini korur
            if (sqrMag > 1.0f)
            {
                return rawInput / Mathf.Sqrt(sqrMag);
            }

            return rawInput;
        }
    }
}