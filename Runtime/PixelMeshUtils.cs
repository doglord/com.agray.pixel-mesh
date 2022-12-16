using System.Collections.Generic;
using UnityEngine;

namespace agray.PixelMesh
{
    static class PixelMeshUtils
    {
        public static Mesh GeneratePixelMesh(int _width, int _height)
        {
            var mesh = new Mesh
            {
                name = "PixelMesh",
                indexFormat = UnityEngine.Rendering.IndexFormat.UInt32
            };

            var i = 0;
            var verts = new List<Vector3>();
            var norms = new List<Vector3>();
            var uvs = new List<Vector2>();
            var tris = new List<int>();

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    var quadPts = new List<Vector3>(){
                    new Vector3(x - 0.5f, 0f, y - 0.5f),
                    new Vector3(x + 0.5f, 0f, y - 0.5f),
                    new Vector3(x - 0.5f, 0f, y + 0.5f),
                    new Vector3(x + 0.5f, 0f, y + 0.5f),
                };

                    var normals = new List<Vector3>() {
                    Vector3.up,
                    Vector3.up,
                    Vector3.up,
                    Vector3.up,
                };


                    var yuves = new List<Vector2>() {
                    new Vector2(((float)x - 0)/(float)_width, ((float)y - 0f)/(float)_height),
                    new Vector2(((float)x+ 1f)/(float)_width, ((float)y - 0f)/(float)_height),
                    new Vector2(((float)x - 0f)/(float)_width, ((float)y+ 1f)/(float)_height),
                    new Vector2(((float)x+ 1f)/(float)_width, ((float)y+ 1f)/(float)_height),
                };

                    var triangles = new List<int>
                {
                    i, i + 2, i + 1,
                    i + 1, i + 2, i + 3
                };

                    verts.AddRange(quadPts);
                    norms.AddRange(normals);
                    uvs.AddRange(yuves);
                    tris.AddRange(triangles);

                    i += 4;
                }
            }

            mesh.vertices = verts.ToArray();
            mesh.normals = norms.ToArray();
            mesh.uv = uvs.ToArray();
            mesh.triangles = tris.ToArray();

            return mesh;
        }
    }
}