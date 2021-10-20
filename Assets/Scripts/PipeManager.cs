using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PipeManager : MySingleton<PipeManager>
{
    public GameObject player;
    public List<GameObject> chunksPipe;
    public GameObject chunkPrefab;
    public int maxNumberChunk = 3;
    public GameObject[] tagPrefab;
    public GameObject[] obstaclePrefab;
    public GameObject[] powerUpPrefab;

    [SerializeField]
    private float distanceParcouru = 0f;
    private float pipeLength = 32f;
    private Vector3 lastPositionPlayer;
    private Vector3 offsetObstacle = new Vector3(0, 0, 4);

    private int numberVerticalSlice = 15;
    private int numberHorizontalSlice = 5;

    private float difficulty = 0f;
    public int[] timeDifficulty = new int[7] { 25, 40, 60, 75, 105, 120, 150};
    public Vector3[] sliceNumbers = new Vector3[7] { new Vector3(1, 1, 5), new Vector3(1, 2, 5), new Vector3(3, 2, 4), new Vector3(3, 3, 4), new Vector3(4, 3, 3), new Vector3(5, 4, 3), new Vector3(5, 5, 2) };

    private int difficultyIndex = 0;

    private int maxPowerUp = 1;

    private void Awake()
    {
        lastPositionPlayer = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        distanceParcouru += Mathf.Abs(lastPositionPlayer.z - player.transform.position.z);
        lastPositionPlayer = player.transform.position;

        if(distanceParcouru >= pipeLength)
        {
            GameObject newChunk = null;
            if (chunksPipe.Count >= maxNumberChunk)
            {
                GameObject chunk = chunksPipe[0];
                List<GameObject> tags = chunk.GetComponent<Pipe>().listTag;
                List< GameObject> obstacles = chunk.GetComponent<Pipe>().obstacle;
                List<GameObject> powerUps = chunk.GetComponent<Pipe>().powerUp;
                
                foreach (GameObject obj in tags)
                {
                    Destroy(obj);
                }

                foreach (GameObject obj in obstacles)
                {
                    Destroy(obj);
                }

                foreach (GameObject obj in powerUps)
                {
                    Destroy(obj);
                }

                tags.Clear();
                obstacles.Clear();
                powerUps.Clear();

                chunksPipe.RemoveAt(0);
                newChunk = chunk;
            }
            else
            {
                newChunk = Instantiate(chunkPrefab, player.transform.position, Quaternion.identity);
            }

            if(chunksPipe.Count > 0)
            {
                AttachPipe(chunksPipe[chunksPipe.Count - 1].GetComponent<Pipe>(), newChunk.GetComponent<Pipe>());
            }
            chunksPipe.Add(newChunk);
            StartCoroutine(SetUpPipe(newChunk.GetComponent<Pipe>()));

            distanceParcouru = 0;
        }

        UpdateDifficulty();
    }
    void UpdateDifficulty()
    {
        difficulty += Time.deltaTime;
        if (difficultyIndex < timeDifficulty.Count() && difficulty > timeDifficulty[difficultyIndex])
        {
            difficultyIndex++;
        }
    }

    //Attache le pipe2 au pipe1
    void AttachPipe(Pipe pipe1, Pipe pipe2)
    {
        pipe2.transform.position = pipe1.endPoint.position;
        pipe2.transform.rotation = pipe1.endPoint.rotation;
    }

    IEnumerator SetUpPipe(Pipe pipe)
    {
        yield return new WaitForSeconds(0.2f);
        
        Vector3 pipePosition = pipe.transform.position;
        float baseOffset = 4;
        Vector3 offset = new Vector3(0,0, baseOffset);
       
        Vector3 dir = Vector3.zero;
        for (int j = 0; j < 3; j++)
        {
            float angle = Random.Range(0, 360);
            for (int i = 0; i < 4; i++)
            {
                int spawn = Random.Range(0, (int)sliceNumbers[difficultyIndex].z);
                if (spawn == 0)
                {
                    dir = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
                    GameObject tag = Instantiate(tagPrefab[(int)Random.Range(0, tagPrefab.Length)], pipePosition + offset + dir * 5f, Quaternion.LookRotation(dir, Vector3.up));
                    tag.transform.SetParent(pipe.transform);
                    pipe.listTag.Add(tag);
                }
                angle += 90;
            }
            offset += new Vector3(0,0,(pipeLength - baseOffset) / 5);
        }

        Debug.DrawLine(pipePosition + offset, pipePosition + offset + dir * 8, Color.red, 10f);


        //tag.transform.localEulerAngles = new Vector3(tag.transform.localEulerAngles.x, 90, 180);

        CreateObstacle(pipePosition + offsetObstacle, pipe);
    }

    void CreateObstacle(Vector3 startPoint, Pipe pipe)
    {
        //int verticalSlice = Random.Range(0, numberVerticalSlice);

        List<int> rangeHorizontal = CreateNumberList(0, numberHorizontalSlice);
        List<int> horizontalDirection = GetNumbersFromList(ref rangeHorizontal, (int)sliceNumbers[difficultyIndex].x);

        int spawnedPowerUp = 0;

        foreach(int horizontalSlice in horizontalDirection)
        {
            List<int> rangeVertical = CreateNumberList(0, numberVerticalSlice);
            List<int> verticalDirection = GetNumbersFromList(ref rangeVertical, (int)sliceNumbers[difficultyIndex].y);

            foreach (int verticalSlice in verticalDirection)
            {
                float angle = (360 / numberVerticalSlice * verticalSlice);
                Vector3 dir = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;

                Debug.DrawLine(startPoint + new Vector3(0,0, (pipeLength/numberHorizontalSlice) * horizontalSlice), startPoint + dir * 8, Color.blue, 10f);

                RaycastHit hit;
                if (Physics.Raycast(startPoint + new Vector3(0, 0, (pipeLength / numberHorizontalSlice) * horizontalSlice), dir, out hit, 100))
                {
                    Debug.Log("hello");
                    //Instantiate(tagPrefab, hit.point, Quaternion.identity);
                }

                GameObject obstacle = Instantiate(obstaclePrefab[(int)Random.Range(0, obstaclePrefab.Length)], startPoint + new Vector3(0, 0, (pipeLength / numberHorizontalSlice) * horizontalSlice) + dir * 8, Quaternion.AngleAxis(angle + 90, Vector3.forward));
                pipe.obstacle.Add(obstacle);

            }

            if (spawnedPowerUp < maxPowerUp && rangeVertical.Count > 0)
            {
                if ((int)Random.Range(0, 7) == 0)
                {
                    float angle = (360 / numberVerticalSlice * GetRandomfromList(ref rangeVertical));
                    Vector3 dir = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;

                    GameObject powerUp = Instantiate(powerUpPrefab[(int)Random.Range(0, powerUpPrefab.Length)], startPoint + new Vector3(0, 0, (pipeLength / numberHorizontalSlice) * horizontalSlice) + dir * 6, Quaternion.AngleAxis(angle + 90, Vector3.forward));
                    pipe.powerUp.Add(powerUp);
                    spawnedPowerUp++;
                }

            }

        }
    }

    private List<int> CreateNumberList(int minInclusive, int maxExclusive)
    {
        /*if (maxExclusive - minInclusive + 1 < numberToChoose)
        {
            Debug.Log("Error : Not Enough Range");
            return null;
        }*/

        List<int> range = new List<int>();
        for (int i = minInclusive; i < maxExclusive; i++)
        {
            range.Add(i);
        }

        return range;
    }

    private List<int> GetNumbersFromList(ref List<int> range, int numberToChoose)
    {
        List<int> randomNumbers = new List<int>();        

        for(int i = 0; i<numberToChoose; i++)
        {
            randomNumbers.Add(GetRandomfromList(ref range));
        }

        return randomNumbers;
    }

    int GetRandomfromList(ref List<int> myList)
    {
        int pos = Random.Range(0, myList.Count);
        int x = myList[pos];
        myList.RemoveAt(pos);

        return x;
    }
}
