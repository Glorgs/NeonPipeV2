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
    private bool isInvulnerable = false;

    private float tInvul;
    private float durationInvul;

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

    private IEnumerator Invulnerability()
    {
        tInvul = 0;
        int blink = 0;

        isInvulnerable = true;
        Physics.IgnoreLayerCollision(0, 0);
        //playerBody.detectCollisions = false;
        playerRenderer.enabled = false;

        while (tInvul < durationInvul)
        {
            blink++;
            if(blink > blinkFrame)
            {
                blink = 0;
                playerRenderer.enabled = !playerRenderer.enabled;
            }
            tInvul += Time.deltaTime;
            yield return null;
        }

        isInvulnerable = false;
        Physics.IgnoreLayerCollision(0, 0, false);
        //playerBody.detectCollisions = true;
        playerRenderer.enabled = true;
    }

    public void StartInvulnerability(float time)
    {
        if(!isInvulnerable)
        {
            durationInvul = time;
            StartCoroutine(Invulnerability());

        }
        else
        {
            durationInvul = time;
            tInvul = 0;
        }

    }


}
