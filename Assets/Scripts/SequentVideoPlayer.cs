using System;
using UnityEngine;
using UnityEngine.Video;

public class SequentVideoPlayer : MonoBehaviour {
	public VideoPlayer activePlayer;
	public VideoPlayer inactivePlayer;
	public String[] sequence;
	private int pos;

	public void Start() {
		pos = 0;

		activePlayer.clip = Resources.Load<VideoClip>(sequence[pos++]);
		inactivePlayer.clip = Resources.Load<VideoClip>(sequence[pos++]);
		
		inactivePlayer.Prepare();
		activePlayer.Play();
		
		inactivePlayer.targetCameraAlpha = 0f;

		activePlayer.loopPointReached += ActivePlayerEndReached;
		inactivePlayer.loopPointReached += ActivePlayerEndReached;
	}

	private void ActivePlayerEndReached(VideoPlayer player) {
		var temp = activePlayer;
		activePlayer = inactivePlayer;
		inactivePlayer = temp;
		
		activePlayer.Play();

		inactivePlayer.enabled = false;
		
		inactivePlayer.targetCameraAlpha = 0f;
		activePlayer.targetCameraAlpha = 1f;

		inactivePlayer.enabled = true;
		inactivePlayer.clip = Resources.Load<VideoClip>(sequence[pos]);
		inactivePlayer.Prepare();
	}

	private void SwapPlayers() {
	}
}