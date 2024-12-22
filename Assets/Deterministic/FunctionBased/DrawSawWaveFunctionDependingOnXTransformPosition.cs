//================================================================
//== [Code | Logic]: [Bicardine] ==
//================================================================
using UnityEngine;

namespace Deterministic.FunctionBased
{
    public class DrawSawWaveFunctionDependingOnXTransformPosition : DrawSawWaveFunction
    {
        [SerializeField] private Transform _transform;

        protected override float GetXDelta() => _transform.position.x;
    }
}