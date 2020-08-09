using System;

namespace Interactions {
	public interface IActionSender {
		event Action<string> prepareRequested;
		event Action<string> runRequested;
	}
}