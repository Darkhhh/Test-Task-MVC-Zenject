using System;
using UnityEngine;

namespace Tiles
{
    public class IngotScript : MonoBehaviour
    {
        public event Action OnIngotClick;

        private void OnMouseDown(){
            OnIngotClick?.Invoke();
        }
    }
}