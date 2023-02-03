using System;
using System.Collections;
using System.Collections.Generic;
using HyperCore;
using UnityEngine;

namespace GameGuruChallenge
{
    public class GameManager : SingletonBehaviour<GameManager>
    {
        [SerializeField] private PanelController _panelController;
        [SerializeField] private UIController _uiController;
        [SerializeField] private GridController _gridController;
        [Range(3,20)][SerializeField] private uint _gridSize = 3;
        private int _matches;
        private int _score;

        private void Start()
        {
            LoadGame();
        }

        private void OnDestroy()
        {
            _gridController.Scored -= OnScored;
        }

        public void LoadGame()
        {
            _score = 0;
            _matches = 0;
            _gridController.Scored -= OnScored;
            
            _panelController.Initialize();
            _gridController.Initialize(_gridSize);
            _uiController.Initialize();
            _gridController.Scored += OnScored;
        }

        private void OnScored(int score)
        {
            _score += score;
            _matches++;
            _uiController.SetMatches(_matches);
        }
    }

}
