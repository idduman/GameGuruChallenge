using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameGuruChallenge;
using UnityEngine;

namespace GameGuruChallenge
{
    public class GridSolver
    {
        public List<int> Evaluate(GridController grid, int index)
        {
            List<int> _indices = new();

            _indices.Add(index);
            int counter = 0;

            for (int i = 0; i < _indices.Count; i++)
            {
                var currentIndex = _indices[i];
                var row = currentIndex / grid.GridSize;
                var column = currentIndex % grid.GridSize;
                var topIndex = grid.GridSize * (row - 1) + column;
                var botIndex = grid.GridSize * (row + 1) + column;
                var leftIndex = grid.GridSize * row + column - 1;
                var rightIndex = grid.GridSize * row + column + 1;

                if(row - 1 >= 0  && !_indices.Contains(topIndex) && grid.Squares[topIndex].Marked)
                    _indices.Add(topIndex);
                if (row + 1 < grid.GridSize && !_indices.Contains(botIndex) && grid.Squares[botIndex].Marked)
                    _indices.Add(botIndex);
                if(column - 1 >= 0 && !_indices.Contains(leftIndex) && grid.Squares[leftIndex].Marked)
                    _indices.Add(leftIndex);
                if(column + 1 < grid.GridSize && !_indices.Contains(rightIndex) && grid.Squares[rightIndex].Marked)
                    _indices.Add(rightIndex);
            }

            return _indices;
        }
    }
}

