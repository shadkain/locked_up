using System;
using UnityEngine;

namespace Video {
	public class EntryPoint : MonoBehaviour {
		[SerializeField] private Controller firstController;
		
		private void Start() {
			firstController.enabled = true;
		}
	}
}