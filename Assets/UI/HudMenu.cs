using UnityEngine;

public class HudMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    
    public void OpenHudMenu()
    {
        menuPanel.SetActive(true);
    }

    public void CloseHudMenu()
    {
        menuPanel.SetActive(false);
    }
}
