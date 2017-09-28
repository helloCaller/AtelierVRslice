using UnityEngine;

namespace MeshManipulation
{
	public class SkinnedMeshExplode : MonoBehaviour
	{
		#region Variables

		public SkinnedMeshRenderer MeshRenderer;
		public ExplodeSettings Settings = new ExplodeSettings();

		#endregion

		#region Unity
		void Start()
		{
			if (MeshRenderer == null)
			{
				MeshRenderer = GetComponent<SkinnedMeshRenderer>();
			}
			if (Settings.Materials == null || Settings.Materials.Length == 0)
			{
				var meshRenderer = GetComponent<SkinnedMeshRenderer>();
				if (meshRenderer != null)
				{
					Settings.Materials = meshRenderer.materials;
				}
			}
		}

		void Update()
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				Explode();
			}
		}
		#endregion

		#region Explode

		public void Explode()
		{
			if (MeshRenderer == null)
			{
				Debug.LogWarning("MeshExplode: SkinnedMeshRenderer is null");
				return;
			}
			ExplodeHelper.Explode(gameObject, Settings);
			Destroy(gameObject);
		}

		#endregion
	}
}