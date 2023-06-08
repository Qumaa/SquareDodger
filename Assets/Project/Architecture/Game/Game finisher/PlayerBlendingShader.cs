﻿using UnityEngine;

namespace Project.Architecture
{
    public class PlayerBlendingShader : IPlayerBlendingShader
    {
        private const string _MATERIAL_BLENDING_RADIUS_NAME = "_BlendingRadius";
        private const string _MATERIAL_BLENDING_WIDTH_NAME = "_BlendingLength";
        private const string _PLAYER_COLOR_PROPERTY_NAME = "_PlayerColor";
        private const string _BLENDING_COLOR_PROPERTY_NAME = "_ObstacleColor";
        private readonly int _blendingRadiusNameId = Shader.PropertyToID(_MATERIAL_BLENDING_RADIUS_NAME);
        private readonly int _blendingLengthNameId = Shader.PropertyToID(_MATERIAL_BLENDING_WIDTH_NAME);
        private readonly int _playerColorPropertyId = Shader.PropertyToID(_PLAYER_COLOR_PROPERTY_NAME);
        private readonly int _blendingColorPropertyId = Shader.PropertyToID(_BLENDING_COLOR_PROPERTY_NAME);

        private float _blendingRadius;
        private float _blendingLength;
        private Color32 _playerColor;
        private Color32 _blendingColor;
        private Material _material;

        public Material Material
        {
            get => _material;
            set => SetMaterial(value);
        }

        public Color32 PlayerColor
        {
            get => _playerColor;
            set => SetPlayerColor(value);
        }

        public Color32 BlendingColor
        {
            get => _blendingColor;
            set => SetBlendingColor(value);
        }

        public float BlendingRadius
        {
            get => _blendingRadius;
            set => SetBlendingRadius(value);
        }

        public float BlendingLength
        {
            get => _blendingLength;
            set => SetBlendingLength(value);
        }

        public float TotalBlendingLength => BlendingRadius + BlendingLength;

        private void SetMaterial(Material material)
        {
            _material = material;
            UpdateMaterialValues();
        }

        private void SetPlayerColor(Color32 color)
        {
            _playerColor = color;
            UpdateMaterialValues();
        }

        private void SetBlendingColor(Color32 color)
        {
            _blendingColor = color;
            UpdateMaterialValues();
        }

        private void SetBlendingRadius(float radius)
        {
            _blendingRadius = radius;
            UpdateMaterialValues();
        }

        private void SetBlendingLength(float length)
        {
            _blendingLength = length;
            UpdateMaterialValues();
        }

        private void UpdateMaterialValues()
        {
            if (_material == null)
                return;

            _material.SetFloat(_blendingRadiusNameId, _blendingRadius);
            _material.SetFloat(_blendingLengthNameId, _blendingLength);
            _material.SetColor(_playerColorPropertyId, _playerColor);
            _material.SetColor(_blendingColorPropertyId, _blendingColor);
        }
    }
}