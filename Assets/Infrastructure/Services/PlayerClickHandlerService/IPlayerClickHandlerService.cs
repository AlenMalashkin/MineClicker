using System;

public interface IPlayerClickHandlerService
{
    event Action PlayerClickHandled;
    void HandlePlayerClick();
}
