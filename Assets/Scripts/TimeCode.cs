using System;
using JetBrains.Annotations;
using UnityEngine;

[Serializable]
public struct TimeCode {
	[Range(0, 59)] public uint minutes;
	[Range(0, 59)] public uint seconds;
	public uint frames;

	[Pure]
	public ulong AbsoluteFrame(double fps) {
		var value = Math.Floor((minutes * 60 + seconds) * fps) + frames;
		return Convert.ToUInt64(value);
	}
}