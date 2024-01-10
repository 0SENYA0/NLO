using UnityEngine;

namespace AnglesTeach
{
	public class VectorLerpTest : MonoBehaviour
	{
		[SerializeField] private Transform _firstVector;
		[SerializeField] private Transform _secondVector;
		[SerializeField] private float _tLerp;
		[SerializeField] private float _sTLerp;

		private Vector3 _lerpVector3 = Vector3.zero;

		private Vector3 _sLerpVector3 = Vector3.zero;

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.cyan;			
			
			Gizmos.DrawLine(Vector3.zero, _firstVector.transform.position);
			Gizmos.DrawLine(Vector3.zero, _secondVector.transform.position);
			
			_sLerpVector3 = Vector3.Slerp(_firstVector.position, _secondVector.position, _sTLerp);
			
			Gizmos.color = Color.magenta;
			Gizmos.DrawLine(Vector3.zero, _sLerpVector3);

			_lerpVector3 = Vector3.Lerp(_firstVector.position, _secondVector.position, _tLerp);
			
			Gizmos.color = Color.green;
			Gizmos.DrawLine(Vector3.zero, _lerpVector3);

		}
	}
}