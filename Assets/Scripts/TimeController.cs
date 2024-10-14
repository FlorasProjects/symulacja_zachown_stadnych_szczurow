using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    [SerializeField]
    public Slider slider;

    float fixedTime = 0f;
    float maxfixedTime = 0f;
    void Start()
    {
        fixedTime = Time.fixedDeltaTime;
        maxfixedTime = Time.fixedDeltaTime;

        SetSlider();
    }

    private void SetSlider()
    {

        slider.value = 1f;
    }
    public void ChangeTimeScale()
    {
        Time.timeScale = slider.value;
        Time.fixedDeltaTime = Mathf.Clamp(fixedTime * Time.timeScale, 0f, maxfixedTime);
    }
}
