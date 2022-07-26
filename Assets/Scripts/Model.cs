using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class Model
{
    #region Private Values

    private int _shovelsNumber;
    private readonly int _defaultShovelsNumber;
    private int _goldNumber;
    private readonly int _winGoldNumber;
    private readonly int _defaultDepth;
    private List<TileModel> _tiles;

    #endregion

    
    #region Events

    public event Action<string> OnEndGameAchieved;
    public event Action<int, int> OnGoldIncreased; 
    public event Action<int, int> OnShovelsDecreased;
    
    public event Action<int, float> OnTileDug;
    public event Action<int> OnGoldIngotGrabbed;
    public event Action<int> OnGoldIngotDepthAchieved;

    #endregion
    
    
    public Model(int defaultShovelsNumber, int winGoldNumber, int tilesInRow, float goldProbability, int defaultDepth)
    {
        CreateTileModels(tilesInRow * tilesInRow, goldProbability, defaultDepth);
        _shovelsNumber = _defaultShovelsNumber = defaultShovelsNumber;
        _winGoldNumber = winGoldNumber;
        _goldNumber = 0;
        _defaultDepth = defaultDepth;
    }


    #region Private Methods

    private void CreateTileModels(int allTilesNumber, float goldProbability, int defaultDepth)
    {
        _tiles = new List<TileModel>();
        for (var i = 0; i < allTilesNumber; i++)
        {
            var goldDepth = -1;
            if(Random.Range(0f,1f) < goldProbability) goldDepth = Random.Range(1, defaultDepth);
            _tiles.Add(new TileModel {CurrentDepth = defaultDepth, IngotDepth = goldDepth, IsIngotActive = false});
        }
    }

    #endregion
    
    
    #region Public Methods

    public void InitializeInterface()
    {
        OnGoldIncreased?.Invoke(_goldNumber, _winGoldNumber);
        OnShovelsDecreased?.Invoke(_shovelsNumber, _defaultShovelsNumber);
    }
    
    public void IncreaseGold(int tileIndex)
    {
        _goldNumber++;
        OnGoldIncreased?.Invoke(_goldNumber, _winGoldNumber);
        _tiles[tileIndex].IsIngotActive = false;
        OnGoldIngotGrabbed?.Invoke(tileIndex);
        if(_goldNumber >= _winGoldNumber) OnEndGameAchieved?.Invoke("Nice, you've found all gold!");
    }

    public void DecreaseShovels(int tileIndex)
    {
        if (_tiles[tileIndex].IsIngotActive) return;
        if (_tiles[tileIndex].CurrentDepth < 1) return;
        _shovelsNumber--;
        _tiles[tileIndex].CurrentDepth--;
        OnShovelsDecreased?.Invoke(_shovelsNumber, _defaultShovelsNumber);
        OnTileDug?.Invoke(tileIndex, (float)_tiles[tileIndex].CurrentDepth/_defaultDepth);
        if (_tiles[tileIndex].CurrentDepth == _tiles[tileIndex].IngotDepth)
        {
            _tiles[tileIndex].IsIngotActive = true;
            OnGoldIngotDepthAchieved?.Invoke(tileIndex);
        }
        if(_shovelsNumber < 1) OnEndGameAchieved?.Invoke("Sorry, you're out of shovels");
    }

    #endregion


    #region Tile Model

    private class TileModel
    {
        public int CurrentDepth { get; set; }
        public bool IsIngotActive { get; set; }
        public int IngotDepth { get; set; }
    }

    #endregion
}
