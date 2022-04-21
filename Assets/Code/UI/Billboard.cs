using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
	#region Inspector Fields

	#endregion

	#region Properties

	#endregion

	#region Fields
	private Transform _cameraTransform = null;
	#endregion

	#region Life Cycle
	private void Update()
	{
		if (_cameraTransform == null)
			_cameraTransform = Camera.main.transform;

		transform.rotation = Quaternion.LookRotation(transform.position - _cameraTransform.position);
	}
	#endregion

	#region Methods
	public void Show()
	{
		gameObject.SetActive(true);
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}

	public void Blink(float duration = .2f)
	{
		gameObject.SetActive(true);
		StartCoroutine(BlinkCoroutine(duration));
	}

	private IEnumerator BlinkCoroutine(float duration)
	{
		yield return new WaitForSeconds(duration);

		gameObject.SetActive(false);
	}
	#endregion
}
