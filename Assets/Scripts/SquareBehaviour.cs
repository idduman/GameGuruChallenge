using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameGuruChallenge
{
    public class SquareBehaviour : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _squareSprite;
        [SerializeField] private GameObject _markObject;


        private bool _highlight;
        private bool _selected;
        private bool _marked;

        public bool Highlight
        {
            get => _highlight;
            set
            {
                _highlight = value;
                _squareSprite.color = _highlight ? Color.green : Color.white;
            }
        }
        
        public bool Selected
        {
            get => _selected;
            set
            {
                _selected = value;
            }
        }

        public bool Marked
        {
            get => _marked;
            set
            {
                _marked = value;
                _markObject.SetActive(_marked);
            }
        }
    }
}