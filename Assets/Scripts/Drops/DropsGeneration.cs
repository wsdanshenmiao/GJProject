using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DropsGeneration : Singleton<DropsGeneration>
{
    [SerializeField] private BaseDrops[] m_DropsPrefabs;

    public Dictionary<string, DropsPool> DropsPools = new Dictionary<string, DropsPool>();

    private void Start()
    {
        // 初始化不同掉落物的对象池
        foreach(BaseDrops drops in m_DropsPrefabs){
            GameObject poolHolder = new GameObject($"Pool:{drops.name}");
            poolHolder.transform.SetParent(transform);

            DropsPool pool = poolHolder.AddComponent<DropsPool>();
            pool.SetPrefab(drops);

            DropsPools.Add(drops.DropsData.DropsID, pool);
        }
    }

    public void SpawnDrops(string dropsID, uint dropCount, Vector3 pos)
    {
        DropsPool pool;
        if(DropsPools.TryGetValue(dropsID, out pool)){
            BaseDrops drops = pool.Get();
            drops.DropCount = dropCount;
            drops.transform.position = pos;
        }
    }
}
