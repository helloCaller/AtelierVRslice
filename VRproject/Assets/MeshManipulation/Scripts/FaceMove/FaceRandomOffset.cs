using UnityEngine;

namespace MeshManipulation
{
	[ExecuteInEditMode]
	public class FaceRandomOffset : FaceMoveHelper
	{
		#region Variables

		public bool UpdateRandomInRuntime = false;
		public FaceEffector CenterEffector;
		public float minDistance = 1;
		public float maxDistance = 2;

		private float[] randomLength = new float[] {0f};

		#endregion

		#region Unity

		void Start()
		{
			InitialSetup();

			if (Application.isPlaying || RunInEditor)
			{
				randomLength = GetRandomLength();
			    CalculateDirections();
			}
		}

		void Update()
		{
		    CalculateDirections();
		}

	    void OnDestroy()
	    {
	        CleanUp();
	    }

	    void OnDisable()
	    {
	        CleanUp();
	    }

		#endregion


		#region FaceMoveHelper

		protected override Mesh ConvertMesh(Mesh mesh)
		{
			var newMesh = base.ConvertMesh(mesh);
			randomLength = GetRandomLength();
		    return newMesh;
		}

		protected override void InitialSetup()
		{
			base.InitialSetup();
			if (CenterEffector == null)
			{
				CenterEffector = GetOrCreateEffector();
			}
		}

		#endregion

		#region FaceRandomOffset

		private float[] GetRandomLength()
		{
			if (Filter == null || Filter.mesh == null)
			{
				return new float[] {0f};
			}

			int vertexCount = Filter.mesh.vertexCount;
			randomLength = new float[vertexCount];
			for (int a = 0; a < vertexCount; ++a)
			{
				randomLength[a] = minDistance + (maxDistance - minDistance)*Random.value;
			}
			return randomLength;
		}

		private void CalculateDirections()
		{
		    if (!ShouldRunUpdate())
		        return;

			if (UpdateRandomInRuntime)
			{
				randomLength = GetRandomLength();
			}

			Vector3[] vertices;
			int[] triangles;
			if (!GetMeshData(out vertices, out triangles))
				return;

			Vector3[] directions = new Vector3[vertices.Length];

			int index = 0;
			int commonDivider = GetTopology(Filter.mesh);
			while (index < vertices.Length && index < randomLength.Length)
			{
				Vector3 direction = GetDirection(GetFaceCenterPosition(vertices, index, commonDivider), transform.InverseTransformPoint(CenterEffector.transform.position), CenterEffector);
				direction *= randomLength[index];
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