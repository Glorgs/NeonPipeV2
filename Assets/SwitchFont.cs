using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SwitchFont : MonoBehaviour
{
    public TMP_FontAsset hoverfont;
    public Color hoverColor;

    public TMP_FontAsset font;
    public Color color;

    private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void fontHover()
    {
        text.font = hoverfont;
        text.color = hoverColor;
    }

    public void fontBase()
    {
        text.font = font;
        text.color = color;
    }

    public void OnDisable()
    {
        fontBase();
    }
}
