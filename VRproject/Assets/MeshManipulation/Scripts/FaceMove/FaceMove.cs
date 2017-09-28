using UnityEngine;

namespace MeshManipulation
{
	[ExecuteInEditMode]
	public class FaceMove : FaceMoveHelper
	{
		// Refs
		public FaceEffector[] EffectorList;

		#region Unity

		void Start()
		{
			InitialSetup();

		    if (!ShouldRunUpdate())
		        return;

			CalculateDirections();
		}

		void Update()
		{
			if (!ShouldRunUpdate())
				return;

			CalculateDirections();
		}

	    void OnDestroy()
	    {
	        CleanUp();
	    }

	    void OnDisbale()
	    {
	        CleanUp();
	    }

		#endregion

		#region FaceMoveHelper

		protected override void InitialSetup()
		{
			base.InitialSetup();
			if (EffectorList == null || EffectorList.Length == 0)
			{
				EffectorList = new[] {GetOrCreateEffector()};
			}
		}

		#endregion

		#region FaceMove

		private void CalculateDirections()
		{
			Vector3[] vertices;
			int[] triangles;
			if (!GetMeshData(out vertices, out triangles))
				return;

			Vector3[] directions = new Vector3[vertices.Length];

			int index = 0;
			int commonDivider = GetTopology(Filter.mesh);
			while (index < vertices.Length)
			{
				Vector3 direction = GetDirection(GetFaceCenterPosition(vertices, index, commonDivider), EffectorList);
				for (int a = 0; a < commonDivider; ++a)
				{
					directions[index + a] = direction;
				}

				index += commonDivider;
			}

			SetDirections(directions);
		}

		#endregion
	}
}