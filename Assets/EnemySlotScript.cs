using UnityEngine;
using System.Collections;

public class EnemySlotScript : MonoBehaviour
{
    int slotCount = 0;
    public int maxSlots = 5;
    ArrayList arrayList;
    // Use this for initialization
    void Start()
    {
        arrayList = new ArrayList();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public bool SlotAvaible()
    {
        if (slotCount < maxSlots)
            return true;
        return false;
    }
    public void InsertIntoSlot(GameObject _enemy)
    {
        arrayList.Add(_enemy);
        slotCount++;
    }
    public void RemoveSlot(GameObject _enemy)
    {
        arrayList.Remove(_enemy);
        slotCount--;
    }
}
