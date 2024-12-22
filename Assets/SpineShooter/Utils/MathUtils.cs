//================================================================
//== [Code | Logic]: [Bicardine] ==
//================================================================
using UnityEngine;

namespace SpineShooter.Utils
{
    public static class MathUtils
    {
        private const float DefaultAmplitude = 1f;
        private const float DefaultFrequency = 1f;

        public const float AngleInCircle = 360;
        public const float ZeroAngle = 0;

        public static float SawWave(float x, float amplitude = DefaultAmplitude, float frequency = DefaultFrequency)
        {
            var frequenceX = x * frequency;

            return amplitude * (frequenceX - Mathf.Floor(frequenceX));
        }
    }
}