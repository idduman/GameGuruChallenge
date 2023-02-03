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

        [SerializeField] private List<GamePanel> _testPanels = new();

        public void Initialize()
        {
            for (int i = 0; i < _testPanels.Count; i++)
            {
                _testPanels[i].SetSize(Vector3.one);
                _testPanels[i].SetNormalizedPosition(((i + 0.5f) * Vector3.up + 0.5f * Vector3.right)
                    / _testPanels.Count);
            }
            
            
            var gridPrc = 1f - _gridPanelMargin;
            _backgroundPanel.Stretch(0f,0f,0f, 0f);
            var bgSize = _backgroundPanel.GetSize();
            var gridSize = new Vector3(bgSize.x * gridPrc, bgSize.x * gridPrc, 1f);
            _gridPanel.SetSize(gridSize);
            var gridSizeNrm = _gridPanel.GetNormalizedSize();
            _gridPanel.SetNormalizedPosition(new Vector3(0.5f, gridPrc - gridSizeNrm.y/2f, 1f));
            //Debug.Log(_backgroundPanel.GetNormalizedPosition());
            //Debug.Log(_gridPanel.GetNormalizedPosition());
        }
    }
}
