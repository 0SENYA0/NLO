using System;
using UnityEngine;

namespace AnglesTeach
{
	public class VectorDot : MonoBehaviour
	{
		[SerializeField] private SimpleVector _vectorOne;
		[SerializeField] private SimpleVector _vectorTwo;

		[SerializeField] private float _result;
		
		private void Update()
		{
			_result = Vector3.Dot(_vectorOne.Vector, _vectorTwo.Vector);
			gameObject.name = _result.ToString("F2");
		}
	}
}