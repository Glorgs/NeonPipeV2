using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPowerUp : PowerUp
{
    public int amountHeal;

    public override void Use(GameObject player)
    {
        VariableGlobale.Si().Heal(amountHeal);
        player.GetComponentInParent<PlayerManager>().playerUI.UpdateLifeBar();
    }
}
