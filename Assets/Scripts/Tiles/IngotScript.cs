using System;
using UnityEngine;

namespace Tiles
{
    public class IngotScript : MonoBehaviour
    {
        public event Action OnIngotClick;

        void OnMouseDown(){
            OnIngotClick?.Invoke();
            gameObject.SetActive(false);
        }
    }
}