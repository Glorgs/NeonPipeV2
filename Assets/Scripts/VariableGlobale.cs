using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariableGlobale : MySingleton<VariableGlobale>
{
    private int maxAmountPlayer = 1;

    [SerializeField]
    private int numberPlayer = 0;

    private float hue;
    private float sat = 1f;
    private float bri = 1f;
    private float rainbowSpeed = 10;

    public Color[] colorsPlayer;

    public Color rainbowColor;

    public GameObject prefabPaintPlayer1;

    public int maxHP = 100;
    private int currentHP;

    public int MaxHP
    {
        get
        {
            return maxHP;
        }
        set
        {
            maxHP = value;
        }
    }

    public int CurrentHP
    {
        get
        {
            return currentHP;
        }
        set
        {
            currentHP = value;
        }
    }
    
    public int MaxAmountPlayer
    {
        get
        {
            return maxAmountPlayer;
        }
        set
        {
            maxAmountPlayer = value;
        }
    }

    public int NumberPlayer
    {
        get
        {
            return numberPlayer;
        }
        set
        {
            numberPlayer = value;
        }
    }

    void Start()
    {
        currentHP = maxHP;
        colorsPlayer = new Color[2];
        rainbowColor = Color.HSVToRGB(hue, sat, bri) * 30;
    }

    void Update()
    {
        Color.RGBToHSV(rainbowColor, out hue, out sat, out bri);

        hue += rainbowSpeed / 1000;
        if (hue >= 1)
        {
            hue = 0;
        }
        rainbowColor = Color.HSVToRGB(hue, sat, bri);
    }

    public void SetColor(Color c, float intensity, int numPlayer)
    {
        colorsPlayer[numPlayer] = c * intensity;
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }

        if (currentHP < 0)
        {
            currentHP = 0;
        }
    }

}
