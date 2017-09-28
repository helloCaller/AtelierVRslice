using UnityEngine;

namespace MeshManipulation
{
	public abstract class FaceMoveHelper : MeshHelper
	{

		#region Variables

		#endregion

	    #region MeshHelper

	    protected override void InitialSetup()
	    {
	        base.InitialSetup();
	        if (Filter != null && Filter.sharedMesh != null && Filter.sharedMesh.vertexCount != Filter.sharedMesh.triangles.Length)
	        {
	            if (Application.isPlaying || RunInEditor)
	            {
	                Filter.mesh = ConvertMesh(Filter.sharedMesh);
	            }
	        }
	    }

	    #endregion

		#region FaceMoveHelper

	    protected Vector3 GetFaceCenterPosition(Vector3[] verts, int index, int length)
		{
			Vector3 position = verts[index];
			Vector3 dir;
			for (int a = 1; a < length; ++a)
			{
				dir = verts[index + a] - position;
				position += dir*(1.0f/(a + 1.0f));
			}

			return position;
		}

		#endregion
	}
}