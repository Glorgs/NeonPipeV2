using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class Painting : MonoBehaviour
{
    public float timeBetweenSpawn = 0.1f;
    public GameObject decalPrefab;
    public Transform decalSpawnTransform;
    public string spraySFX;
    public string scoringSFX;

    [SerializeField] private InGamePlayerUI playerUI;

    [SerializeField] private float timeBetweenSpawnDecrease = 0.001f;
    [SerializeField] private float timeBetweenSpawnMin = 0.0001f;
    private float timeNoFeedback = 0f;
    private string[] feedbackWord = new string[3] { "GREAT", "WOW", "AWESOME" };
    private Color[] firstColor = new Color[3] { new Color(6f / 255, 1.0f, 0.0f), new Color(0f / 255, 52f / 255, 1.0f), new Color(255f / 255, 0.0f, 115f / 255) };
    private Color[] secondColor = new Color[3] { new Color(0f / 255, 255 / 255f, 231f / 255), new Color(190f / 255, 0.0f, 1.0f), new Color(255f / 255, 24f / 255, 0.0f) };

    private int score = 0;

    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
        }
    }

    private float t = 0f;
    private GameObject peinture;

    private PlayerController playerController;

    private void Start() {
        playerController = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        timeNoFeedback += Time.deltaTime;

        if(t > timeBetweenSpawn && playerController.isPainting)
        {
            peinture = Instantiate(decalPrefab, decalSpawnTransform.position, Quaternion.AngleAxis(90,Vector3.right));
            DecalProjector m_DecalProjectorComponent = peinture.GetComponent<DecalProjector>();

            // Creates a new material instance for the DecalProjector.
            m_DecalProjectorComponent.material = new Material(m_DecalProjectorComponent.material);
            m_DecalProjectorComponent.material.SetColor("_EmissiveColor", VariableGlobale.Si().colorsPlayer[GetComponent<PlayerController>().NumeroPlayer]);

            peinture.transform.localEulerAngles = new Vector3(90 - transform.eulerAngles.z , 90, 0);

            if(spraySFX != null)
            {
                Debug.Log("spray");
                AudioManager.Si().PlaySFX(spraySFX, AudioManager.Si().gameObject);
            }
            CheckIfInTag();

            t = 0;
        }

        UpdatePaintingRate();
    }

    public void AddScore(int scoreModifier) {
        score += scoreModifier;
        VariableGlobale.Si().GlobalScore += scoreModifier;
        playerUI.UpdateScoreText(VariableGlobale.Si().GlobalScore);
    }

    public int GetScore() {
        return score;
    }

    private void UpdatePaintingRate() {
        timeBetweenSpawn = Mathf.Clamp(timeBetweenSpawn - timeBetweenSpawnDecrease * Time.deltaTime, timeBetweenSpawnMin, 1);
    }

    void CheckIfInTag()
    {
        Vector3 projectionPeinture = peinture.transform.position;// + peinture.transform.forward * 7.5f;
        foreach(GameObject obj in PipeManager.Si().chunksPipe)
        {
            foreach (GameObject tag in obj.GetComponent<Pipe>().listTag)
            {
                Transform t = tag.transform;

                Vector3 newProjectPeinture = projectionPeinture - t.position;
                float up = Vector3.Dot(newProjectPeinture, t.up);
                float forward = Vector3.Dot(newProjectPeinture, t.forward);
                float right = Vector3.Dot(newProjectPeinture, t.right);

                if (Mathf.Abs(up) < t.GetComponent<DecalProjector>().size.y/2f && Mathf.Abs(right) < t.GetComponent<DecalProjector>().size.x / 2f && forward > 0)
                {
                    AddScore(10);

                    TagManager.Si().tags[GetComponent<PlayerController>().NumeroPlayer] = tag;
                    TagManager.Si().peintures[GetComponent<PlayerController>().NumeroPlayer] = peinture;
                    Feedback(projectionPeinture);


                    if (scoringSFX != null)
                    {
                        AudioManager.Si().PlaySFX(scoringSFX, AudioManager.Si().gameObject);
                    }

                    break;
                }

            }
        }
    }

    private void Feedback(Vector3 pos)
    {
        if(timeNoFeedback > 0.7f)
        {
            int i = Random.Range(0, feedbackWord.Length);
            ShowText.Si().ShowDamageNumberColor(feedbackWord[i], pos, firstColor[i]);
            timeNoFeedback = 0f;
        }


    }

}
