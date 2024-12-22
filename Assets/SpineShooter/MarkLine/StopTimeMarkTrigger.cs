//================================================================
//== [Code | Logic]: [Bicardine] ==
//================================================================
using SpineShooter.Model.Data;
using UnityEngine;

namespace SpineShooter.MarkLine
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Collider2D))]
    public class StopTimeMarkTrigger : MonoBehaviour, IItemRenderer<Color>
    {
        [SerializeField] private Color _colorOnExit = Color.white;
        [SerializeField] private Color _colorOnEnter = Color.red;

        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Render(Color data)
        {
            _spriteRenderer.color = data;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out TriggerLine triggerLine) == false) return;

            Render(_colorOnEnter);

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPaused = true;
#endif
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out TriggerLine triggerLine) == false) return;

            Render(_colorOnExit);
        }
    }
}