using UnityEngine.SceneManagement;

public class Controller
{
    #region Private Values

    private readonly View _view;
    private readonly Model _model;
    private readonly int _winGoldNumber;
    private readonly int _defaultShovelsNumber;

    #endregion
    

    public Controller(View view, Model model, int winGoldNumber, int defaultShovelsNumber)
    {
        _view = view;
        _model = model;
        _winGoldNumber = winGoldNumber;
        _defaultShovelsNumber = defaultShovelsNumber;
        _view.OnRestartButtonClicked += HandleRestartEvent;
        _view.OnTileDigged += HandleTileDigEvent;
        _view.OnGoldGrabbed += HandleGoldGrabbedEvent;
        _model.OnGoldIncreased += HandleIncreaseGoldEvent;
        _model.OnShovelsDecreased += HandleDecreaseShovelsEvent;
        
        _view.SetGoldAmount(0, _winGoldNumber);
        _view.SetShovelsAmount(defaultShovelsNumber, defaultShovelsNumber);
    }

    
    #region Events Handlers

    private void HandleIncreaseGoldEvent(int goldNumber)
    {
        _view.SetGoldAmount(goldNumber, _winGoldNumber);
        if(goldNumber >= _winGoldNumber) _view.ActivateEndGamePanel("Nice, you've found all gold!");
    }
    
    private void HandleDecreaseShovelsEvent(int shovelsNumber)
    {
        _view.SetShovelsAmount(shovelsNumber, _defaultShovelsNumber);
        if(shovelsNumber <= 0) _view.ActivateEndGamePanel("Sorry, you're out of shovels");
    }

    private void HandleRestartEvent()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void HandleTileDigEvent()
    {
        _model.DecreaseShovels();
    }

    private void HandleGoldGrabbedEvent()
    {
        _model.IncreaseGold();
    }

    #endregion
}
