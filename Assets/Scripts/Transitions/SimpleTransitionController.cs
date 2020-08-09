using System;
using System.Collections.Generic;
using Interactions;
using Interface;
using UnityEngine;
using Video;

// ReSharper disable InconsistentNaming

namespace Transitions {
	[Serializable]
	public class SimpleTransition {
		public string id;
		public Controller to;
		public TimeCode entryFrame;
		public ulong delay;

		public void Prepare() {
			to.Prepare(entryFrame);
		}

		public void Run() {
			to.enabled = true;
		}
	}

	public class SimpleTransitionController : MonoBehaviour {
		// Serialized fields
		[SerializeField, RequireInterface(typeof(IActionSender))]
		private UnityEngine.Object _actionSender;
		[SerializeField] private List<SimpleTransition> transitions;

		private IActionSender actionSender => _actionSender as IActionSender;

		private void Awake() {
			actionSender.prepareRequested += Prepare;
			actionSender.runRequested += Transit;
		}

		private void Prepare(string id) {
			var transition = transitions.Find(el => el.id == id);

			if (transition == null) {
				Debug.LogWarning($"Nonexistent \"{id}\" prepare transition requested");
			}

			transition?.Prepare();
		}

		private void Transit(string id) {
			var transition = transitions.Find(el => el.id == id);

			if (transition == null) {
				Debug.LogWarning($"Nonexistent \"{id}\" run transition requested");
			}
			
			transition?.Run();
		}
	}
}