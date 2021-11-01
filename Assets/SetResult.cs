using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetResult : MonoBehaviour
{
    [SerializeField] private PlayerManager[] PlayerHit;
    [SerializeField] private Painting[] PlayerScore;
    [SerializeField] private TextMeshProUGUI[] scoreText;
    [SerializeField] private TextMeshProUGUI[] hitText;

    public void SetEnd()
    {
        for(int i = 0; i<PlayerScore.Length;i++)
        {
            scoreText[i].SetText(PlayerScore[i].Score.ToString());
            hitText[i].SetText(PlayerHit[i].HitTimes.ToString());
        }
    }
}
