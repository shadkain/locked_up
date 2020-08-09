using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseChaser : MonoBehaviour {
	// Update is called once per frame
	void Update() {
		var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		pos.z = 0f;

		transform.position = pos;
	}
}