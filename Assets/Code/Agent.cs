using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
	#region Inspector Fields
	[SerializeField] private float _speed = 1;
	#endregion

	#region Methods
	public void Move(Vector2 direction)
	{
		Move(new Vector3(direction.x, 0, direction.y));
	}

	public void Move(Vector3 direction)
	{
		transform.rotation = Quaternion.LookRotation(direction);
		transform.Translate(_speed * Time.deltaTime * Vector3.forward);
	}
	#endregion
}
