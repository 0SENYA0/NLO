using System;
using DefaultNamespace;
using UnityEngine;

// Мировы в локальные transform.TransformDirection()
// Локальные в мировы transform.InverseTransformDirection()

namespace Player
{
	public class Engine : MonoBehaviour
	{
		[Header("SphereCast")] [SerializeField]
		private LayerMask _layerMask;

		[SerializeField] private float _sphereCastRadius;
		[SerializeField] private float _maxDistance;
		[SerializeField] private float _altitude;

		[Header("Lift")] [SerializeField] private float _maxForce;
		[SerializeField] private float _damping;

		[HideInInspector] public bool IsOverride = false;

		private Rigidbody _targetBody;
		private Transform _transform;
		private float _springSpeed;
		private float _oldDistance;
		private float _distance;
		private float _inputY;

		public float GetCurrentAltitude()
		{
			if (Physics.SphereCast(_transform.position, _sphereCastRadius, _transform.forward, out RaycastHit raycastHit, _maxDistance, _layerMask, QueryTriggerInteraction.Ignore))
				return raycastHit.distance;

			return _maxDistance;
		}

		private void FixedUpdate()
		{
			if (_targetBody == null)
				return;

			Vector3 forward = _transform.forward;

			if (IsOverride)
				ForceUpDown(forward);
			else
				Lift(forward);

			Damping();
		}

		private void ForceUpDown(Vector3 forward)
		{
			float forceFactor = _inputY > 0 ? 1 : 0; 
			_targetBody.AddForce(-forward * Mathf.Max(forceFactor * _maxForce - _springSpeed * _maxForce * _damping, 0), ForceMode.Force);
		}

		public void SetAltitude(float altitude) =>
			_altitude = Mathf.Clamp(altitude, _sphereCastRadius, _maxDistance);

		public void Initiliaze(Rigidbody targetBody)
		{
			_transform = transform;
			_targetBody = targetBody;
		}

		private void Lift(Vector3 forward)
		{
			if (Physics.SphereCast(_transform.position, _sphereCastRadius, forward, out RaycastHit raycastHit, _maxDistance, _layerMask, QueryTriggerInteraction.Ignore))
			{
				_distance = raycastHit.distance;

				float minForceHeight = _altitude + 1f;
				float maxForceHeight = _altitude - 1f;


				float forceFactor = Mathf.Clamp(_distance, maxForceHeight, minForceHeight).Remap(maxForceHeight, minForceHeight, 1, 0);
				_targetBody.AddForce(-forward * Mathf.Max(forceFactor * _maxForce - _springSpeed * _maxForce * _damping, 0), ForceMode.Force);
			}
		}

		private void Damping()
		{
			_springSpeed = (_distance - _oldDistance) * Time.fixedDeltaTime;
			_springSpeed = Mathf.Max(_springSpeed, 0);
			_oldDistance = _distance;
		}

		private void OnDrawGizmos()
		{
			Vector3 startPoint = transform.position;
			Vector3 endPoint = transform.position + transform.forward * _maxDistance;

			Gizmos.color = Color.red;
			Gizmos.DrawWireCube(transform.position, Vector3.one * 0.2f);
			Gizmos.color = Color.cyan;

			Gizmos.DrawSphere(endPoint, _sphereCastRadius);
			Gizmos.DrawLine(startPoint, endPoint);
		}

		public void SetOverrideControls(float controllsY) =>
			_inputY = controllsY;
	}
}