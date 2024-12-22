//================================================================
//== [Code | Logic]: [Bicardine] ==
//================================================================
using Spine.Unity;
using Spine.Unity.Examples;
using System;
using UnityEngine;

namespace Deterministic
{
    public class ReverseComponent : MonoBehaviour
    {
        [SerializeField] private SkeletonAnimation _skeleton;
        [SerializeField] private PointerPosition _pointerPosition;
        [SerializeField] private MoveToXPointerPositionComponent _moveXPosition;
        [SerializeField] private UpdateAnimationDependingOnSpeed _updateAnimationDependingOnSpeed;
        [SerializeField] private ReadHeroInput _readHeroInput;

        private bool _isFacingLeft;
        private bool _isAiming;

        public event Action OnReverse;

        private const float VelocityLoverValueToReverseFace = 0.1f;
        private const float ScaleXMultiplierOnRevese = -1;

        private void OnEnable()
        {
            _readHeroInput.OnAimStarted += OnAimStarted;
            _readHeroInput.OnAimEnded += OnAimEnded;
        }

        private void OnDisable()
        {
            _readHeroInput.OnAimStarted -= OnAimStarted;
            _readHeroInput.OnAimEnded -= OnAimEnded;
        }

        private void Update()
        {
            TryReverseFacing();
        }

        private void OnAimStarted() => _isAiming = true;

        private void OnAimEnded() => _isAiming = false;

        private void TryReverseFacing()
        {
            if (NeedReverseFacing() && CanReverseFacing())
                ReverseFacing();
        }

        private bool NeedReverseFacing() => (_pointerPosition.IsMouseToTheLeft != _isFacingLeft) && _isAiming;

        private void ReverseFacing()
        {
            _isFacingLeft = !_isFacingLeft;

            _updateAnimationDependingOnSpeed.SetIdleAnimation();

            _skeleton.Skeleton.ScaleX *= ScaleXMultiplierOnRevese;

            OnReverse?.Invoke();
        }

        private bool CanReverseFacing() => _moveXPosition.AbsMoveXPosition < VelocityLoverValueToReverseFace;
    }
}