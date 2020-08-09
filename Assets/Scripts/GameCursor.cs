using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCursor : MonoBehaviour {
	private SpriteRenderer _renderer;
	private bool _isActive;
	public Sprite activeSprite;
	public float activeSpriteOpacity;
	public Sprite sleepSprite;
	public float sleepSpriteOpacity;

	private bool _isRotating;
	private float _rotationZ;
	public uint rotationFrames;
	public bool rotateClockwise;


	// Start is called before the first frame update
	void Start() {
		Cursor.visible = false;
		_renderer = GetComponent<SpriteRenderer>();
		SetActive();

		_isRotating = false;
		_rotationZ = 0f;
	}

	// Update is called once per frame
	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			OnMouseButtonDown();
		} else if (Input.GetMouseButtonUp(0)) {
			OnMouseButtonUp();
		}

		if (_isRotating) {
			UpdateRotation();
		}
	}

	private void OnMouseButtonDown() {
		if (!_isRotating) {
			StartRotation();
		}
		
		transform.localScale = new Vector3(0.12f, 0.12f);
	}
	
	private void OnMouseButtonUp() {
		transform.localScale = new Vector3(0.15f, 0.15f);
	}

	private void StartRotation() {
		_isRotating = true;
		_rotationZ = 0f;
	}

	private void UpdateRotation() {
		_rotationZ += 360f / rotationFrames * (rotateClockwise ? -1 : 1);

		if (Math.Abs(_rotationZ) >= 360f) {
			EndRotation();
		}

		transform.rotation = Quaternion.Euler(0f, 0f, _rotationZ);
	}

	private void EndRotation() {
		_rotationZ = 0f;
		_isRotating = false;

		if (_isActive) {
			SetSleep();
		} else {
			SetActive();
		}
	}

	private void SetActive() {
		_isActive = true;
		_renderer.sprite = activeSprite;
		_renderer.color = new Color(1, 1, 1, activeSpriteOpacity);
	}

	private void SetSleep() {
		_isActive = false;
		_renderer.sprite = sleepSprite;
		_renderer.color = new Color(1, 1, 1, sleepSpriteOpacity);
	}
}