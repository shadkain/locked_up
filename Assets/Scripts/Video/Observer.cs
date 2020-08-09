using System;
using UnityEngine;
using UnityEngine.Video;

namespace Video {
	public class Observer {
		public event Action<ulong> frameUpdated;
		public event Action lastFrameEnded;

		private readonly VideoPlayer player;
		private long lastFrame;

		public Observer(VideoPlayer player) {
			this.player = player;
			this.player.loopPointReached += LastFrameEnded;
			Reset();
		}

		public void Reset() {
			lastFrame = -1;
		}

		public void Update() {
			var currentFrame = player.frame;
			if (currentFrame > lastFrame) {
				FrameUpdated(Convert.ToUInt64(currentFrame));
				lastFrame = currentFrame;
			}
		}

		private void FrameUpdated(ulong frame) {
			frameUpdated?.Invoke(frame);
		}

		private void LastFrameEnded(VideoPlayer target) {
			lastFrameEnded?.Invoke();
		}
	}
}