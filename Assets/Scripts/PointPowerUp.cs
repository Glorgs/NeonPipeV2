using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PointPowerUp : PowerUp
{
    public int amountBonus;

    public override void Use(GameObject player)
    {
        player.GetComponentInParent<Painting>().AddScore(amountBonus);  
        ShowText.Si().ShowDamageNumber("BONUS PTS", transform.position, new Color(248f / 255, 95f / 255, 105f / 255), new Color(248f / 255, 223f / 255, 92f / 255));
    }
}
