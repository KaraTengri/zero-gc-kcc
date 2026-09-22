using UnityEngine;

namespace Core.Physics
{
    public static class KCCSpatialUtility
    {
        private const float Epsilon = 0.0001f;

        /// <summary>
        /// 2D hareket girdisini verilen referans yönlere göre X-Z düzlemine izdüşürür. (Zero-GC)
        /// </summary>
        public static Vector3 ProjectToWorldSpace(
            in Vector2 processedInput, 
            in Vector3 forward, 
            in Vector3 right)
        {
            // Y bileşenini sıfırla (X-Z düzlemine yatır)
            Vector3 flatForward = new Vector3(forward.x, 0f, forward.z);
            Vector3 flatRight = new Vector3(right.x, 0f, right.z);

            float sqrMagFwd = flatForward.sqrMagnitude;
            float sqrMagRgt = flatRight.sqrMagnitude;

            // Dik bakış durumunda NaN önleme
            if (sqrMagFwd < Epsilon || sqrMagRgt < Epsilon)
            {
                return Vector3.zero;
            }

            flatForward /= Mathf.Sqrt(sqrMagFwd);
            flatRight /= Mathf.Sqrt(sqrMagRgt);

            return (flatRight * processedInput.x) + (flatForward * processedInput.y);
        }
    }
}