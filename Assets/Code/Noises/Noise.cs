using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise : MonoBehaviour
{
	#region Inspector Fields
	[SerializeField] private float _noiseMagnitude = 5f;
	[SerializeField] private float _duration = 1f;
	[SerializeField] private AnimationCurve _curve = null;
	#endregion

	#region Properties

	#endregion

	#region Fields
	private float _strength = 0f;
	private float _lifetime = 0f;
	private float _progress => _lifetime / _duration;

	private List<IHearable> _hearables = new List<IHearable>();
	#endregion

	#region Life Cycle
	public void Initialize(float magnitude = -1f)
	{
		if(magnitude > 0f)
			_noiseMagnitude = magnitude;

		StartCoroutine(MakeNoiseCoroutine());
	}

	private void OnTriggerEnter(Collider collider)
	{
		if (collider.TryGetComponent(out IHearable hearable))
		{
			if (_hearables.Contains(hearable)) return;
			_hearables.Add(hearable);

			hearable.HearNoise(this);

			Debug.Log($"{hearable} heard this noise.");
		}
	}

	private void OnTriggerExit(Collider collider)
	{
		if (collider.TryGetComponent(out IHearable hearable))
		{
			if (!_hearables.Contains(hearable)) return;

			_hearables.Remove(hearable);
		}
	}
	#endregion

	#region Methods
	private IEnumerator MakeNoiseCoroutine()
	{
		while(gameObject.activeSelf)
		{
			SetStrength();
			transform.localScale = new Vector3(_strength, 1, _strength);

			_lifetime += Time.deltaTime;

			if(_lifetime >= _duration)
				Destroy(gameObject);

			yield return null;
		}
	}

	private void SetStrength()
	{
		_strength = _curve.Evaluate(_progress) * _noiseMagnitude;
	}
	#endregion
}
