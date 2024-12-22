using Deterministic.InputBased;
using Spine.Unity.Examples;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Deterministic
{
    public class ReadHeroInput : MonoBehaviour
    {
        private Controls _controls;

        public event Action OnAimStarted;
        public event Action OnAimEnded;

        private void Awake()
        {
            _controls = new Controls();
        }

        private void OnEnable()
        {
            _controls.Enable();
            _controls.Hero.Aim.started += HandleOnAimStart;
            _controls.Hero.Aim.canceled += HandleOnAimEnd;
        }

        private void OnDisable()
        {
            _controls.Disable();
            _controls.Hero.Aim.started -= HandleOnAimStart;
            _controls.Hero.Aim.canceled -= HandleOnAimEnd;
        }

        private void HandleOnAimStart(InputAction.CallbackContext context) => OnAimStart();

        private void HandleOnAimEnd(InputAction.CallbackContext context) => OnAimEnd();

        private void OnAimStart() => OnAimStarted?.Invoke();

        private void OnAimEnd() => OnAimEnded?.Invoke();
    }
}