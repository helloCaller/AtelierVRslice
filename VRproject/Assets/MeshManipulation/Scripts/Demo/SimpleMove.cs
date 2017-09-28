using UnityEngine;

namespace MeshManipulation
{
	public class SimpleMove : MonoBehaviour
	{
		#region Variables

		public bool UpdateBeforeDelay = true;
		public bool UpdateAfterDuration = true;
		public float Delay = 0;
		public Vector3 FromPosition;
		public Vector3 ToPosition;
		public float Duration = 1f;
		public int RepeatCount = -1;
		public AnimationCurve TransitionCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

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


			float value = TransitionCurve.Evaluate(time/Duration);
			transform.localPosition = (1 - value)*FromPosition + value*ToPosition;
		}

		#endregion
	}
}