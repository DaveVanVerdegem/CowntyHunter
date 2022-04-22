using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Foundation.Patterns.Facade;

public class TippedCowState : CowState
{
	#region Properties
	public override CowStateType State => CowStateType.Tipped;
	public override bool CanBeTipped => false;
	public override bool CanBeAlerted => false;
	#endregion

	#region Fields
	private float _timer = 0;
	private bool _firstWiggle = false;
	#endregion

	#region Constructors
	public TippedCowState(Cow cow) : base(cow)
	{
		Debug.Log($"{_cow.name} is tipped.");
		_cow.TippingPoint.transform.Rotate(Vector3.right, 180);

		Settings.SpawnNoise(_cow.transform.position);
		AudioPlayer.Play(Settings.FallingCowClip, _cow.transform.position);

		_cow.StartCoroutine(FartCoroutine());
	}
	#endregion

	#region Life Cycle
	public override void Run()
	{
		if(_timer > .2f)
		{
			if (_firstWiggle)
				_cow.TippingPoint.transform.localEulerAngles = new Vector3(0, 0, 170f);
			else
				_cow.TippingPoint.transform.localEulerAngles = new Vector3(0, 0, 190f);

			_firstWiggle = !_firstWiggle;
			_timer = 0;
		}

		_timer += Time.deltaTime;
	}

	public override void Exit()
	{
	}
	#endregion

	#region Methods
	public override void ShowDebugInfo()
	{
	}

	private IEnumerator FartCoroutine()
	{
		yield return new WaitForSeconds(1);

		Object.Instantiate(Settings.PoopPrefab, _cow.transform.position - (_cow.transform.forward * 1.7f), Quaternion.identity);
		AudioPlayer.Play(Settings.FartClip, _cow.transform.position);
	}
	#endregion
}
