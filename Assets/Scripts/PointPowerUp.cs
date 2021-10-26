using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PointPowerUp : PowerUp
{
    public int amountBonus;

    public override void Use(GameObject player)
    {
        player.GetComponentInParent<Painting>().AddScore(amountBonus);  
        ShowText.Si().ShowDamageNumber("1000 pts", transform.position);
    }
}
