using UnityEngine;
using UnityEngine.UI;

public class BoostIndicator : MonoBehaviour
{
    [SerializeField] private Image boostImage;
    [SerializeField] private Image filler;

    public BoostType Type { get; set; }
    public Image BoostImage => boostImage;

    public void FillImage(float fillValue)
    {
        filler.fillAmount = fillValue;
    }
}
