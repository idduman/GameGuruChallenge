using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameGuruChallenge
{
    public class PanelController : MonoBehaviour
    {
        [SerializeField] private GamePanel _backgroundPanel;
        [SerializeField] private GamePanel _gridPanel;
        [SerializeField] [Range(0f, 0.5f)] private float _gridPanelMargin = 0.05f;
        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            var gridPrc = 1f - _gridPanelMargin;
            _backgroundPanel.Stretch(0f,0f,0f, 0f);
            var bgSize = _backgroundPanel.GetSize();
            var gridSize = new Vector3(bgSize.x * gridPrc, bgSize.x * gridPrc, 1f);
            _gridPanel.SetSize(gridSize);
            var gridSizeNrm = _gridPanel.GetNormalizedSize();
            _gridPanel.SetNormalizedPosition(new Vector3(0.5f, gridPrc - gridSizeNrm.y/2f, 1f));
        }
    }
}
