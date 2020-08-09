using System;

namespace Interactions {
	public class ActivityController {
		private readonly ulong startFrame;
		private readonly ulong endFrame;
		private readonly IInteraction interaction;
		private Action delayedAction;
		
		public ulong duration => endFrame - startFrame + 1;

		public ActivityController(IInteraction interaction) {
			this.interaction = interaction;
			startFrame = interaction.on.AbsoluteFrame(interaction.videoController.player.frameRate);
			endFrame = interaction.off.AbsoluteFrame(interaction.videoController.player.frameRate);

			interaction.videoController.observer.frameUpdated += FrameUpdated;
			interaction.videoController.observer.lastFrameEnded += LastFrameEnded;
		}

		~ActivityController() {
			interaction.videoController.observer.frameUpdated -= FrameUpdated;
			interaction.videoController.observer.lastFrameEnded -= LastFrameEnded;
		}
		
		private void FrameUpdated(ulong frame) {
			delayedAction?.Invoke();
			delayedAction = null;

			if (frame < startFrame || frame > endFrame) {
				return;
			}

			RelatedFrameUpdated(frame - startFrame);
		}

		private void RelatedFrameUpdated(ulong relatedFrame) {
			var normalized = (float) relatedFrame / (duration - 1);

			switch (normalized) {
				case 0f:
					interaction.BeginInteraction();
					break;
				case 1f:
					delayedAction += () => interaction.EndInteraction();
					break;
			}
			
			interaction.RelatedFrameUpdated(relatedFrame, normalized);
		}

		private void LastFrameEnded() {
			interaction.EndInteraction();
		}
	}
}