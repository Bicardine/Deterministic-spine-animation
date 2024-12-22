//================================================================
//== [Code | Logic]: [Bicardine] ==
//================================================================
using UnityEngine;

namespace Deterministic
{
    // Instead of MonoBehaviour can use ServiceLocator or DI-Zenject or camera service for example but it's overdose for not real big project I quess.
    // So this class just for camera reference instead of /Camera.main/ and repeating ScreenToWorldPoint code. 
    public class PointerPosition : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _longAimPosition;
        [SerializeField] private Transform _target;

        public bool IsMouseToTheLeft => MousePosition.x < _target.position.x;

        public Vector3 MousePosition => _camera.ScreenToWorldPoint(Input.mousePosition);
        public bool IsLongAnimPisition => MousePosition.y > _longAimPosition.position.y;
    }

}