using System;
using Gameplay;
using UnityEngine;

namespace Player
{
	[RequireComponent(typeof(Rigidbody))]
	public class Player : MonoBehaviour
	{
		[SerializeField] private Engine _engine;
		[SerializeField] private float _constantForcePower;
		[SerializeField] private CowCatcher _cowCatcher;
		
		private PlayerInput _playerInput;
		private Transform _transform;
		private Rigidbody _rigidbody;
		private ConstantForce _constantForce;

		private void Awake()
		{
			_transform = transform;
			_playerInput = gameObject.AddComponent<PlayerInput>();
			_rigidbody = GetComponent<Rigidbody>();
			_constantForce = GetComponent<ConstantForce>();
			_cowCatcher.SetInput(_playerInput);
			
			_engine.Initiliaze(_rigidbody);
		}

		private void Update()
		{
			bool isVerticalAxisActive = Mathf.Approximately(_playerInput.Controlls.y, 0) == false;

			if (isVerticalAxisActive)
			{
				_engine.SetAltitude(_engine.GetCurrentAltitude());
				_engine.SetOverrideControls(_playerInput.Controlls.y);
			}

			_engine.IsOverride = isVerticalAxisActive;
		}
 
		private void FixedUpdate()
		{
			_constantForce.force = -Vector3.right * _playerInput.Controlls.x * _constantForcePower + Physics.gravity * _rigidbody.mass;
		}
	}
}