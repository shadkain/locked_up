using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace Interactions {
	[Serializable]
	public class Transition {
		public string id;
		public VideoPlayer to;
		public ulong entryFrame;
		public ulong delay;

		public void Prepare() {
			to.frame = Convert.ToInt64(entryFrame);
			to.Prepare();
		}
	}

	public class TransitionController : MonoBehaviour {
		public VideoFramesObserver videoFramesObserver;
		public Transition[] transitions;
		private Dictionary<string, Transition> transitionsDict;

		private void Start() {
			transitionsDict = new Dictionary<string, Transition>();
			Array.ForEach(transitions, transition => transitionsDict.Add(transition.id, transition));
			transitions = null;
		}

		public void Prepare(string id) {
			if (transitionsDict.TryGetValue(id, out var transition)) {
				transition.Prepare();
				return;
			}
			
			Debug.LogWarning($"Cannot prepare not existing transition '{id}'");
		}

		public void Transit(string id) {
			if (transitionsDict.TryGetValue(id, out var transition)) {
				if (transition.delay == 0) {
					InstantTransit(transition);
				}
				return;
			}
			
			Debug.LogWarning($"Transition '{id}' doesn't exist");
		}

		private void InstantTransit(Transition transition) {
			transition.to.targetCamera = Camera.main;
			transition.to.started += source => {
				Debug.Log("started invoked");
				videoFramesObserver.player.targetCamera = null;
			};
			
			transition.to.Play();
			Debug.Log("play called");
			transition.to.enabled = true;

			videoFramesObserver.player.Pause();
		}
	}
}