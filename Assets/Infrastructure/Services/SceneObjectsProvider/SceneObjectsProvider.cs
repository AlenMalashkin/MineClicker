using System;
using System.Collections.Generic;
using UnityEngine;

public class SceneObjectsProvider : ISceneObjectsProvider
{
    public Dictionary<Type, GameObject> SceneObjectsMap { get; }

    public SceneObjectsProvider()
    {
        SceneObjectsMap = new Dictionary<Type, GameObject>();
    }
}
