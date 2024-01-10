using System;
using UnityEngine;

namespace AnglesTeach
{
	public class SimpleVector : MonoBehaviour
	{
		[SerializeField] private Color _color = Color.yellow;
		private Transform _transform;

		public Vector3 Vector => transform.forward * transform.localScale.magnitude;

		private void Awake() =>
			_transform = transform;

		public void SetVector(Vector3 value)
		{
			
			
			Quaternion quaternion = Quaternion.identity;;
			quaternion.SetLookRotation(value.normalized);

			transform.rotation = quaternion;
			transform.localScale = Vector3.one.normalized * value.magnitude;
		}
		
		private void OnDrawGizmos()
		{
			if (_transform == null)
				_transform = transform;
			
			Gizmos.color = _color;
			
			Gizmos.DrawRay(_transform.position, Vector );
			Gizmos.DrawSphere(_transform.position + Vector, 0.1f);
		}
	}
}