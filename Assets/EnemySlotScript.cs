using UnityEngine;
using System.Collections;

public class EnemySlotScript : MonoBehaviour
{
    int slotCount = 0;
    public int maxSlots = 5;
    public ArrayList arrayList;
    public Vector3[] outerSlots;
    int nextAvaibleSlot = 0;
    // Use this for initialization
    void Start()
    {
        arrayList = new ArrayList();
        outerSlots = new Vector3[40];
        float degrees = 0;
        for (int i = 0; i < 40; i++, degrees += 360 / 40)
        {
            outerSlots[i].x = Mathf.Cos(degrees * Mathf.Deg2Rad) * 5;
            outerSlots[i].y = 0;
            outerSlots[i].z = Mathf.Sin(degrees * Mathf.Deg2Rad) * 5;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public bool SlotAvaible()
    {
        //if (slotCount < maxSlots)
        //    return true;
        return (slotCount < maxSlots);
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
    public Vector3 GetOuterSlotPosition()
    {
        if (nextAvaibleSlot == 40)
            return Vector3.zero;
        Vector3 temp = outerSlots[nextAvaibleSlot];
        nextAvaibleSlot++;
        return temp;
    }
    public void ResetSlots()
    {
        foreach (GameObject enemy in arrayList)
        {
            if (enemy)
                enemy.SendMessage("ResetSlot");
        }
        arrayList.Clear();
        slotCount = 0;
        nextAvaibleSlot = 0;
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Room")
        {
            ResetSlots();
        }

    }
}
