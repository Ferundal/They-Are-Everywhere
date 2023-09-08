using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(Image))]
public class Transperancy : MonoBehaviour
{
    private Image _image;
    private void Awake()
    {
        _image = GetComponent<Image>();
    }
    public void ChangeTransperancy(float number)
    {
        UnityEngine.Color __alpha = _image.color;
        __alpha.a = number;
        _image.color = __alpha;
    }
}
