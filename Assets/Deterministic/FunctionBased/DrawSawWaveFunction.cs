//================================================================
//== [Code | Logic]: [Bicardine] ==
//================================================================
using Deterministic.Utils;
using System;
using UnityEngine;

namespace Deterministic.FunctionBased
{
    [RequireComponent(typeof(LineRenderer))]
    public class DrawSawWaveFunction : MonoBehaviour
    {
        [SerializeField] private Transform _circle;
        [SerializeField][Range(0, 10)] private float _frequency = 1.0f;
        [SerializeField][Range(0, 10)] private float _amplitude = 5.0f;
        [SerializeField][Range(0, 10)] private float _xScaleMultiplier = 5.0f;

        private LineRenderer _lineRenderer;

        private float XDelta => GetXDelta();

        private const int LastPointIndex = PointsCount - 1;
        private const int PointsCount = 100;

        public event Action<Vector3> OnNewLastPosition;

        private void Start()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.positionCount = PointsCount;
        }

        private void Update()
        {
            DrawSawWaveGraph();
        }

        protected virtual float GetXDelta() => Time.time;

        private void DrawSawWaveGraph()
        {
            for (int i = 0; i < PointsCount; i++)
            {
                var x = (float)i / (LastPointIndex);
                var y = MathUtils.SawWave(x + XDelta, _amplitude, _frequency);

                var newPosition = new Vector3(x * _xScaleMultiplier, y, 0) + transform.position;

                _lineRenderer.SetPosition(i, newPosition);
            }

            OnNewLastPosition?.Invoke(_lineRenderer.GetPosition(LastPointIndex));
        }
    }
}