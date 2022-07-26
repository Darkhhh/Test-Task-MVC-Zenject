using System;
using UnityEngine;

namespace Tiles
{
    public class TileScript : MonoBehaviour
    {
        #region Enum
     
        public enum TileEvent
        {
            DecreaseShovel, IncreaseGold
        }
     
        #endregion

        #region Serialized Fields

        [SerializeField] private GameObject goldIngot;

        #endregion

        #region Events

        public event Action<TileEvent, TileScript> OnTileClick;

        #endregion

        #region Private Values

        private SpriteRenderer _spriteRenderer;
        private IngotScript _ingotScript;

        #endregion


        
        #region Event Functions

        private void Start()
        {
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            _ingotScript = goldIngot.GetComponent<IngotScript>();
            _ingotScript.OnIngotClick += HandleIngotClick;
            SetIngotActive(false);
        }

        private void OnMouseDown()
        {
            OnTileClick?.Invoke(TileEvent.DecreaseShovel, this);
        }

        #endregion


        #region Private Methods

        private void HandleIngotClick()
        {
            OnTileClick?.Invoke(TileEvent.IncreaseGold, this);
        }

        #endregion


        #region Public Methods

        public void SetOpacity(float value)
        {
            var color = _spriteRenderer.color;
            color = new Color(color.a, color.g, color.b, value);
            _spriteRenderer.color = color;
        }

        public void SetIngotActive(bool value)
        {
            goldIngot.SetActive(value);
        }

        #endregion
    }
}