using UnityEngine;

namespace Core.Settings
{
    [CreateAssetMenu(fileName = "KCCSettings", menuName = "KCC/Settings Asset")]
    public class KCCSettings : ScriptableObject
    {
        [Header("Movement Settings")]
        [Tooltip("Karakterin ulaşabileceği maksimum yatay hız.")]
        public float maxSpeed = 8.0f;

        [Tooltip("Karakterin maksimum hıza ulaşma ivmesi.")]
        public float acceleration = 30.0f;

        [Tooltip("Girdi olmadığında yerdeki yavaşlatma sürtünmesi.")]
        public float groundFriction = 25.0f;

        [Header("Mass & Inertia")]
        [Tooltip("Kütle fiziğini aktif eder (a = F / m). Pasifse Arcade mod çalışır.")]
        public bool useMass = false;

        [Tooltip("Karakterin kütlesi. Yüksek kütle ivmelenmeyi geciktirir.")]
        public float mass = 1.0f;

        [Header("Wall & Slide Settings")]
        [Tooltip("Açıya bağlı duvar sürtünmesini aktif eder.")]
        public bool useWallFriction = true;

        [Tooltip("Duvar sürtünme katsayısı (Coulomb friction).")]
        public float wallFriction = 2.0f;

        [Header("Collision & Spatial Settings")]
        [Tooltip("Karakterin fizik kapsül yarıçapı.")]
        public float radius = 0.5f;

        [Tooltip("Karakterin toplam yüksekliği.")]
        public float height = 2.0f;

        [Tooltip("Engellere saplanmayı önleyen tampon payı (Skin Width).")]
        public float skinWidth = 0.015f;

        [Tooltip("Girdi için ölü bölge (Deadzone) eşiği.")]
        public float inputDeadzone = 0.1f;
    }
}