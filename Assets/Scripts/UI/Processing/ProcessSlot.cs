using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProcessSlot : MonoBehaviour
{
    [SerializeField] private BluePrint blueprint;

    [SerializeField] private Button makeBtn;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI displayNameText;
    [SerializeField] private TextMeshProUGUI needItemText;

    public InventoryUI Inventory { get; set; }

    private int[] curIngredientsCount;

    private void Start()
    {
        icon.sprite = blueprint.icon;

        displayNameText.text = blueprint.displayName + " - " + blueprint.description;

        for (int i = 0; i < blueprint.needItemNames.Length; ++i)
            needItemText.text += blueprint.needItemNames[i] + " x " + blueprint.needIngredients[i] + " ";

        makeBtn.interactable = false;

        curIngredientsCount = new int[blueprint.needItemNames.Length];
    }

    private void Update()
    {
        bool isMake = true;

        for (int i = 0; i < curIngredientsCount.Length; ++i)
        {
            if (curIngredientsCount[i] < blueprint.needIngredients[i])
            {
                isMake = false;
                break;
            }
        }

        if (isMake)
            makeBtn.interactable = true;
        else
            makeBtn.interactable = false;
    }

    public void OnClickButton()
    {
        Inventory.ProcessItem(blueprint);
    }

    public void ChangeCount(string name, bool isAdd)
    {
        for (int i = 0; i < blueprint.needItemNames.Length; ++i)
        {
            if (blueprint.needItemNames[i] == name)
            {
                if (isAdd)
                    ++curIngredientsCount[i];
                else
                    --curIngredientsCount[i];
            }   
        }
    }
}