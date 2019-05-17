using UnityEngine;
using System.Collections;

namespace InfiniteJumper.Verical
{

	public class FollowTarget : MonoBehaviour
	{
		public Transform target;
		public float ySmoothing;
		public float yMargin;

		private bool _disabled;
		
		void LateUpdate ()
		{	
			if (target && !_disabled) {
				TrackPlayer ();
			}
		}

		public void Disable ()
		{
			_disabled = true;
		}

		private bool CheckYMargin ()
		{
			return Mathf.Abs (transform.position.y - target.transform.position.y) > yMargin;
		}
	
		private void TrackPlayer ()
		{
			
			var targetY = transform.position.y;
			
			if (CheckYMargin ()) {
				targetY = Mathf.Lerp (transform.position.y, target.transform.position.y, ySmoothing * Time.deltaTime);
			}
			
			transform.position = new Vector3 (0, targetY, transform.position.z);
			
		}
	}
}
