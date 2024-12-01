using UnityEngine;
using UnityEngine.UI;

public class ResizeImage : MonoBehaviour
{
    public Image image;
    public int newWidth = 100;
    public int newHeight = 100;

    void Start()
    {
        image = GetComponent<Image>();
        // 获取图片的 RectTransform
        RectTransform rectTransform = image.GetComponent<RectTransform>();

        // 调整图片的大小
        rectTransform.sizeDelta = new Vector2(newWidth, newHeight);
    }
}
