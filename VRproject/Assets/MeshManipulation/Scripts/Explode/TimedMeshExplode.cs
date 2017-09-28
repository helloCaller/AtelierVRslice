using UnityEngine;

namespace MeshManipulation
{
	public class TimedMeshExplode : MeshExplode
	{

		#region Variables

		public float Delay = 1f;

		#endregion

		#region Unity
		void Update()
		{
			if (Delay >= 0 && Delay < Time.time)
			{
				Explode();
				Delay = -1f;
			}
		}

		#endregion

	}
}