
//================================================================
//== [Code | Logic]: [Bicardine] ==
//================================================================
using UnityEngine;

namespace SpineShooter.FunctionBased
{
    public class FollowToSawWaveLastPositionComponent : MonoBehaviour
    {
        [SerializeField] private DrawSawWaveFunction _drawSawWaveFunction;

        private void OnEnable()
        {
            _drawSawWaveFunction.OnNewLastPosition += OnNewLastPosition;
        }

        private void OnDisable()
        {
            _drawSawWaveFunction.OnNewLastPosition -= OnNewLastPosition;
        }

        private void OnNewLastPosition(Vector3 lastPosition)
        {
            transform.position = lastPosition;
        }
    }
}