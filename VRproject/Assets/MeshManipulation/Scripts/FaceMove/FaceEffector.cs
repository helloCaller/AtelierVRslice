using UnityEngine;

namespace MeshManipulation
{
	public class FaceEffector : MonoBehaviour
	{
		#region Variables

		public AnimationCurve EffectedCurve = AnimationCurve.Linear(0, 1, 1, 0);

		#endregion

		#region Unity

		void OnDrawGizmosSelected()
		{
			var color = Gizmos.color;
			Gizmos.color = new Color(0, 0, 1, 0.2f);
			Gizmos.DrawSphere(transform.position, EffectedCurve.keys[EffectedCurve.keys.Length - 1].time);
			Gizmos.color = color;
		}

		#endregion

		#region FaceMoveHelper

		public float GetEffectStrength(float distance)
		{
			return EffectedCurve.Evaluate(distance);
		}

		#endregion
	}
}