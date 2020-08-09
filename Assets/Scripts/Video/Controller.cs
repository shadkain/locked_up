using System;
using UnityEngine;
using UnityEngine.Video;

namespace Video {
	public class Controller : MonoBehaviour {
		// Serialized fields
		[SerializeField] private VideoClip clip;

		public VideoPlayer player { get; private set; }
		public Observer observer { get; private set; }

		public event Action becameEnabled;
		public event Action becameDisabled;

		private void Awake() {
			player = gameObject.AddComponent<VideoPlayer>();
			player.clip = clip;
			player.playOnAwake = false;
			player.waitForFirstFrame = false;
			player.isLooping = false;
			player.skipOnDrop = true;
			player.renderMode = VideoRenderMode.CameraFarPlane;
			player.targetCamera = null;
			player.targetCameraAlpha = 1f;
			player.aspectRatio = VideoAspectRatio.FitHorizontally;

			observer = new Observer(player);
			enabled = false;
		}

		private void Update() {
			observer.Update();
		}

		public void Prepare(TimeCode timeCode) {
			Prepare(timeCode.AbsoluteFrame(player.frameRate));
		}
		
		public void Prepare(ulong frame) {
			player.frame = Convert.ToInt64(frame);
			player.Prepare();
			observer.Reset();
		}

		private void OnEnable() {
			player.targetCamera = Camera.main;
			player.Play();
			observer.Reset();
			
			ControllerHub.shared.SetActive(this);
			becameEnabled?.Invoke();
		}

		private void OnDisable() {
			player.targetCamera = null;
			becameDisabled?.Invoke();
		}
	}
}