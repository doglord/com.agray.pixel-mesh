using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace agray.PixelMesh
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
    public class PixelMeshRenderer : MonoBehaviour
    {
        public Texture2D PixelTexture;
        public Material DefaultMaterial;

        public bool GenerateOnAwake = true;

        Color32[] _pixels;
        Material _material;
        MeshFilter _filter;
        MeshCollider _collider;
        MeshRenderer _renderer;

        void Awake()
        {
            _filter = GetComponent<MeshFilter>();
            _collider = GetComponent<MeshCollider>();
            _renderer = GetComponent<MeshRenderer>();

            if (GenerateOnAwake)
                InitializePixelMesh(PixelTexture, DefaultMaterial);
        }

        void InitializePixelMesh(Texture2D pixelTexture, Material material = null)
        {
            SetMesh(pixelTexture.width, pixelTexture.height);
            SetMaterial(material);
            SetPixelTexture(pixelTexture);
        }

        void SetMesh(int width, int height)
        {
            var pixelMesh = PixelMeshUtils.GeneratePixelMesh(width, height);

            _filter.mesh = pixelMesh;
            _collider.sharedMesh = pixelMesh;
        }

        void SetMaterial(Material material)
        {
            material = material == null ? DefaultMaterial : material;

            var mat = new Material(material);

            _renderer.sharedMaterial = mat;
            _material = mat;
        }

        void SetPixelTexture(Texture2D texture)
        {
            SetPixelTexture(texture.width, texture.height, texture.GetPixels32());
        }
        void SetPixelTexture(int width, int height, Color32[] pixels)
        {
            var copy = new Texture2D(width, height, TextureFormat.RGBA32, false);
            copy.filterMode = FilterMode.Point;
            copy.SetPixels32(pixels);
            copy.Apply();

            _renderer.material.mainTexture = copy;

            _pixels = pixels;
            PixelTexture = copy;
        }




        // api
        public void UpdatePixelTexture(Color32[] pixels)
        {
            PixelTexture.SetPixels32(pixels);
            PixelTexture.Apply();

            _pixels = pixels;
        }

    }
}