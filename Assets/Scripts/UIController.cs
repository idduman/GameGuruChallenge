using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GameGuruChallenge
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _scoreText;

        public void Initialize()
        {
            SetMatches(0);
        }

        public void SetMatches(int m)
        {
            _scoreText.text = $"Matches: {m}";
        }

        public void RestartGame()
        {
            GameManager.Instance.LoadGame();
        }
    }
}

