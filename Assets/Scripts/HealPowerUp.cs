using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPowerUp : PowerUp
{
    public int amountHeal;

    public override void Use(GameObject player)
    {
        player.GetComponentInParent<PlayerManager>().playerUI.UpdateLifeBar(amountHeal);
        ShowText.Si().ShowDamageNumber("HP UP", transform.position, new Color(248f / 255, 95f / 255, 105f / 255), new Color(248f / 255, 223f / 255, 92f / 255));
    }
}
