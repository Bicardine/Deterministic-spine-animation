//================================================================
//== [Code | Logic]: [Bicardine] ==
//================================================================
using Deterministic.Utils;
using UnityEngine;

namespace Deterministic
{
    [RequireComponent(typeof(LineRenderer))]
    public class RenderCircleAndSetShootPosition : MonoBehaviour
    {
        [SerializeField] private PointerPosition _pointerPosition;
        [SerializeField] LineRenderer _arrowLine;
        [SerializeField][Range(0, 5)] float _radius = 5f;
        [SerializeField][Range(5, 100)] private int _segments = 100;

        [SerializeField] private ReverseComponent _reverseComponent;

        private LineRenderer _lineRenderer;

        public Vector3 PositionOnCircle { get; private set; }

        private const int IncreaseCegmetsCountToLoopSegments = 1;
        private const int PointPositionToCoursorLineDirection = 2;

        private const int CenterPointPositionIndex = 0;
        private const int OnCirclePointPositionIndex = 1;

        private const float OnReversePositionMultiplier = -1f;

        private const float ZMousePosition = 0;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }

        private void Start()
        {
            _lineRenderer.positionCount = _segments + IncreaseCegmetsCountToLoopSegments;
            _lineRenderer.loop = true;

            DrawCircle();
        }

        private void OnEnable()
        {
            _reverseComponent.OnReverse += OnReverse;
        }

        private void OnDisable()
        {
            _reverseComponent.OnReverse -= OnReverse;
        }

        private void OnReverse()
        {
            ChangePosition();
        }

        private void ChangePosition()
        {
            var reversedX = transform.localPosition.x * OnReversePositionMultiplier;
            var targetPosition = transform.localPosition;
            targetPosition.x = reversedX;

            transform.localPosition = targetPosition;
        }

        void Update()
        {
            DrawCircle();
            UpdateCursorDirection();
        }

        void DrawCircle()
        {
            var angle = MathUtils.ZeroAngle;

            for (int i = 0; i <= _segments; i++)
            {
                var x = Mathf.Cos(angle * Mathf.Deg2Rad) * _radius;
                var y = Mathf.Sin(angle * Mathf.Deg2Rad) * _radius;

                var targetPosition = GetTargetPosition(x, y);

                _lineRenderer.SetPosition(i, targetPosition);

                angle += MathUtils.AngleInCircle / _segments;
            }
        }

        private Vector3 GetTargetPosition(float x, float y)
        {
            var targetPosition = Vector3.zero;
            targetPosition.x = x;
            targetPosition.y = y;
            targetPosition += transform.position;

            return targetPosition;
        }

        private void UpdateCursorDirection()
        {
            var mousePosition = _pointerPosition.MousePosition;
            mousePosition.z = ZMousePosition;

            var direction = (mousePosition - transform.position).normalized;

            var pointOnCircle = transform.position + direction * _radius;

            _arrowLine.positionCount = PointPositionToCoursorLineDirection;

            _arrowLine.SetPosition(CenterPointPositionIndex, transform.position);
            _arrowLine.SetPosition(OnCirclePointPositionIndex, pointOnCircle);

            PositionOnCircle = pointOnCircle;
        }
    }
}