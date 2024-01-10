using UnityEngine;

namespace Gameplay
{
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(Animator))]
	public class CowModel : MonoBehaviour
	{
		[SerializeField] private float _jumpPower;
		[SerializeField] private GameObject _deadCowPrefab;
		[SerializeField] private float _minJumpTime = 1f;
		[SerializeField] private float _maxJumpTime = 2f;

		private float _jumpTimer = 1f;
		private Rigidbody _rigidbody;
		private Animator _animator;
		private Transform _transform;
		private bool _catched = false;

		private void Awake()
		{
			_transform = transform;
			_rigidbody = GetComponent<Rigidbody>();
			_animator = GetComponent<Animator>();
		}

		private void Update()
		{
			if (_catched == false)
				TryJump();
		}

		private void TryJump()
		{
			if (_jumpTimer > 0)
			{
				_jumpTimer -= Time.deltaTime;

				if (_jumpTimer < 0)
				{
					Jump();
					_jumpTimer = Random.Range(_minJumpTime, _maxJumpTime);
				}
			}
		}

		public void Catched()
		{
			_catched = true;
			_animator.SetBool("Fly", true);
			_rigidbody.isKinematic = true;
		}

		private void Jump()
		{
			_animator.SetTrigger("Jump");
			_rigidbody.velocity = (Vector3.up + _transform.forward) * _jumpPower;
		}

		private void OnCollisionEnter(Collision collision)
		{
			if (_catched)
				return;
			Debug.Log("Rigidbody");
			Rigidbody attachedRigidbody = collision.collider.attachedRigidbody;

			if (attachedRigidbody == null)
				return;

			if (attachedRigidbody.GetComponent<Player.Player>() != null)
			{
				Instantiate(_deadCowPrefab, _transform.position, _transform.rotation);
				Destroy(gameObject);
			}
		}
	}
}