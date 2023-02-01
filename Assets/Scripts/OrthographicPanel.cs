using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

namespace GameGuruChallenge
{
    public class OrthographicPanel : GamePanel
    {
        // Offset from the camera in Z direction
        [SerializeField] private float _cameraOffsetZ = 5f;
        
        private Camera _mainCamera;
        private Vector3 _lowerAnchor;
        private float _sizeY;
        private float _sizeX;
        private float _halfSizeY;
        private float _halfSizeX;

        private void Awake()
        {
            _mainCamera = Camera.main;
            _halfSizeY = _mainCamera.orthographicSize;
            _halfSizeX =  _mainCamera.aspect * _halfSizeY;
            _sizeY = 2f * _halfSizeY;
            _sizeX = 2f * _halfSizeX;
            _lowerAnchor = _mainCamera.transform.position +
                           new Vector3(-_halfSizeX, -_halfSizeY / 2f, _cameraOffsetZ);
        }

        public override Vector3 GetNormalizedPosition()
        {
            var pos = transform.position - _lowerAnchor;
            return new Vector3(pos.x / _sizeX, pos.y / _sizeY, pos.z);
        }

        public override void SetNormalizedPosition(Vector3 position)
        {
            transform.position = _lowerAnchor + 
                                 new Vector3(position.x * _sizeX,
                                     position.y * _sizeY,
                                     position.z);
        }

        public override Vector3 GetSize()
        {
            return transform.lossyScale;
        }

        public override void SetSize(Vector3 size)
        {
            var parent = transform.parent;
            transform.parent = transform.root;
            transform.localScale = size;
            transform.parent = parent;
        }

        public override Vector3 GetNormalizedSize()
        {
            var scale = transform.lossyScale;
            return new Vector3(scale.x / _sizeX, scale.y / _sizeY, scale.z);
        }

        public override void SetNormalizedSize(Vector3 size)
        {
            var parent = transform.parent;
            transform.parent = transform.root;
            transform.localScale = new Vector3(size.x * _sizeX, size.y * _sizeY, size.z);
            transform.parent = parent;
        }

        public override void Stretch(float top, float bottom, float left, float right)
        {
            var normalizedPos = GetNormalizedPosition();
            SetNormalizedPosition(new Vector3((0.5f + left - right) /2f,
                (0.5f + bottom - top)/2f, normalizedPos.z));

            var normalizedSize = GetNormalizedSize();
            SetNormalizedSize(new Vector3(1f - left - right, 1f - top - bottom, normalizedSize.z));
        }
    }
}

