using System.Collections.Generic;
using UnityEngine;

namespace MeshManipulation
{
	public class ExplodeHelper
	{
		public static void Explode(GameObject explodeObject, ExplodeSettings settings)
		{
			var meshFilter = explodeObject.GetComponentsInChildren<MeshFilter>();
			if (meshFilter != null && meshFilter.Length > 0)
			{
				foreach (var filter in meshFilter)
				{
					var go = ExplodeMesh(filter.mesh, settings);
					go.transform.position = filter.transform.position;
					go.transform.rotation = filter.transform.rotation;
					go.transform.localScale = filter.transform.localScale;
				}
				return;
			}

			var meshRenderer = explodeObject.GetComponentsInChildren<SkinnedMeshRenderer>();
			if (meshRenderer != null && meshRenderer.Length > 0)
			{
				foreach (var renderer in meshRenderer)
				{
					var go = ExplodeMesh(renderer.sharedMesh, settings);
					go.transform.position = renderer.transform.position;
					go.transform.rotation = renderer.transform.rotation;
					go.transform.localScale = renderer.transform.localScale;
				}
			}
		}

		public static GameObject ExplodeMesh(Mesh mesh, ExplodeSettings settings)
		{
			if (mesh == null)
			{
				Debug.LogWarning("Explode: Mesh is null");
				return null;
			}
			if (settings == null)
			{
				Debug.LogWarning("Explode: Settings are null");
				return null;
			}

			var parent = new GameObject();
			List<GameObject> gos = new List<GameObject>();
			int topology = MeshHelper.GetTopology(mesh);
			for (int a = 0; a < mesh.triangles.Length; a += topology)
			{
				var go = CreateMesh(a, topology, mesh, settings);
				go.transform.SetParent(parent.transform);
				if (settings.AddCollider)
				{
					go.AddComponent<BoxCollider>();
				}
				if (settings.AddRigidbody)
				{
					go.AddComponent<Rigidbody>();
				}
				gos.Add(go);
			}

			return parent;
		}

		private static GameObject CreateMesh(int startIndex, int topology, Mesh mesh, ExplodeSettings settings)
		{
			var endIndex = (startIndex + topology - 1);
			var go = new GameObject("Mesh " + startIndex + "-" + endIndex);
			var filter = go.AddComponent<MeshFilter>();
			var render = go.AddComponent<MeshRenderer>();
			var newMesh = new Mesh();

			var triangles = new int[topology];
			var vertices = new Vector3[topology];
			var uv = new Vector2[topology];
			var uv2 = new Vector2[topology];
			var uv3 = new Vector2[topology];
			var uv4 = new Vector2[topology];
			var colors = new Color[topology];
			var colors32 = new Color32[topology];
			var normals = new Vector3[topology];
			var tangents = new Vector4[topology];

			for (int a = 0; a < topology; ++a)
			{
				int triangleId = mesh.triangles[startIndex + a];
				triangles[a] = a;

				vertices[a] = mesh.vertices[triangleId];
				uv[a] = mesh.uv[triangleId];
				if (mesh.uv2.Length > triangleId)
					uv2[a] = mesh.uv2[triangleId];
				if (mesh.uv3.Length > triangleId)
					uv3[a] = mesh.uv3[triangleId];
				if (mesh.uv4.Length > triangleId)
					uv4[a] = mesh.uv4[triangleId];
				if (mesh.colors.Length > triangleId)
					colors[a] = mesh.colors[triangleId];
				if (mesh.colors32.Length > triangleId)
					colors32[a] = mesh.colors32[triangleId];
				if (mesh.normals.Length > triangleId)
					normals[a] = mesh.normals[triangleId];
				if (mesh.tangents.Length > triangleId)
					tangents[a] = mesh.tangents[triangleId];
			}

			newMesh.vertices = vertices;
			newMesh.triangles = triangles;
			newMesh.uv = uv;
			newMesh.uv2 = uv2;
			newMesh.uv3 = uv3;
			newMesh.uv4 = uv4;
			newMesh.colors = colors;
			newMesh.colors32 = colors32;
			newMesh.normals = normals;
			newMesh.tangents = tangents;

			filter.mesh = newMesh;
			render.materials = settings.Materials;

			return go;
		}
	}
}