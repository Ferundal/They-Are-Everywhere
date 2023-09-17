using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Transperancy : MonoBehaviour
{
    private Image _image;
    private void Awake()
    {
        _image = GetComponent<Image>();
        PauseManager._instance.OnStateChanged += OnStateChanged;
    }
    public void ChangeTransperancy(float number)
    {
        UnityEngine.Color __alpha = _image.color;
        __alpha.a = number;
        _image.color = __alpha;
    }

    private void OnStateChanged(bool isPaused)
    {
        if (isPaused)
        {
            UnityEngine.Color __alpha = _image.color;
            __alpha.a = 1;
            _image.color = __alpha;
            gameObject.SetActive(false);
        }
        else gameObject.SetActive(true);
    }
}
