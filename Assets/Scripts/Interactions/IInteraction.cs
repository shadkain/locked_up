namespace Interactions {
	public interface IInteraction {
		Video.Controller videoController { get; }
		TimeCode on { get; }
		TimeCode off { get; }
		
		void BeginInteraction();
		void EndInteraction();

		void RelatedFrameUpdated(ulong relatedFrame, float normalized);
	}
}