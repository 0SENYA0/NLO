using System;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

namespace AnglesTeach
{
	public class QuaternionRotation : MonoBehaviour
	{
		[SerializeField] private float _angle;
		[SerializeField] private Transform _axis;

		private Transform _transform;

		private void Awake() =>
			_transform = transform;

		[ProPlayButton]
		public void Rotate()
		{
			Quaternion quaternion = Quaternion.AngleAxis(_angle, _axis.forward);
			_transform.rotation = quaternion * _transform.rotation;
		}

		private float t = 0.5f;

		private void Update()
		{
			if (Input.GetKeyUp(KeyCode.A))
			{
				t = 0.1f;
				Debug.Log("t = " + t);
			}

			if (Input.GetKeyUp(KeyCode.S))
			{
				t = 0.4f;
				Debug.Log("t = " + t);
			}

			if (Input.GetKeyUp(KeyCode.D))
			{
				t = 0.9f;
				Debug.Log("t = " + t);
			}

			if (Input.GetKeyUp(KeyCode.Space))
			{
				Debug.Log(Mathf.Lerp(2, 5, t));
			}
		}

		private void OnDrawGizmos()
		{
			if (_axis == null)
				return;
			Gizmos.color = Color.red;

			Gizmos.DrawRay(transform.position, _axis.forward * 10f);
			Gizmos.DrawRay(transform.position, -_axis.forward * 10f);
		}
	}
}