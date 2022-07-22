using System;

public class Model
{
    #region Private Values

    private int _shovelsNumber;
    private int _goldNumber;

    #endregion

    #region Events

    public event Action<int> OnGoldIncreased; 
    public event Action<int> OnShovelsDecreased; 

    #endregion
    
    public Model(int defaultShovelsNumber)
    {
        _shovelsNumber = defaultShovelsNumber;
        _goldNumber = 0;
    }

    public void IncreaseGold()
    {
        _goldNumber++;
        OnGoldIncreased?.Invoke(_goldNumber);
    }

    public void DecreaseShovels()
    {
        _shovelsNumber--;
        OnShovelsDecreased?.Invoke(_shovelsNumber);
    }
}
