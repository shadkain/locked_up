using System;
using UnityEngine;
using Video;

// ReSharper disable InconsistentNaming

namespace Interactions.CircleClick {
	public class CircleClick : MonoBehaviour, IInteraction, IActionSender {
		private AppearanceController appearanceController;
		private ActivityController activityController;
		private bool wasHit;

		// Serializable fields
		[SerializeField] private Video.Controller _videoController;
		[SerializeField] private GameObject interactionObject;
		[SerializeField] private TimeCode _on;
		[SerializeField] private TimeCode _off;
		public ResultDescription hitResult;
		public ResultDescription missResult;

		private void Awake() {
			videoController.becameEnabled += () => enabled = true;
			videoController.becameDisabled += () => enabled = false;
		}

		private void OnEnable() {
			appearanceController = new AppearanceController(interactionObject) {
				clickable = false,
				visible = false,
			};
			activityController = new ActivityController(this);
		}

		private void OnDisable() {
			activityController = null;
		}

		// IInteraction implementation
		public Controller videoController => _videoController;
		public TimeCode on => _on;
		public TimeCode off => _off;

		public void BeginInteraction() {
			appearanceController.visible = true;
			appearanceController.clickable = true;

			wasHit = false;
		}

		public void EndInteraction() {
			runRequested?.Invoke(wasHit ? hitResult.actionId : missResult.actionId);
		}

		public void RelatedFrameUpdated(ulong relatedFrame, float normalized) {
			appearanceController.ShowTimer(normalized);
			Debug.Log($"normalized: {normalized}");
		}

		// IActionSender implementation
		public event Action<string> prepareRequested;
		public event Action<string> runRequested;
	}
}