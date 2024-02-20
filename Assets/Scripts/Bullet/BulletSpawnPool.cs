using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnPool : MonoBehaviour
{
    public static BulletSpawnPool instance;
    [SerializeField] List<GameObject> bulletPrefabs;
    public List<Queue<GameObject>> availableObjects = new List<Queue<GameObject>>();

    private void Awake()
    {
        instance = this;
    }

    private void GrowPool(GameObject bullet)
    {
        var InstanceToAdd = Instantiate(bullet);
        InstanceToAdd.transform.SetParent(transform);
        AddToPool(InstanceToAdd);
    }

    public void AddToPool(GameObject instanceToAdd)
    {
        int index = GetBulletIndex(instanceToAdd);
        if (index == -1) return;
        instanceToAdd.SetActive(false);
        availableObjects[index].Enqueue(instanceToAdd);
    }
    public bool CheckIfBulletInPool(GameObject bullet)
    {
        return bulletPrefabs.Contains(bullet);
    }

    public void AddBulletPrefabToPool(GameObject bullet)
    {
        if (CheckIfBulletInPool(bullet)) return;
        bulletPrefabs.Add(bullet);
        int index = GetBulletIndex(bullet);
        if (availableObjects.Count <= index)
        {
            availableObjects.Add(new Queue<GameObject>());
        }
    }
    public GameObject GetFromPool(GameObject bullet)
    {
        int index = GetBulletIndex(bullet);
        if (availableObjects[index].Count == 0)
        {
            GrowPool(bullet);
        }
        var instance = availableObjects[index].Dequeue();
        instance.SetActive(true);
        return instance;
    }

    public int GetBulletIndex(GameObject orb)
    {
        for (int i = 0; i < bulletPrefabs.Count; i++)
        {
            if (bulletPrefabs[i].name == orb.name.Replace("(Clone)", "").Trim()) return i;
        }
        return -1;
    }
}
