using System;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneObjectsProvider
{ 
    Dictionary<Type, GameObject> SceneObjectsMap { get; }
}
