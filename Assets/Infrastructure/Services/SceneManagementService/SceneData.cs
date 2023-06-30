using UnityEngine;

[CreateAssetMenu(menuName = "SceneData", fileName = "SceneData", order = 1)]
public class SceneData : ScriptableObject
{
    [SerializeField] private Scene scene;
    [SerializeField] private string sceneName;
    public Scene Scene => scene;
    public string SceneName => sceneName;
}
