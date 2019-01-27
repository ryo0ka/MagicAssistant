using System.Collections;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

namespace MagicAssistant
{
	public class LifecycleController : MonoBehaviour
	{
		[SerializeField]
		Transform _mascot;

		[SerializeField]
		Animator _mascotAnimator;

		[SerializeField]
		Collider _mascotCollider;

		[SerializeField]
		Transform _controller;

		Camera _camera;
		bool _play;

		IEnumerator Start()
		{
			_camera = Camera.main;

			MLInput.OnTriggerUp += (_, __) =>
			{
				_play = !_play;
			};

			yield return Do();
		}

		IEnumerator Do()
		{
			_mascotAnimator.CrossFade("Idle", 1f);

			while (!_play || Input.GetKey(KeyCode.Space))
			{
				_mascot.position = _controller.position + _controller.forward.normalized * .5f;

				_mascot.LookAt(_camera.transform);
				Vector3 angles = _mascot.eulerAngles;
				angles.x = 0;
				_mascot.eulerAngles = angles;

				yield return null;
			}
			
			Debug.Log("checking look");

			while (!CheckLookedAt())
			{
				yield return null;
			}

			_mascotAnimator.CrossFade("Work", .1f);

			yield return new WaitForSeconds(4f);

			while (CheckLookedAt())
			{
				yield return null;
			}

			yield return new WaitForSeconds(1f);

			_mascotAnimator.CrossFade("Back", .1f);

			while ( _play || Input.GetKey(KeyCode.Space))
			{
				yield return null;
			}

			yield return Do();
		}

		bool CheckLookedAt()
		{
			Ray ray = _camera.ViewportPointToRay(Vector3.one / 2);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit) &&
			    hit.collider == _mascotCollider)
			{
				return true;
			}

			return false;
		}
	}
}