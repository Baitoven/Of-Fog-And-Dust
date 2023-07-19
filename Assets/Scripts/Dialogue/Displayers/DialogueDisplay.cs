using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OfFogAndDust.Dialogue.Displayers
{
    internal class DialogueDisplay : MonoBehaviour
    {
        [SerializeField] private Image background;
        [SerializeField] private Image speakerImage;
        [SerializeField] private TextMeshProUGUI speakerName;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private List<DialogueAnswer> answers;
    }
}
