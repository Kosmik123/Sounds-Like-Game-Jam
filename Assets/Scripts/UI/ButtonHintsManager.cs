using Bipolar.UI;
using Bipolar.EventTriggers;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class ButtonHintsManager : MonoBehaviour
{
	[System.Serializable]
	internal struct ButtonTextMapping
	{
		public PointerEnterEventTrigger button;
		public string text;
	}

	[SerializeField]
	private ButtonTextMapping[] buttonHints;
	private readonly Dictionary<PointerEnterEventTrigger, string> hintByButton = new Dictionary<PointerEnterEventTrigger, string>();

	[SerializeField]
	private TextMeshProUGUI hintLabel;

	private void Awake()
	{
		foreach (var mapping in buttonHints)
			hintByButton.Add(mapping.button, mapping.text);
	}

	private void OnEnable()
	{
		foreach (var button in hintByButton.Keys)
			button.OnEventTriggered += RefreshHint;
	}

	private void RefreshHint(PointerEventData data)
	{

	}

	private void RefreshHint(UIButton button, bool highlighted)
	{
		//if (highlighted == false)
		//{
		//	hintLabel.enabled = false;
		//	hintLabel.SetText(string.Empty);
		//}
		//else if (hintByButton.TryGetValue(button, out var hint))
		//{
		//	hintLabel.enabled = true;
		//	hintLabel.SetText(hint);
		//}
	}

	private void OnDisable()
	{
		//foreach (var button in hintByButton.Keys)
		//	button.OnHighlightChanged -= RefreshHint;
	}
}
