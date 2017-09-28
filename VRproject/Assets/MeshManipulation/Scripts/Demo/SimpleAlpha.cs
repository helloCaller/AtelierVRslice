using UnityEngine;

namespace MeshManipulation
{
	public class SimpleAlpha : MonoBehaviour
	{

		#region Variables

		public Material[] Materials;
		public bool UpdateBeforeDelay = true;
		public bool UpdateAfterDuration = true;
		public float Delay = 0;
		public float Duration = 1f;
		public int RepeatCount = -1;
		public AnimationCurve AlphaCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

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

			float value = AlphaCurve.Evaluate(time/Duration);
			foreach (var material in Materials)
			{
				if (material.HasProperty("_Color"))
				{
					Color color = material.GetColor("_Color");
					color.a = value;
					material.SetColor("_Color", color);
				}
			}
		}

		#endregion

		#region SimpleAlpha

		#endregion
	}
}