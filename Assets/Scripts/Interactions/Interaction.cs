using UnityEngine;
using UnityEngine.Video;

namespace Interactions {
	public abstract class Interaction : MonoBehaviour {
		public TimeCode on;
		public TimeCode off;

		public VideoFramesObserver videoFramesObserver;

		public abstract void Activate();
		public abstract void Deactivate();
	}

	public class LifetimeController {
		private readonly ulong startFrame;
		private readonly ulong endFrame;
		private readonly Interaction controlled;
		private bool deactivateOnNextFrame;

		public ulong duration => endFrame - startFrame + 1;

		public LifetimeController(Interaction controlled) {
			startFrame = controlled.videoFramesObserver.CalculateFrame(controlled.on);
			endFrame = controlled.videoFramesObserver.CalculateFrame(controlled.off);
			this.controlled = controlled;
			this.controlled.videoFramesObserver.frameUpdatedEvent += FrameUpdated;

			deactivateOnNextFrame = false;
		}

		private void FrameUpdated(ulong frame) {
			if (frame < startFrame || frame > endFrame) {
				if (!deactivateOnNextFrame) return;
				controlled.Deactivate();
				controlled.videoFramesObserver.frameUpdatedEvent -= FrameUpdated;
				return;
			}

			ProcessLifetimeFrame(frame);
		}
		
		private void ProcessLifetimeFrame(ulong frame) {
			var lifetimeFrame = frame - startFrame;
			var normalized = (float) lifetimeFrame / (duration - 1);

			switch (normalized) {
				case 0f:
					controlled.Activate();
					break;
				case 1f:
					deactivateOnNextFrame = true;
					break;
			}
		}
	}
}