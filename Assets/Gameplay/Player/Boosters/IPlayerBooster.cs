using System;
using VavilichevGD.Utils.Timing;

public interface IPlayerBooster
{
    BoosterData BoosterData { get; }
    SyncedTimer Timer { get; }
    void StartBoost();
    void TickBoost();
    void EndBoost();
}
