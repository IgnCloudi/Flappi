using System;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;


public class PipeSpawner : MonoBehaviour
{

    // ----Obj pool Setup----
    [Serializable]
    public class Pool
    {
        public Obstacles Type;
        public GameObject Prefab;
        public int Size; 
    }

    // used to set elements in the inspector
    // Then used in start to fill the final dict.
    public List<Pool> AvailableObjPools;
    public enum Obstacles {PIPE}
    //
    public Dictionary<Obstacles, Queue<GameObject>> FinalObsPool = new Dictionary<Obstacles, Queue<GameObject>>();

    // ----SPAWN PARAMS----
    [Range(1.5f, 10)]
    public float MinSpawnRate = 1.0f;
    [Range(1.5f, 10)]
    public float MaxSpawnRate = 2.0f;

    private float SpawnRate = 2f;
    private float Timer = 0;

    [Range(0, 10f)]
    public float PipeOffsetRange = 2f;
    private float CurPipeOffset = 0;

    public GameManager Manager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var CurPool in AvailableObjPools)
        {
            Queue<GameObject> ObjPool = new Queue<GameObject>();
            
            for(var i = 0; i < CurPool.Size; i++)
            {
                GameObject Obj = Instantiate(CurPool.Prefab);
                IScoreGiver Giver = GetScoreGiver(Obj);
                if (Giver != null)
                {
                    // add manager as subscriber if the obj has a Giver
                    Giver.OnObstacleCrossed += Manager.AddScore;
                }
                Obj.SetActive(false);
                ObjPool.Enqueue(Obj);
            }
            FinalObsPool.Add(CurPool.Type, ObjPool);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Timer < SpawnRate)
        {
            Timer += Time.deltaTime;
        }
        else
        {
            SetNewPipeOffset();
            SpawnFromPool(Obstacles.PIPE, transform.position + new Vector3(0, CurPipeOffset, 0), transform.rotation);
           // Instantiate(PipesCtrl, transform.position + new Vector3(0, CurPipeOffset, 0), transform.rotation);
            Timer = 0;
            SetNewSpawnRate();
        }
    }

    private IScoreGiver GetScoreGiver(GameObject Obj)
    {
        IScoreGiver Giver = Obj.GetComponent<IScoreGiver>();
        if (Giver == null)
        {
            Debug.LogWarning("Obj: " + Obj + " does not extend IScoreGiver, It does ot have any actiom/event");
            return null;
        }
        return Giver;
    }

    GameObject SpawnFromPool(Obstacles ObjType, Vector3 Pos, Quaternion Rot)
    {
        if (!FinalObsPool.ContainsKey(ObjType))
        {
            Debug.LogWarning("Pool w tag: " + ObjType + " Does not exist");
            return null;
        }

        //Take the first instantiated obj
        GameObject Spawning = FinalObsPool[ObjType].Dequeue();
        // place it
        Spawning.SetActive(true);
        Spawning.transform.position = Pos;
        Spawning.transform.rotation = Rot;
        // requew it (as queues follow first in frst out principle, this Obj will be queued at the LAST as the other objs were queued first)
        //This will be the last in the queue now
        FinalObsPool[ObjType].Enqueue(Spawning);
        return Spawning; 
    }


    void SetNewSpawnRate()
    {
        SpawnRate = UnityEngine.Random.Range(MinSpawnRate, MaxSpawnRate);
    }

    void SetNewPipeOffset()
    {
        CurPipeOffset = UnityEngine.Random.Range(-PipeOffsetRange, PipeOffsetRange);
    }
}
