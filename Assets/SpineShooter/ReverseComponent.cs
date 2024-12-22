//================================================================
//== [Code | Logic]: [Bicardine] ==
//================================================================
using Spine.Unity.Examples;
using System;
using UnityEngine;

namespace SpineShooter
{
    public class ReverseComponent : MonoBehaviour
    {
        [SerializeField] private PointerPosition _pointerPosition;
        [SerializeField] private MoveXPositionComponent _moveXPosition;
        [SerializeField] private UpdateAnimationDependingOnSpeed _updateAnimationDependingOnSpeed;
        [SerializeField] private SpineboyBeginnerModel _spineBoyModel;

        private bool _isAiming;

        public event Action OnReverse;

        private const float VelocityLoverValueToReverseFace = 0.1f;

        private void OnEnable()
        {
            _spineBoyModel.StartAimEvent += OnStartAim;
            _spineBoyModel.StopAimEvent += OnStopAim;
        }

        private void OnDisable()
        {
            _spineBoyModel.StartAimEvent -= OnStartAim;
            _spineBoyModel.StopAimEvent -= OnStopAim;
        }

        private void Update()
        {
            TryReverseFacing();
        }

        private void OnStartAim() => _isAiming = true;

        private void OnStopAim() => _isAiming = false;

        private void TryReverseFacing()
        {
            if (NeedReverseFacing() && CanReverseFacing())
                ReverseFacing();
        }

        private bool NeedReverseFacing() => (_pointerPosition.IsMouseToTheLeft != _spineBoyModel.facingLeft) && _isAiming;

        private void ReverseFacing()
        {
            _spineBoyModel.facingLeft = !_spineBoyModel.facingLeft;

            _updateAnimationDependingOnSpeed.SetIdleAnimation();

            OnReverse?.Invoke();
        }

        private bool CanReverseFacing() => _moveXPosition.AbsMoveXPosition < VelocityLoverValueToReverseFace;
    }
}