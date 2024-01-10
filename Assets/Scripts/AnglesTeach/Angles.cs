using System;
using UnityEngine;

namespace AnglesTeach
{
	public class Angles : MonoBehaviour
	{
		[SerializeField] private SimpleVector _simpleVector1;
		[SerializeField] private SimpleVector _simpleVector2;

		[SerializeField] private SimpleVector _simpleAdditional;

		private void Update()
		{
			float angle = Vector3.Angle(_simpleVector1.Vector, _simpleVector2.Vector);
			
			Quaternion rotation = Quaternion.AngleAxis(90f, _simpleVector1.Vector);
			Vector3 additional = rotation * Vector3.up;
			float sign = Mathf.Sign(Vector3.Dot(_simpleVector2.Vector, additional));
			angle *= sign;
			gameObject.name = $"angle = {angle:F2}";
		}
	}
}