using System;
using UnityEngine;

namespace AnglesTeach
{
	public class VectorCross : MonoBehaviour
	{
		[SerializeField] private SimpleVector _vectorOne;
		[SerializeField] private SimpleVector _vectorTwo;

		[SerializeField] private SimpleVector _result;

		private void Update()
		{
			_result.SetVector(Vector3.Cross(_vectorOne.Vector, _vectorTwo.Vector));	
		}
	}
}