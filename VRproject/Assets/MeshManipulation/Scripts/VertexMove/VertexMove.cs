using UnityEngine;

namespace MeshManipulation
{
	[ExecuteInEditMode]
	public class VertexMove : VertexMoveHelper
	{
		// Refs
		public FaceEffector[] EffectorList;

		#region Unity

		void Start()
		{
			InitialSetup();

		    if (!ShouldRunUpdate())
		        return;

		    CalculateVertexDirections();
		}

		void Update()
		{
			if (!ShouldRunUpdate())
				return;

			CalculateVertexDirections();
		}

		#endregion

		#region VertexMoveHelper

		protected override void InitialSetup()
		{
			base.InitialSetup();
			if (EffectorList == null || EffectorList.Length == 0)
			{
				EffectorList = new[] {GetOrCreateEffector()};
			}
		}

		#endregion

		#region VertexMove

		private void CalculateVertexDirections()
		{
			Vector3[] vertices;
			int[] triangles;
			if (!GetMeshData(out vertices, out triangles))
				return;

			Vector3[] directions = new Vector3[vertices.Length];

			int index = 0;
			while (index < vertices.Length)
			{
				Vector3 direction = GetDirection(GetVertexPosition(vertices, index), EffectorList);

			    //direction = transform.InverseTransformVector(direction);

				directions[index] = direction;
				index++;
			}

			SetDirections(directions);
		}

		#endregion
	}
}