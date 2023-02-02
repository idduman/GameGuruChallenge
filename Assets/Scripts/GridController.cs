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
        private Camera _mainCamera;
        private LayerMask _squareMask;

        private List<SquareBehaviour> _squares = new();

        private SquareBehaviour _highlightedSquare;
        private SquareBehaviour _selectedSquare;

        private void Awake()
        {
            _mainCamera = Camera.main;
            _gridLayout = GetComponent<GridLayoutGroup>();
            _squareMask = LayerMask.GetMask("Square");
            Initialize();
            
        }

        private void OnDestroy()
        {
            InputController.Moved -= OnMoved;
            InputController.Pressed -= OnPressed;
            InputController.Released -= OnReleased;
        }

        public void Initialize()
        {
            var size = 1f / _gridSize;
            var squareSize = size * (1f - 2f * _gridSpacing);
            _gridLayout.cellSize = size * Vector2.one;

            for (int i = 0; i < _gridSize*_gridSize; i++)
            {
                var square = Instantiate(_squarePrefab, transform);
                square.transform.localScale = new Vector3(squareSize, squareSize, 1f);
                square.Highlight = false;
                square.Selected = false;
                square.Marked = false;
                _squares.Add(square);
            }

            InputController.Moved += OnMoved;
            InputController.Pressed += OnPressed;
            InputController.Released += OnReleased;
        }

        private void OnMoved(Vector3 pos)
        {
            Debug.Log("Moved");
            Ray ray = _mainCamera.ScreenPointToRay(pos);

            if (!Physics.Raycast(ray, out var hit, int.MaxValue, _squareMask)
                || !hit.transform.TryGetComponent<SquareBehaviour>(out var square)
                || !_squares.Contains(square)
                || square.Marked)
            {
                if (!_highlightedSquare)
                    return;
                
                _highlightedSquare.Highlight = false;
                _highlightedSquare = null;
                return;
            }
            
            if (square == _highlightedSquare) 
                return;

            if (_highlightedSquare)
                _highlightedSquare.Highlight = false;
            
            square.Highlight = true;
            Debug.Log("Highlighted: " + square);

            _highlightedSquare = square;
        }

        private void OnPressed(Vector3 pos)
        {
            if (!_highlightedSquare)
                return;
            
            _selectedSquare = _highlightedSquare;
            _selectedSquare.Selected = true;
        }
        
        private void OnReleased(Vector3 pos)
        {
            if (!_selectedSquare)
                return;
            
            Ray ray = _mainCamera.ScreenPointToRay(pos);
            
            if (!Physics.Raycast(ray, out var hit, int.MaxValue, _squareMask)
                || !hit.transform.TryGetComponent<SquareBehaviour>(out var square)
                || _selectedSquare != square)
                return;

            _selectedSquare.Highlight = false;
            _selectedSquare.Selected = false;
            _selectedSquare.Marked = true;
        }
    }

}
