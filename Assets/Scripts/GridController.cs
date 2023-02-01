using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameGuruChallenge
{
    [RequireComponent(typeof(GridLayoutGroup))]
    public class GridController : MonoBehaviour
    {
        [SerializeField] private SquareBehaviour _squarePrefab;
        [SerializeField] private uint _gridSize = 3;
        [SerializeField] private float _gridSpacing = 0.05f;

        private GridLayoutGroup _gridLayout;

        private void Awake()
        {
            _gridLayout = GetComponent<GridLayoutGroup>();
            Initialize();
            
        }

        public void Initialize()
        {
            var size = 1f / _gridSize;
            var squareSize = size * (1f - 2f * _gridSpacing);
            _gridLayout.cellSize = size * Vector2.one;
            _squarePrefab.transform.localScale = new Vector3(squareSize, squareSize, 1f);
            
            for (int i = 0; i < _gridSize*_gridSize; i++)
            {
                Instantiate(_squarePrefab, transform);
            }
        }
    }

}
