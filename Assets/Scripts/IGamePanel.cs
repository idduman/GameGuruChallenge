using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameGuruChallenge
{
    public interface IGamePanel
    {
        // Get the panels current absolute position
        //public Vector3 GetPosition();
    
        // Move the panel to the given absolute position
        //public void SetPosition(Vector3 position);
        
        // Get the panels current normalized position
        public Vector3 GetNormalizedPosition();
    
        // Move the panel to the given normalized position
        public void SetNormalizedPosition(Vector3 position);
        
        // Get the panels current absolute size
        public Vector3 GetSize();
    
        // Resize the panel to the given absolute size
        public void SetSize(Vector3 size);

        // Get the panels current normalized size
        public Vector3 GetNormalizedSize();
    
        // Resize the panel to the normalized size
        public void SetNormalizedSize(Vector3 size);

        //Resize and stretch to panel to fit the screen with given normalized margins
        public void Stretch(float top, float bottom, float left, float right);
    }

}
