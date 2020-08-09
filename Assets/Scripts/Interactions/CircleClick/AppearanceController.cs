using UnityEngine;

namespace Interactions.CircleClick {
	public class AppearanceController {
		private readonly Animator animator;
		private readonly SpriteRenderer renderer;
		private readonly CircleCollider2D collider;

		public bool visible {
			get => renderer.enabled;
			set => renderer.enabled = value;
		}

		public bool clickable {
			get => collider.enabled;
			set => collider.enabled = value;
		}

		public AppearanceController(GameObject container) {
			animator = container.GetComponent<Animator>();
			renderer = container.GetComponent<SpriteRenderer>();
			collider = container.GetComponent<CircleCollider2D>();

			animator.enabled = true;
		}

		public void ShowTimer(float normalized) {
			animator.Play("Circle_Timer", 0, normalized);
		}

		public void ShowExitAnimation(ExitAnimation animation) {
			animator.SetBool(animation.ToString("G"), true);
		}
	}
}