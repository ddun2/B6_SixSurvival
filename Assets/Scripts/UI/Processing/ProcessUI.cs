using System.Diagnostics;
using UnityEngine;

public class ProcessUI : MonoBehaviour
{
    [SerializeField] private GameObject processWindow;
    [SerializeField] private Transform slotPanel;
    [SerializeField] private InventoryUI inventory;

    private PlayerController controller;

    private ProcessSlot[] slots;

    private void Start()
    {
        controller = CharacterManager.Instance.Player.controller;

        controller.process += Toggle;

        processWindow.SetActive(false);

        slots = new ProcessSlot[slotPanel.childCount];
        for (int i = 0; i < slots.Length; ++i)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<ProcessSlot>();
            slots[i].Inventory = inventory;
        }
    }

    public void Toggle()
    {
        if (IsOpen())
            processWindow.SetActive(false);
        else
            processWindow.SetActive(true);
    }

    private bool IsOpen()
    {
        return processWindow.activeInHierarchy;
    }

    public void AddItem(string name)
    {
        for (int i = 0; i < slots.Length; ++i)
        {
            slots[i].ChangeCount(name, true);
        }
    }

    public void RemoveItem(string name)
    {
        for (int i = 0; i < slots.Length; ++i)
        {
            slots[i].ChangeCount(name, false);
        }
    }
}