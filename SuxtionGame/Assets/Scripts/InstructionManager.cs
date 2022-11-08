using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InstructionManager : MonoBehaviour
{
    public TextMeshProUGUI title, textBlock;
    public Button controlsTab;
    public string controlsText;

    private void Start()
    {
        controlsTab.Select();
        title.text = "Controls";
        textBlock.text = controlsText;
    }

    public void InstructionName(string instructionName)
    {
        title.text = instructionName;
    }
    public void InstructionText(string instructionText)
    {
        textBlock.text = instructionText;
    }
}
