using UnityEngine;
using UnityEngine.Video;

namespace Video {
	public class ControllerHub {
		private static ControllerHub _instance;
		public static ControllerHub shared => _instance ?? (_instance = new ControllerHub());

		private Controller activeController;
		private Controller previousController;

		private ControllerHub() { }

		public void SetActive(Controller controller) {
			previousController = activeController;
			activeController = controller;

			if (previousController != null && previousController != activeController) {
				previousController.player.Pause();
				activeController.player.started += DisablePrevious;
			}
		}

		private void DisablePrevious(VideoPlayer source) {
			previousController.enabled = false;
			source.started -= DisablePrevious;
		}
	}
}