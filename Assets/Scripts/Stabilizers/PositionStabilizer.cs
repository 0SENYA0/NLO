using System;
using UnityEngine;

namespace Stabilizers
{
	[RequireComponent(typeof(Rigidbody))]
	public class PositionStabilizer : MonoBehaviour
	{
		[SerializeField] private float _stabilizerForce;
		private Rigidbody _rigidBody;

		private void Awake() =>
			_rigidBody = GetComponent<Rigidbody>();

		private void FixedUpdate()
		{
			_rigidBody.AddForce(-_rigidBody.position.z * Vector3.forward * _stabilizerForce, ForceMode.Force);
		}
	}
}