using System;
using System.Collections;
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

    public event Action OnTileDigged;
    public event Action OnGoldGrabbed;
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
        Debug.Log("Activating EndGame Panel");
        inGameCanvas.SetActive(false);
        endgameCanvas.SetActive(true);
        endgameText.text = message;
    }

    #endregion


    #region Handlers

    public void RestartButtonHandler()
    {
        OnRestartButtonClicked?.Invoke();
    }

    private void HandleTileClickEvent(TileScript.TileEvent tileEvent)
    {
        switch (tileEvent)
        {
            case TileScript.TileEvent.IncreaseGold:
                OnGoldGrabbed?.Invoke();
                break;
            case TileScript.TileEvent.DecreaseShovel:
                OnTileDigged?.Invoke();
                break;
        }
    }

    #endregion
}
