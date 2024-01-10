using Player;
using UnityEngine;

namespace Gameplay
{
	public class CowCatcher : MonoBehaviour
	{
		[SerializeField] private float _catchDistance;
		[SerializeField] private float _catchRadius;
		[SerializeField] private GameObject _effect;
		[SerializeField] private LayerMask _layerMask;
		[SerializeField] private float _catchTime = -1f;

		private bool _isCatchActionActive = false;
		private Transform _transform;
		private float _catchTimer;
		private Transform _catchedCow;
		private Vector3 _startCowPosition;
		private Vector3 _startCowScale;

		private void Awake() =>
			_transform = transform;

		public void SetInput(PlayerInput playerInput)
		{
			playerInput.CatchPressed += OnCatchPressed;
			playerInput.CatchReleased += OnCatchReleased;
		}

		private void SetCatch(bool value)
		{
			_effect.SetActive(value);
			_isCatchActionActive = value;
		}

		private void OnCatchReleased()
		{
			if (_catchedCow != null)
				return;

			SetCatch(false);
		}

		private void OnCatchPressed() =>
			SetCatch(true);

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.yellow;

			Gizmos.DrawSphere(transform.position + transform.forward * _catchDistance, _catchRadius);
		}

		private void Update()
		{
			if (_catchTimer > 0)
			{
				_catchTimer -= Time.deltaTime / _catchTime;

				if (_catchTimer <= 0)
				{
					if (_catchedCow != null)
					{
						Destroy(_catchedCow.gameObject);
						_catchedCow = null;
						OnCatchReleased();
					}
				}
			}

			if (_catchedCow != null)
				UpdateCowTransform();
		}

		private void UpdateCowTransform()
		{
			float time = Mathf.SmoothStep(0, 1, _catchTimer);

			_catchedCow.transform.localPosition = Vector3.Lerp(Vector3.zero, _startCowPosition, time);
			_catchedCow.transform.localScale = Vector3.Lerp(Vector3.zero, _startCowScale, time);
		}

		private void FixedUpdate()
		{
			if (_isCatchActionActive == false)
				return;
			
			if (_catchedCow != null)
				return;
			
			Collider[] colliders = Physics.OverlapSphere(_transform.position + _transform.forward * _catchDistance, _catchRadius, _layerMask, QueryTriggerInteraction.Ignore);

			foreach (Collider collider in colliders)
			{
				CowModel cowModel = collider.GetComponentInParent<CowModel>();

				if (cowModel != null)
				{
					cowModel.Catched();
					_catchedCow = cowModel.transform;
					_catchedCow.SetParent(_transform);
					_startCowPosition = _catchedCow.localPosition;
					_startCowScale = _catchedCow.localScale;

					_catchTimer = 1f;
					return;
				}
			}
		}
	}
}