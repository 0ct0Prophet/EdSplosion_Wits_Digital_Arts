using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Audio.ProcessorInstance;
[CreateAssetMenu(fileName = "New Narration", menuName = "Narration")]
public class Narration : ScriptableObject

{
    public NarrationLines RootNarration;
}
