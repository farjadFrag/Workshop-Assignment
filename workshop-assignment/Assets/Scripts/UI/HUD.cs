using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private TMP_Text bullets;


    private void OnEnable()
    {
        Gun.onBulletUpdate += UpdateBulletCount;
    }

    private void OnDisable()
    {
        Gun.onBulletUpdate -= UpdateBulletCount;
    }

    private void UpdateBulletCount(int currentBullets, int maxBullets)
    {
        bullets.text = $"{currentBullets}/{maxBullets}";
    }
}
