using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Foundation.Patterns.Facade;

public class Farmer : MonoBehaviour
{
	#region Inspector Fields
	[SerializeField] private GameObject _bottomLights = null;
	[SerializeField] private GameObject _middleLights = null;
	[SerializeField] private GameObject _topLights = null;
	#endregion

	#region Properties

	#endregion

	#region Fields
	private int _escalationLevel = 0;
	#endregion

	#region Life Cycle
	private void Awake()
	{
		GlobalEvents.CowAlerted.AddListener(Escalate);
	}

	private void Start()
	{
	}

	private void Update()
	{
	}
	#endregion

	#region Methods
	private void Escalate(Cow cow, Player player)
	{
		_escalationLevel++;

		UpdateLights();
	}

	private void UpdateLights()
	{
		_bottomLights.SetActive(_escalationLevel > 0);
		_middleLights.SetActive(_escalationLevel > 1);
		_topLights.SetActive(_escalationLevel > 2);
	}
	#endregion
}
