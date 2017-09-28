using UnityEngine;

namespace MeshManipulation
{
	[ExecuteInEditMode]
	public class SkinnedVertexMove : VertexMoveHelper
	{
		// Refs
		public SkinnedMeshRenderer MeshRenderer;
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
			if (MeshRenderer == null)
			{
				MeshRenderer = GetComponent<SkinnedMeshRenderer>();
			}
			if (EffectorList == null || EffectorList.Length == 0)
			{
				EffectorList = new[] { GetOrCreateEffector() };
			}
		}

		#endregion

		#region VertexMove

		protected override bool GetMeshData(out Vector3[] vertices, out int[] triangles)
		{
			Mesh mesh = null;
			if (MeshRenderer != null)
			{
				if (Application.isPlaying)
				{
					mesh = Instantiate(MeshRenderer.sharedMesh);
					originalMesh = MeshRenderer.sharedMesh;
					MeshRenderer.sharedMesh = mesh;

				}
				else if (RunInEditor)
				{
					mesh = Instantiate(MeshRenderer.sharedMesh);
					MeshRenderer.sharedMesh = mesh;
				}
			}

			if (mesh == null)
			{
				vertices = null;
				triangles = null;
				return false;
			}

			vertices = mesh.vertices;
			triangles = mesh.triangles;
			return true;
		}

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