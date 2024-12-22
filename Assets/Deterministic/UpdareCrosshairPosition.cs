//================================================================
//== [Code | Logic]: [Bicardine] ==
//================================================================
using Spine;
using Spine.Unity;
using UnityEngine;

namespace Deterministic
{
    public class UpdareCrosshairPosition : MonoBehaviour
    {
        [SerializeField] private PointerPosition _pointerPosition;
        [SerializeField] private RenderCircleAndSetShootPosition _render;
        [SerializeField] private SkeletonAnimation _skeletonAnimation;

        [SerializeField]
        [SpineBone(dataField: nameof(_skeletonAnimation))]
        private string boneName;

        private Bone _bone;

        private void OnValidate()
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