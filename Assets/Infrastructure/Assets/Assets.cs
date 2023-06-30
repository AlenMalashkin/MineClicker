using UnityEngine;

public class Assets : IAssets
{
    public GameObject GetPrefab(string path)
        => Resources.Load<GameObject>(path);
}
