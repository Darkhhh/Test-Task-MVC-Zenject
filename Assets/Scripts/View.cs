using System;
using System.Collections.Generic;
using Tiles;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class View : MonoBehaviour
{
    #region Serialize Fields

    [SerializeField] private GameObject inGameCanvas;
    [SerializeField] private GameObject endgameCanvas;

    [SerializeField] private Text endgameText;
    [SerializeField] private Text shovelsAmountText;
    [SerializeField] private Text goldAmountText;

    #endregion


    #region Private Values

    private List<TileScript> _tiles;

    #endregion
    

    #region Events

    public event Action<int> OnTileClicked;
    public event Action<int> OnGoldClicked;
    public event Action OnRestartButtonClicked;

    #endregion
    
    
    #region Event Functions

    [Inject]
    private void Construct(List<TileScript> tiles)
    {
        _tiles = tiles;
        foreach (var tile in _tiles)
        {
            tile.OnTileClick += HandleTileClickEvent;
        }
    }
    
    private void Awake()
    {
        inGameCanvas.SetActive(true);
        endgameCanvas.SetActive(false);
    }

    #endregion
        
        
    #region Public Methods

    public void SetShovelsAmount(int shovelsAmount, int defaultShovelsAmount)
    {
        var text = "Shovels: " + shovelsAmount + "/" + defaultShovelsAmount;
        shovelsAmountText.text = text;
    }

    public void SetGoldAmount(int goldAmount, int winGoldAmount)
    {
        var text = "Gold: " + goldAmount + "/" + winGoldAmount;
        goldAmountText.text = text;
    }

    public void ActivateEndGamePanel(string message)
    {
        inGameCanvas.SetActive(false);
        endgameCanvas.SetActive(true);
        endgameText.text = message;
    }

    public void SetActiveGoldIngotOnTile(int tileIndex, bool active)
    {
        _tiles[tileIndex].SetIngotActive(active);
    }

    public void DigTile(int tileIndex, float opacity)
    {
        _tiles[tileIndex].SetOpacity(opacity);
    }

    #endregion


    #region Handlers

    public void RestartButtonHandler()
    {
        OnRestartButtonClicked?.Invoke();
    }

    private void HandleTileClickEvent(TileScript.TileEvent tileEvent, TileScript tile)
    {
        var tileIndex = -1;
        for (var i = 0; i < _tiles.Count; i++)
        {
            if (!ReferenceEquals(tile, _tiles[i])) continue;
            tileIndex = i;
            break;
        }

        if (tileIndex == -1) throw new Exception("TASK: Couldn't find tile");
        switch (tileEvent)
        {
            case TileScript.TileEvent.IncreaseGold:
                OnGoldClicked?.Invoke(tileIndex);
                break;
            case TileScript.TileEvent.DecreaseShovel:
                OnTileClicked?.Invoke(tileIndex);
                break;
        }
    }

    #endregion
}
