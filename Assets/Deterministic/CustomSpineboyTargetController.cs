//================================================================
//== [Code | Logic]: [Bicardine] ==
//================================================================
using Spine;
using Spine.Unity;
using UnityEngine;

namespace Deterministic
{
    public class CustomSpineboyTargetController : MonoBehaviour
    {
        [SerializeField] private PointerPosition _pointerPosition;
        [SerializeField] private RenderCircleAndSetShootPosition _render;
        [SerializeField] private SkeletonAnimation _skeletonAnimation;

        [SpineBone(dataField: nameof(_skeletonAnimation))]
        public string boneName;
        public Camera cam;

        private Bone _bone;

        void OnValidate()
        {
            if (_skeletonAnimation == null)
                _skeletonAnimation = GetComponent<SkeletonAnimation>();
        }

        private void Start()
        {
            _bone = _skeletonAnimation.Skeleton.FindBone(boneName);
        }

        private void Update()
        {
            var worldMousePosition = _pointerPosition.MousePosition;

            var skeletonSpacePoint = _skeletonAnimation.transform.InverseTransformPoint(_pointerPosition.IsLongAnimPisition ? _pointerPosition.MousePosition : _render.PositionOnCircle);
            skeletonSpacePoint.x *= _skeletonAnimation.Skeleton.ScaleX;
            skeletonSpacePoint.y *= _skeletonAnimation.Skeleton.ScaleY;

            _bone.SetLocalPosition(skeletonSpacePoint);
        }
    }
}