using System.Collections.Generic;
using UnityEngine;

public interface IGameFactory
{
    List<IProgressUpdater> ProgressUpdaters { get; }
    GameObject CreateEnemy(EnemyType type, Transform at);
    GameObject CreatePlayer(Transform at);
    GameObject CreateHud(Transform at);
    GameObject CreateShopItem(ItemType type, Transform at);
}
