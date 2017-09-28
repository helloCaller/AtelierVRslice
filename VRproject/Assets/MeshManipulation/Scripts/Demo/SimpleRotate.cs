using UnityEngine;

namespace MeshManipulation
{
	public class SimpleRotate : MonoBehaviour
	{

		#region Variables

		public bool UpdateBeforeDelay = true;
		public bool UpdateAfterDuration = true;
		public float Delay = 0;
		public float Duration = 1f;
		public int RepeatCount = -1;
		public bool LocalRotation = true;
		public Vector3 RotationAxis = Vector3.up;
		public AnimationCurve RotationCurve = AnimationCurve.Linear(0f, 0.1f, 1f, 0.1f);

		private float time = 0;
		private int repeats = 0;

		#endregion

		#region Unity

		void Start()
		{
			time = -Delay;
		}

		void Update()
		{
			time += Time.deltaTime;

			if (!UpdateBeforeDelay && time < 0)
				return;
			if (time > Duration && RepeatCount > -1)
			{
				if (RepeatCount == 0 || repeats < RepeatCount)
				{
					time = time%Duration;
					repeats++;
				}
			}
			if (!UpdateAfterDuration && time > Duration)
				return;

			float value = RotationCurve.Evaluate(time/Duration);
			var axis = RotationAxis;
			if (LocalRotation)
			{
				axis = transform.right*axis.x +
				       transform.up*axis.y +
				       transform.forward*axis.z;
			}
			transform.Rotate(axis, value);
		}

		#endregion
	}
}