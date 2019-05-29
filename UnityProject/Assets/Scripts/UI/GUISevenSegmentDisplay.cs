using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUISevenSegmentDisplay : MonoBehaviour
{
	public Sprite num_0;
	public Sprite num_1;
	public Sprite num_2;
	public Sprite num_3;
	public Sprite num_4;
	public Sprite num_5;
	public Sprite num_6;
	public Sprite num_7;
	public Sprite num_8;
	public Sprite num_9;

	public Image displayImage;

	int num = 0;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void SetNumber(int n)
	{
		num = n;
		UpdateSprite();
	}

	private void UpdateSprite()
	{
		switch(num)
		{
			case 0:
				displayImage.sprite = num_0;
				break;
			case 1:
				displayImage.sprite = num_1;
				break;
			case 2:
				displayImage.sprite = num_2;
				break;
			case 3:
				displayImage.sprite = num_3;
				break;
			case 4:
				displayImage.sprite = num_4;
				break;
			case 5:
				displayImage.sprite = num_5;
				break;
			case 6:
				displayImage.sprite = num_6;
				break;
			case 7:
				displayImage.sprite = num_7;
				break;
			case 8:
				displayImage.sprite = num_8;
				break;
			case 9:
				displayImage.sprite = num_9;
				break;
		}
	}
}
