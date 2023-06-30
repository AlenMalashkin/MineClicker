using System;

public class PlayerClickHandlerService : IPlayerClickHandlerService
{
    public event Action PlayerClickHandled;
    
    public void HandlePlayerClick()
    {
        PlayerClickHandled?.Invoke();
    }
}
