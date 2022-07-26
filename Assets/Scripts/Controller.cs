using UnityEngine.SceneManagement;

public class Controller
{
    #region Private Values

    private readonly View _view;
    private readonly Model _model;

    #endregion
    

    public Controller(View view, Model model)
    {
        _view = view;
        _model = model;
        
        //View Events Handlers
        _view.OnRestartButtonClicked += HandleRestartEvent;
        _view.OnTileClicked += HandleTileDigEvent;
        _view.OnGoldClicked += HandleGoldClickedEvent;
        
        //Model Events Handlers
        _model.OnGoldIncreased += HandleIncreaseGoldEvent;
        _model.OnShovelsDecreased += HandleDecreaseShovelsEvent;
        _model.OnGoldIngotDepthAchieved += HandleGoldDepthAchieveEvent;
        _model.OnTileDug += HandleTileDugEvent;
        _model.OnGoldIngotGrabbed += HandleGoldGrabbedEvent;
        _model.OnEndGameAchieved += HandleEndGameAchievedEvent;
        _model.InitializeInterface();
    }

    
    #region Model Events Handlers

    private void HandleIncreaseGoldEvent(int goldNumber, int winGoldNumber)
    {
        _view.SetGoldAmount(goldNumber, winGoldNumber);
    }

    private void HandleEndGameAchievedEvent(string message)
    {
        _view.ActivateEndGamePanel(message);
    }
    
    private void HandleDecreaseShovelsEvent(int shovelsNumber, int defaultShovelsNumber)
    {
        _view.SetShovelsAmount(shovelsNumber, defaultShovelsNumber);
    }

    private void HandleTileDugEvent(int tileIndex, float opacity)
    {
        _view.DigTile(tileIndex, opacity);
    }

    private void HandleGoldGrabbedEvent(int tileIndex)
    {
        _view.SetActiveGoldIngotOnTile(tileIndex, false);
    }
    
    private void HandleGoldDepthAchieveEvent(int tileIndex)
    {
        _view.SetActiveGoldIngotOnTile(tileIndex, true);
    }

    #endregion

    
    #region View Events Handlers

    private static void HandleRestartEvent()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void HandleTileDigEvent(int tileIndex)
    {
        _model.DecreaseShovels(tileIndex);
    }

    private void HandleGoldClickedEvent(int tileIndex)
    {
        _model.IncreaseGold(tileIndex);
    }

    #endregion
}
