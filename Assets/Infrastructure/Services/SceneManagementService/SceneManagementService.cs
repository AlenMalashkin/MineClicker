using UnityEngine.SceneManagement;

public class SceneManagementService : ISceneManagementService
{
    private IStaticDataLoadService _staticDataLoadService;
    
    public SceneManagementService(IStaticDataLoadService staticDataLoadService)
    {
        _staticDataLoadService = staticDataLoadService;
    }
    
    public void LoadScene(Scene scene)
    {
        SceneData sceneData = _staticDataLoadService.ForScene(scene);
        SceneManager.LoadScene(sceneData.SceneName);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
