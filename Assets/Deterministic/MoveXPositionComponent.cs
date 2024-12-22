//================================================================
//== [Code | Logic]: [Bicardine] ==
//================================================================
using System;
using UnityEngine;

namespace Deterministic
{
    [RequireComponent(typeof(CharacterController))]
    public class MoveXPositionComponent : MonoBehaviour
    {
        [SerializeField] private PointerPosition _pointerPosition;

        [Header("Speed based")]
        [SerializeField][Range(1f, 10)] private float _maxSpeed = 5f;
        [SerializeField][Range(1f, 10)] private float _acceleration = 10f;
        [SerializeField][Range(1f, 10)] private float _deceleration = 5f;

        private CharacterController _characterController;

        private float _currentSpeed;

        private float _previousXPosition;

        public float XPosition => transform.position.x;

        public float AbsMoveXPosition => Mathf.Abs(XVelocity);

        public float MaxSpeed => _maxSpeed;
        public float XVelocity => _currentSpeed;

        private const float MinDistanceToAcceleration = 1f;
        private const float TargetOnDeceleration = 0f;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            var mousePosition = _pointerPosition.MousePosition;
            var distance = mousePosition.x - transform.position.x;

            if (Mathf.Abs(distance) > MinDistanceToAcceleration)
                _currentSpeed = Mathf.MoveTowards(_currentSpeed, _maxSpeed * Mathf.Sign(distance), _acceleration * Time.deltaTime);
            else
                _currentSpeed = Mathf.MoveTowards(_currentSpeed, TargetOnDeceleration, _deceleration * Time.deltaTime);

            var motion = Vector3.zero;
            motion.x = _currentSpeed * Time.deltaTime;

            _characterController.Move(motion);

            _previousXPosition = XPosition;
        }
    }
}