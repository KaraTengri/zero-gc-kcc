using UnityEngine;
using Core.Input;
using UnityEngine.InputSystem;
using Core.Settings;


using UnityInput = UnityEngine.Input;
using UnityPhysics = UnityEngine.Physics;

namespace Core.Physics
{
    
    [RequireComponent(typeof(CapsuleCollider))]
    public class KCCMotor : MonoBehaviour
    {
        [Header("Input Asset")]
        [SerializeField] private InputActionAsset inputActions; 
        [SerializeField] private string moveActionName = "Move"; 

        [Header("Configuration")]
        [SerializeField] private KCCSettings settings;

        // Cached Input Action (0-GC Runtime)
        private InputAction _moveAction;

        [Header("Ground Detection")]
        [SerializeField] private LayerMask groundMask = ~0;
        [SerializeField] private float groundCheckDistance = 0.1f;

       
        private KCCStateData _state;
        private InputData _input;

        
        private CapsuleCollider _capsule;
        private Transform _transform;

        public KCCStateData State => _state;

        private void OnEnable()
        {
            // Aksiyonu aktif et
            _moveAction?.Enable();
            inputActions?.Enable();
        }

        private void OnDisable()
        {
           
            _moveAction?.Disable();
            inputActions?.Disable();
        }
        private void Awake()
        {
          _transform = transform;
            _capsule = GetComponent<CapsuleCollider>();
            
            _state.Reset();
            _input.Reset();

            _state.Position = _transform.position; 
            _state.Rotation = _transform.rotation;

          
            if (inputActions != null)
            {
                _moveAction = inputActions.FindAction(moveActionName, throwIfNotFound: false);
            }
        }
        
        private void Update()
        {
            // 1. Girdi Alma (0 Byte GC - Önbelleğe alınmış aksiyondan okuma)
            Vector2 rawMove = _moveAction != null 
                ? _moveAction.ReadValue<Vector2>() 
                : Vector2.zero;

            _input.MoveInput = KCCInputProcessor.ProcessMoveInput(rawMove, settings != null ? settings.inputDeadzone : 0.1f);
            
            // 2. Girdiyi Kamera/Dünya Yönüne İzdüşürme
            Vector3 targetDir = KCCSpatialUtility.ProjectToWorldSpace(_input.MoveInput, _transform.forward, _transform.right);
            _state.RawInputVector = targetDir;

            // 3. Zemin Tespit (Allocation-Free Physics)
            EvaluateGround();

            // 4. İvmele / Hareket Hesabı (Motor Core)
            float dt = Time.deltaTime;
            float maxSpeed = settings != null ? settings.maxSpeed : 8.0f;
            float accel = settings != null ? settings.acceleration : 30.0f;
            float mass = settings != null ? settings.mass : 1.0f;
            bool useMass = settings != null && settings.useMass;

            if (targetDir.sqrMagnitude > 0.001f)
            {
                _state.Velocity = KCCMotorCore.Accelerate(_state.Velocity, targetDir, maxSpeed, accel, mass, useMass, dt);
            }
            else if (_state.IsGrounded)
            {
                // Girdi yoksa zeminde sürtünme uygula
                float friction = settings != null ? settings.groundFriction : 25.0f;
                _state.Velocity = KCCMotorCore.ApplyFriction(_state.Velocity, friction, dt);
            }

            // 5. Pozisyon Güncelleme & Hız Hesaplama 
            _state.Position += _state.Velocity * dt;
            _transform.position = _state.Position;
            _state.Speed = _state.Velocity.magnitude;
        }

        /
        private void EvaluateGround()
        {
            Vector3 origin = _transform.position + Vector3.up * 0.1f;
            Vector3 direction = Vector3.down;
            float distance = groundCheckDistance + 0.1f;

            
            int hitCount = UnityPhysics.RaycastNonAlloc(origin, direction, NonAllocPhysicsBuffer.HitBuffer, distance, groundMask);

            bool hasHit = hitCount > 0;
            RaycastHit hit = hasHit ? NonAllocPhysicsBuffer.HitBuffer[0] : default;

          
            SurfaceResolver.ResolveSurfaceState(hit, hasHit, ref _state);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = _state.IsGrounded ? Color.green : Color.red;
            Gizmos.DrawLine(transform.position + Vector3.up * 0.1f, transform.position + Vector3.up * 0.1f + Vector3.down * (groundCheckDistance + 0.1f));
        }
    }
}