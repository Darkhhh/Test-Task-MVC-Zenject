using System;
using UnityEngine;
using Random = UnityEngine.Random;

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
        
        
        #region Serialize Fields

        [SerializeField] private int defaultDepth;
        [Range(0f,1f)] public float goldProbability;
        [SerializeField] private GameObject goldIngot;

        #endregion


        #region Private Values

        private int _localDepth;
        private int _goldLevel;
        private bool _isIngotActive;
        private SpriteRenderer _spriteRenderer;

        #endregion


        #region Public Values

        public event Action<TileEvent> OnTileClick; 

        #endregion

        
        #region Event Functions

        void Start()
        {
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            if(defaultDepth < 1) Debug.LogError("Incorrect value for default depth");
            _localDepth = defaultDepth;
            _goldLevel = -1;
            _isIngotActive = false;
            goldIngot.SetActive(false);
            SolveGoldCondition();
        }
        
        void OnMouseDown()
        {
            if(_localDepth > 0 && !_isIngotActive) Digging();
        }

        #endregion


        #region Private Methods

        private void SolveGoldCondition(){
            if(Random.Range(0f,1f) < goldProbability)
                _goldLevel = Random.Range(1, defaultDepth);
        }
        
        private void Digging(){
            _localDepth--;
            SetOpacity((float)_localDepth/defaultDepth);
            if(_localDepth == _goldLevel) SetGold(true);
            OnTileClick?.Invoke(TileEvent.DecreaseShovel);
        }
        
        private void SetOpacity(float value)
        {
            var color = _spriteRenderer.color;
            color = new Color(color.a, color.g, color.b, value);
            _spriteRenderer.color = color;
        }
        
        private void SetGold(bool value){
            goldIngot.SetActive(value);
            _isIngotActive = true;
            goldIngot.GetComponent<IngotScript>().OnIngotClick += IncreaseGoldAmount;
        }
        
        private void IncreaseGoldAmount()
        {
            _isIngotActive = false;
            OnTileClick?.Invoke(TileEvent.IncreaseGold);
        }

        #endregion
    }
}