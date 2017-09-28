using UnityEngine;

namespace MeshManipulation
{
	[ExecuteInEditMode]
	public class MeshExplode : MonoBehaviour
	{
		#region Variables

		public MeshFilter Filter;
		public ExplodeSettings Settings = new ExplodeSettings();

		#endregion

		#region Unity
		void Start()
		{
			if (Filter == null)
			{
				Filter = GetComponent<MeshFilter>();
			}
			if (Settings.Materials == null || Settings.Materials.Length == 0)
			{
				var meshRenderer = GetComponent<MeshRenderer>();
				if (meshRenderer != null)
				{
					Settings.Materials = meshRenderer.materials;
				}
			}
		}

		#endregion

		#region Explode

		public void Explode()
		{
			if (Filter == null)
			{
				Debug.LogWarning("MeshExplode: Filter is null");
				return;
			}
			ExplodeHelper.Explode(gameObject, Settings);
			Destroy(gameObject);
		}

		#endregion
	}
}