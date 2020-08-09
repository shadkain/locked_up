using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoFramesObserver : MonoBehaviour {
	public delegate void FrameUpdatedEventHandler(ulong frame);

	public event FrameUpdatedEventHandler frameUpdatedEvent;
	public event Action lastFrameEndedEvent;

	private long lastFrame;

	public VideoPlayer player { get; private set; }

	private void Awake() {
		player = GetComponent<VideoPlayer>();
		player.loopPointReached += LastFrameEnded;
		
		Restore();
	}

	public void Restore() {
		lastFrame = -1;
	}

	public ulong CalculateFrame(TimeCode timeCode) {
		return timeCode.AbsoluteFrame(player.frameRate);
	}

	private void Update() {
		var currentFrame = player.frame;
		if (currentFrame > lastFrame) {
			FrameUpdated(Convert.ToUInt64(currentFrame));
			lastFrame = currentFrame;
		}
	}

	private void FrameUpdated(ulong frame) {
		frameUpdatedEvent?.Invoke(frame);
	}

	private void LastFrameEnded(VideoPlayer videoPlayer) {
		lastFrameEndedEvent?.Invoke();
	}
}