using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    // text로 하려고 했는데 마땅한게 없음
    
    public Sprite[] countdownsImage = new Sprite[4];

    public Image image;
    public void SetImage(int index)
    {
        image.sprite = countdownsImage[index];
    }
    
}
