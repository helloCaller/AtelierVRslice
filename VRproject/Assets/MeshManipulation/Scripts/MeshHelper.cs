using UnityEngine;

namespace MeshManipulation
{
    public abstract class MeshHelper : MonoBehaviour
    {

        #region Variables

        [SerializeField] [Tooltip("This option will instantiate meshes out of play mode. Possible leaks")]
        protected bool RunInEditor = false;

        [SerializeField] [Tooltip("This option will enable updates while in play mode. Otherwise on Start() only")]
        protected bool RuntimeUpdate = true;

        public MeshFilter Filter;
        public Material[] Materials;

        public bool localSpace = true;
        public Vector3 EffectStrength = Vector3.one;

        protected Mesh originalMesh;
        protected Mesh convertedMesh;

        #endregion

        #region MeshHelper
        public virtual bool ShouldRunUpdate()
        {
            return (!Application.isPlaying && RunInEditor) || (Application.isPlaying && RuntimeUpdate);
        }

        public virtual void SetDistance(float newDistance)
        {
            foreach (var material in Materials)
            {
                if (material.HasProperty("_Distance"))
                {
                    material.SetFloat("_Distance", newDistance);
                }
            }
        }

        protected virtual void InitialSetup()
        {
            if (Filter == null)
            {
                Filter = GetComponent<MeshFilter>();
            }

            if (Filter != null && Filter.gameObject.isStatic)
            {
                Debug.LogError("MESH ERROR! MeshHelper: The GameObject is static and the mesh cannot be modified.");
            }
        }

        protected virtual void CleanUp()
        {
            if (originalMesh != null)
            {
                Filter.sharedMesh = originalMesh;
                originalMesh = null;
            }
        }

        protected virtual FaceEffector GetOrCreateEffector()
        {
            var effector = GetComponentInChildren<FaceEffector>();
            if (effector == null)
            {
                effector = CreateEffector();
            }
            return effector;
        }

        protected virtual Mesh ConvertMesh(Mesh mesh)
        {
            if (originalMesh != null)
                return convertedMesh;

            originalMesh = mesh;
            var newMesh = new Mesh();
            var vertices = new Vector3[mesh.triangles.Length];
            var uvs1 = new Vector2[mesh.triangles.Length];
            var uvs2 = new Vector2[mesh.triangles.Length];
            var uvs3 = new Vector2[mesh.triangles.Length];
            var uvs4 = new Vector2[mesh.triangles.Length];
            var colors = new Color[mesh.triangles.Length];
            var normals = new Vector3[mesh.triangles.Length];
            var tangents = new Vector4[mesh.triangles.Length];
            var triangles = new int[mesh.triangles.Length];

            for (int a = 0; a < mesh.triangles.Length; ++a)
            {
                int triangleIndex = mesh.triangles[a];
                vertices[a] = mesh.vertices[triangleIndex];
                uvs1[a] = triangleIndex < mesh.uv.Length ? mesh.uv[triangleIndex] : Vector2.zero;
                uvs2[a] = triangleIndex < mesh.uv2.Length ? mesh.uv2[triangleIndex] : Vector2.zero;
                uvs3[a] = triangleIndex < mesh.uv3.Length ? mesh.uv3[triangleIndex] : Vector2.zero;
                uvs4[a] = triangleIndex < mesh.uv4.Length ? mesh.uv4[triangleIndex] : Vector2.zero;
                colors[a] = a < mesh.colors.Length ? mesh.colors[triangleIndex] : Color.black;
                normals[a] = mesh.normals[triangleIndex];
                tangents[a] = mesh.tangents[triangleIndex];
                triangles[a] = a;
            }

            newMesh.vertices = vertices;
            newMesh.uv = uvs1;
            newMesh.uv2 = uvs2;
            newMesh.uv3 = uvs3;
            newMesh.uv4 = uvs4;
            newMesh.normals = normals;
            newMesh.tangents = tangents;
            newMesh.colors = colors;
            newMesh.triangles = triangles;

            convertedMesh = newMesh;
            return newMesh;
        }

        protected virtual FaceEffector CreateEffector()
        {
            var go = new GameObject("FaceEffector");
            go.transform.SetParent(transform);
            go.transform.localPosition = Vector3.zero;
            go.transform.rotation = Quaternion.identity;
            var effector = go.AddComponent<FaceEffector>();
            return effector;
        }

        protected virtual bool GetMeshData(out Vector3[] vertices, out int[] triangles)
        {
            Mesh mesh = null;
            if (Filter != null)
            {
                if (Application.isPlaying)
                {
                    mesh = Filter.mesh;
                }
                else if (RunInEditor)
                {
                    mesh = Instantiate(Filter.sharedMesh);
                    Filter.mesh = mesh;
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

        protected Vector3 GetVertexPosition(Vector3[] verts, int index)
        {
            return verts[index];
        }

        protected virtual Vector3 GetDirection(Vector3 vertexPosition, Vector3 effectorPosition, FaceEffector effector)
        {
            Vector3 dir = vertexPosition - effectorPosition;
            Vector3 scaledDir = new Vector3(
				dir.x * transform.lossyScale.x,
				dir.y * transform.lossyScale.y,
				dir.z * transform.lossyScale.z);
			dir = dir*effector.GetEffectStrength(scaledDir.magnitude);

            var strength = GetEffectStrength();

            dir.x *= strength.x;
            dir.y *= strength.y;
            dir.z *= strength.z;
			
            return dir;
        }

        protected Vector3 GetEffectStrength()
        {
            if (localSpace)
            {
                return transform.right * EffectStrength.x +
                       transform.up * EffectStrength.y +
                       transform.forward * EffectStrength.z;
            }
            return EffectStrength;
        }

        protected virtual Vector3 GetDirection(Vector3 vertexPosition, FaceEffector[] effectorList)
        {
            Vector3 direction = Vector3.zero;
            foreach (var effector in effectorList)
            {
                direction += GetDirection(vertexPosition, transform.InverseTransformPoint(effector.transform.position), effector);
            }
            return direction;
        }

        protected void SetDirections(Vector3[] directions)
        {
            var colors = new Color[directions.Length];
            for (int a = 0; a < directions.Length; ++a)
            {
                colors[a] = new Color(directions[a].x, directions[a].y, directions[a].z, 0);
            }

            if (Filter != null)
            {
                Filter.mesh.colors = colors;
            }
        }

        #endregion

        #region static

        public static int GetTopology(Mesh mesh)
        {
            if (mesh == null)
            {
                Debug.LogWarning("Explode: Mesh is null. Return 3 as default.");
                return 3;
            }

            MeshTopology topology = mesh.GetTopology(0);

            if (topology == MeshTopology.Triangles)
                return 3;
            else if (topology == MeshTopology.Quads)
                return 4;
            else if (topology == MeshTopology.Points)
                return 1;
            else if (topology == MeshTopology.Lines)
                return 2;
            else if (topology == MeshTopology.LineStrip)
                return 2;

            return 3;
        }
        #endregion
    }
}