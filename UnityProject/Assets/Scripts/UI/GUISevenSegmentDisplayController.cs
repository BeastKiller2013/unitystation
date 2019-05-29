using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUISevenSegmentDisplayController : MonoBehaviour
{
	public List<GUISevenSegmentDisplay> displays;

	public GUISevenSegmentDisplay tenthsDisplay;

	int num = 0;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void SetNumber(float num)
	{
		string number = num.ToString();
		int len = number.Length;
		int decimalPostition = number.IndexOf('.');
		// Pad zeroes where needed
		while (decimalPostition < displays.Count)
		{
			number = "0" + number;
			decimalPostition += 1;
		}

		for(int i = 0; i < displays.Count; i++)
		{
			displays[i].SetNumber(
				int.Parse(
					number.Substring(
						decimalPostition - (displays.Count - i), 1
						)));
		}

		if(tenthsDisplay)
			tenthsDisplay.SetNumber(int.Parse(number.Substring(decimalPostition + 1, 1)));
	}
}
