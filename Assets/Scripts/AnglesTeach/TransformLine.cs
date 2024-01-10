using System;
using UnityEngine;

namespace AnglesTeach
{
	public class TransformLine : MonoBehaviour
	{
		[SerializeField] private Transform _lineEnd;

		private void OnDrawGizmos()
		{
			if (_lineEnd == null)
				return;
			
			Gizmos.color = Color.red;
			Gizmos.DrawLine(transform.position, _lineEnd.position);
			Gizmos.DrawSphere(_lineEnd.position, 0.2f);
		}
	}
}