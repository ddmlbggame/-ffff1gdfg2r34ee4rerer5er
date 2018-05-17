using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class localizationText : Text {
	[HideInInspector]
	public string[] localization_languages;
	[HideInInspector]
	public LocalizationLanguage[] lanuages;
}
