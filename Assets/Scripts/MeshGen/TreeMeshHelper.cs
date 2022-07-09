using Assets.Scripts.TreeGen;
using Assets.Scripts.TreeGen.Parts;
using UnityEngine;

namespace Assets.Scripts.MeshGen {
    public static class TreeMeshHelper {
        public static void AppendSegment(this RawListMesh mesh, Part segment, Vector3 basePos) {
            Vector3 targetPos = new Vector3();
            targetPos += segment.direction;
            targetPos += basePos;

            // Debug.Log(basePos);
            // Debug.Log(targetPos);
            //for (int i = 0; i < 4 * 4; i += 4)
            //{
            //    mesh.Uv.Add(new Vector2(0, 1));
            //    mesh.Uv.Add(new Vector2(0, 0));
            //    mesh.Uv.Add(new Vector2(1, 0));
            //    mesh.Uv.Add(new Vector2(1, 1));
            //}

            //left -x
            mesh.Vertices.Add(new Vector3(targetPos.x + 0.5f - segment.targetWidth / 2, targetPos.y, targetPos.z + 0.5f + segment.targetWidth / 2));
            mesh.Vertices.Add(new Vector3(basePos.x + 0.5f - segment.baseWidth / 2, basePos.y, basePos.z + 0.5f + segment.baseWidth / 2));
            mesh.Vertices.Add(new Vector3(basePos.x + 0.5f - segment.baseWidth / 2, basePos.y, basePos.z + 0.5f - segment.baseWidth / 2));
            mesh.Vertices.Add(new Vector3(targetPos.x + 0.5f - segment.targetWidth / 2, targetPos.y, targetPos.z + 0.5f - segment.targetWidth / 2));

            mesh.Triangles.Add(mesh.TriCount + 2);
            mesh.Triangles.Add(mesh.TriCount + 1);
            mesh.Triangles.Add(mesh.TriCount + 0);
            mesh.Triangles.Add(mesh.TriCount + 0);
            mesh.Triangles.Add(mesh.TriCount + 3);
            mesh.Triangles.Add(mesh.TriCount + 2);
            mesh.TriCount += 4;

            //back z
            mesh.Vertices.Add(new Vector3(targetPos.x + 0.5f - segment.targetWidth / 2, targetPos.y, targetPos.z + 1 - 0.5f + segment.targetWidth / 2));
            mesh.Vertices.Add(new Vector3(basePos.x + 0.5f - segment.baseWidth / 2, basePos.y, basePos.z + 1 - 0.5f + segment.baseWidth / 2));
            mesh.Vertices.Add(new Vector3(basePos.x + 0.5f + segment.baseWidth / 2, basePos.y, basePos.z + 1 - 0.5f + segment.baseWidth / 2));
            mesh.Vertices.Add(new Vector3(targetPos.x + 0.5f + segment.targetWidth / 2, targetPos.y, targetPos.z + 1 - 0.5f + segment.targetWidth / 2));

            mesh.Triangles.Add(mesh.TriCount + 0);
            mesh.Triangles.Add(mesh.TriCount + 1);
            mesh.Triangles.Add(mesh.TriCount + 2);
            mesh.Triangles.Add(mesh.TriCount + 2);
            mesh.Triangles.Add(mesh.TriCount + 3);
            mesh.Triangles.Add(mesh.TriCount + 0);
            mesh.TriCount += 4;

            //right x
            mesh.Vertices.Add(new Vector3(targetPos.x + 0.5f + segment.targetWidth / 2, targetPos.y, targetPos.z + 0.5f - segment.targetWidth / 2));
            mesh.Vertices.Add(new Vector3(basePos.x + 0.5f + segment.baseWidth / 2, basePos.y, basePos.z + 0.5f - segment.baseWidth / 2));
            mesh.Vertices.Add(new Vector3(basePos.x + 0.5f + segment.baseWidth / 2, basePos.y, basePos.z + 0.5f + segment.baseWidth / 2));
            mesh.Vertices.Add(new Vector3(targetPos.x + 0.5f + segment.targetWidth / 2, targetPos.y, targetPos.z + 0.5f + segment.targetWidth / 2));

            mesh.Triangles.Add(mesh.TriCount + 2);
            mesh.Triangles.Add(mesh.TriCount + 1);
            mesh.Triangles.Add(mesh.TriCount + 0);
            mesh.Triangles.Add(mesh.TriCount + 0);
            mesh.Triangles.Add(mesh.TriCount + 3);
            mesh.Triangles.Add(mesh.TriCount + 2);
            mesh.TriCount += 4;

            //front -z
            mesh.Vertices.Add(new Vector3(targetPos.x + 0.5f + segment.targetWidth / 2, targetPos.y, targetPos.z + 1 - 0.5f - segment.targetWidth / 2));
            mesh.Vertices.Add(new Vector3(basePos.x + 0.5f + segment.baseWidth / 2, basePos.y, basePos.z + 1 - 0.5f - segment.baseWidth / 2));
            mesh.Vertices.Add(new Vector3(basePos.x + 0.5f - segment.baseWidth / 2, basePos.y, basePos.z + 1 - 0.5f - segment.baseWidth / 2));
            mesh.Vertices.Add(new Vector3(targetPos.x + 0.5f - segment.targetWidth / 2, targetPos.y, targetPos.z + 1 - 0.5f - segment.targetWidth / 2));

            mesh.Triangles.Add(mesh.TriCount + 0);
            mesh.Triangles.Add(mesh.TriCount + 1);
            mesh.Triangles.Add(mesh.TriCount + 2);
            mesh.Triangles.Add(mesh.TriCount + 2);
            mesh.Triangles.Add(mesh.TriCount + 3);
            mesh.Triangles.Add(mesh.TriCount + 0);
            mesh.TriCount += 4;
        }
    }
}