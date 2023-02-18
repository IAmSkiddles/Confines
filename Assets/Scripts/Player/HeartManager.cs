using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;
    public Image heartImage;

    private void Awake()
    {
        heartImage = GetComponent<Image>();
    }

    public void SetHeartState(HeartStatus status)
    {
        switch (status)
        {
            case HeartStatus.EMPTY:
                heartImage.sprite = emptyHeart; break;
            case HeartStatus.HALF:
                heartImage.sprite = halfHeart; break;
            case HeartStatus.FULL:
                heartImage.sprite = fullHeart; break;
        }
    }

    public enum HeartStatus
    {
        EMPTY = 0,
        HALF = 1,
        FULL = 2
    }
}
