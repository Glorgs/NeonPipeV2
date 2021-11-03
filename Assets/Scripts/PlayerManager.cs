using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int numberHP = 3;
    public float invulnerabilityTime = 1f;
    public int blinkFrame = 3;
    public string damageSFX;

    public InGamePlayerUI playerUI;
    public int scoreLostOnCollision = 5;

    private Renderer playerRenderer;
    private Rigidbody playerBody;

    private Painting playerPainting;

    private int damageCollision = 20;
    private int hitTimes = 0;

    public int HitTimes
    {
        get
        {
            return hitTimes;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerRenderer = GetComponentInChildren<Renderer>();
        playerBody = GetComponentInChildren<Rigidbody>();
        playerPainting = GetComponent<Painting>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        {
            Damage();
            Debug.Log("Collision");
        }
    }

    void Damage()
    {
        numberHP--;
        hitTimes++;
        ScreenShake.Si().Shake();

        if (damageSFX != null)
        {
            AudioManager.Si().PlaySFX(damageSFX, AudioManager.Si().gameObject);
        }
        
        playerPainting.AddScore(-scoreLostOnCollision);


        playerUI.UpdateLifeBar(-damageCollision);

        //playerUI.UpdateScoreText(playerPainting.GetScore());

        if (VariableGlobale.Si().CurrentHP <= 0)
        {
            UIManager.Si().ShowEndMenu();
        }
        else
        {
            StartInvulnerability(invulnerabilityTime);
        }
    }

    private IEnumerator Invulnerability(float duration = 1.0f)
    {
        float t = 0;
        int blink = 0;

        Physics.IgnoreLayerCollision(0, 0);
        //playerBody.detectCollisions = false;
        playerRenderer.enabled = false;

        while (t < duration)
        {
            blink++;
            if(blink > blinkFrame)
            {
                blink = 0;
                playerRenderer.enabled = !playerRenderer.enabled;
            }
            t += Time.deltaTime;
            yield return null;
        }

        Physics.IgnoreLayerCollision(0, 0, false);
        //playerBody.detectCollisions = true;
        playerRenderer.enabled = true;
    }

    public void StartInvulnerability(float time)
    {
        StartCoroutine(Invulnerability(time));
    }


}
