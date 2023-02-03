using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace GameGuruChallenge
{
    [RequireComponent(typeof(GridLayoutGroup))]
    public class GridController : MonoBehaviour
    {
        public event Action<int> Scored;

        [SerializeField] private SquareBehaviour _squarePrefab;
        [SerializeField] private float _gridSpacing = 0.05f;
        private uint _gridSize = 3;
        public int GridSize => (int)_gridSize;

        private GridLayoutGroup _gridLayout;
        private Camera _mainCamera;
        private LayerMask _squareMask;

        private List<SquareBehaviour> _squares = new();
        public List<SquareBehaviour> Squares => _squares;

        private SquareBehaviour _highlightedSquare;
        private SquareBehaviour _selectedSquare;

        private GridSolver _gridSolver;

        private void Awake()
        {
            _mainCamera = Camera.main;
            _gridLayout = GetComponent<GridLayoutGroup>();
            _squareMask = LayerMask.GetMask("Square");
        }

        private void OnDestroy()
        {
            InputController.Moved -= OnMoved;
            InputController.Pressed -= OnPressed;
            InputController.Released -= OnReleased;
        }

        public void Initialize(uint gridSize)
        {
            _gridSize = gridSize;
            _gridSolver = new GridSolver();
            var size = 1f / _gridSize;
            var squareSize = size * (1f - 2f * _gridSpacing);
            _gridLayout.cellSize = size * Vector2.one;
            
            foreach (var s in _squares)
            {
                Destroy(s.gameObject);
            }
            _squares.Clear();

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
            if (!_highlightedSquare || !_selectedSquare)
                return;
            
            Ray ray = _mainCamera.ScreenPointToRay(pos);
            
            if (!Physics.Raycast(ray, out var hit, int.MaxValue, _squareMask)
                || !hit.transform.TryGetComponent<SquareBehaviour>(out var square)
                || _selectedSquare != square)
                return;

            _selectedSquare.Highlight = false;
            _selectedSquare.Selected = false;
            _selectedSquare.Marked = true;
            
            var index = _squares.FindIndex(s => s.gameObject == _selectedSquare.gameObject);
            var indices = _gridSolver.Evaluate(this, index);

            _selectedSquare = null;

            if (indices.Count < 3)
                return;
            
            foreach (var i in indices)
            {
                _squares[i].Marked = false;
            }

            Scored?.Invoke(indices.Count);
        }
    }

}
