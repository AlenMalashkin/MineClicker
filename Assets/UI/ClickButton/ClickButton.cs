using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ClickButton : MonoBehaviour
{
    [SerializeField] private Button clickButton;

    private IPlayerClickHandlerService _playerClickHandlerService;

    [Inject]
    private void Construct(IPlayerClickHandlerService playerClickHandlerService)
    {
        _playerClickHandlerService = playerClickHandlerService;
    }
    
    private void OnEnable()
    {
        clickButton.onClick.AddListener(Click);
    }

    private void OnDisable()
    {
        clickButton.onClick.RemoveListener(Click);
    }

    private void Click()
    {
        _playerClickHandlerService.HandlePlayerClick();
    }
}
