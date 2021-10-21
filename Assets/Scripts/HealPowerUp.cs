using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPowerUp : PowerUp
{
    public int amountHeal;

    public override void Use(GameObject player)
    {
        player.GetComponentInParent<PlayerManager>().playerUI.UpdateLifeBar(amountHeal);
    }
}
