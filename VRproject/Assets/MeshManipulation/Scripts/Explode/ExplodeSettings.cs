using System;
using UnityEngine;

namespace MeshManipulation
{
	[Serializable]
	public class ExplodeSettings
	{

		public Material[] Materials;
		public bool AddCollider = false;
		public bool AddRigidbody = false;
	}
}
