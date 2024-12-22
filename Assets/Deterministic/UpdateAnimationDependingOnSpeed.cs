//================================================================
//== [Code | Logic]: [Bicardine] ==
//================================================================
using Spine;
using Spine.Unity;
using Deterministic.Utils;
using UnityEngine;
using MathUtils = Deterministic.Utils.MathUtils;
namespace Deterministic
{
    public class UpdateAnimationDependingOnSpeed : MonoBehaviour
    {
        [SerializeField] private ReadHeroInput _readHeroInput;

        [SerializeField] private float _runDelta;
        [SerializeField] private PointerPosition _pointerPosition;
        [SerializeField] private MoveToXPointerPositionComponent _moveXPositionComponent;

        [SerializeField] private SkeletonAnimation _skeleton;
        [SerializeField] private ReverseComponent _reverseComponent;

        [Header("Animations references")]
        [SerializeField] private AnimationReferenceAsset _idleTurnAnimation;
        [SerializeField] private AnimationReferenceAsset _idleAnimation;
        [SerializeField] private AnimationReferenceAsset _walkAnimation;
        [SerializeField] private AnimationReferenceAsset _runAnimation;
        [SerializeField] private AnimationReferenceAsset _aimAnimation;

        [SerializeField][Range(0f, 10f)] private float _velocityToMovingAnimation = 0f;
        [SerializeField][Range(0.1f, 10f)] private float _runAnimationFrequency = 0.5f;
        [SerializeField][Range(0.1f, 10f)] private float _walkAnimationFrequency = 1f;

        private bool _reverse;

        private Spine.AnimationState AnimationState => _skeleton.AnimationState;
        private float AbsMoveXPosition => _moveXPositionComponent.AbsMoveXPosition;

        private float HalfOfMaxVelocity => _moveXPositionComponent.MaxSpeed / HalfMaxVelocityDevider;

        private float _dynamicMixDurationSeconds;

        private const float HalfMaxVelocityDevider = 2f;
        private const float SubtrahendOnReverse = 1;
        private const float VelocityLoverValueToReverseOnBack = 0;
        private const float RunMix = 0.05f;

        private const bool IdleAnimationLoop = true;
        private const bool MovingAnimationLoop = false;

        private float _secondsSinecLastAnimationChanged;

        private void OnEnable()
        {
            _readHeroInput.OnAimStarted += StartAim;
            _readHeroInput.OnAimEnded += StopAim;

            _reverseComponent.OnReverse += OnReverse;
        }

        private void OnDisable()
        {
            _readHeroInput.OnAimStarted -= StartAim;
            _readHeroInput.OnAimEnded -= StopAim;

            _reverseComponent.OnReverse -= OnReverse;
        }

        private void Update()
        {
            UpdateAnimation();
        }

        public void SetIdleAnimation()
        {
            SetAnimation(SpineUtils.ZeroTrackIndex, _idleTurnAnimation, false);
        }

        private void StartAim()
        {
            var aimTrack = SetAnimation(2, _aimAnimation, true);
            aimTrack.AttachmentThreshold = 1f;
            aimTrack.MixDuration = 0f;
        }

        private void StopAim()
        {
            AnimationState.AddEmptyAnimation(2, 0.5f, 0.1f);
        }

        private void OnReverse() => _reverse = !_reverse;

        // As alternative here is /state machine/, but /if else/ it's enough for now coz just 3-type states, so YAGNI-principle.
        private void UpdateAnimation()
        {
            _secondsSinecLastAnimationChanged += Time.deltaTime;

            if (AbsMoveXPosition > _velocityToMovingAnimation)
            {
                if (AbsMoveXPosition >= HalfOfMaxVelocity)
                {
                    var track = SetAnimation(SpineUtils.ZeroTrackIndex, _runAnimation, MovingAnimationLoop);
                    track.TrackTime = GetCurrentAnimationTrackTime(_runAnimationFrequency);
                    track.MixDuration = RunMix;
                }
                else
                {
                    var track = SetAnimation(SpineUtils.ZeroTrackIndex, _walkAnimation, MovingAnimationLoop)
                        .TrackTime = GetCurrentAnimationTrackTime(_walkAnimationFrequency);
                }
            }
            else
            {
                if (_skeleton.AnimationName == _idleAnimation.name) return;

                SetAnimation(SpineUtils.ZeroTrackIndex, _idleAnimation, IdleAnimationLoop);
            }
        }

        private TrackEntry SetAnimation(int trackIndex, Spine.Animation animation, bool loop)
        {
            var track = AnimationState.SetAnimation(trackIndex, animation, loop);

            return track;
        }

        private float GetCurrentAnimationTrackTime(float frequence)
        {
            var animationEnd = AnimationState.GetCurrent(SpineUtils.ZeroTrackIndex).AnimationEnd;
            var multiplier = MathUtils.SawWave(_moveXPositionComponent.XPosition, frequency: frequence);

            if (_reverse)
                multiplier = SubtrahendOnReverse - multiplier;

            var trackTime = animationEnd * multiplier;

            return trackTime;
        }
    }
}