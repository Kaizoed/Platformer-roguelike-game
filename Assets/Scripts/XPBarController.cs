using UnityEngine;

public class XPBarController : MonoBehaviour
{
    public RectTransform xpFill;

    public float maxXP = 100;
    public float currentXP = 0;

    private float fullWidth;

    void Start()
    {
        fullWidth = xpFill.sizeDelta.x;
        UpdateXPBar();
    }

    public void GainXP(float amount)
    {
        currentXP = Mathf.Clamp(currentXP + amount, 0, maxXP);
        UpdateXPBar();
    }

    void UpdateXPBar()
    {
        float width = (currentXP / maxXP) * fullWidth;
        xpFill.sizeDelta = new Vector2(width, xpFill.sizeDelta.y);
    }
}