using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Objects;

public class GUI_GasContainer : NetTab
{
	private GasContainer gasContainer;

	public Image background;
	public Image stripe;

	public Image redLight;
	public Image yellowLight;
	public Image greenLight;

	public Color redOnColor;
	public Color redOffColor;
	public Color yellowOnColor;
	public Color yellowOffColor;
	public Color greenOnColor;
	public Color greenOffColor;

	public GUISevenSegmentDisplayController pressureReading;
	public GUISevenSegmentDisplayController temperatureReading;

	float maxRot = 315;
	float minRot = 45;

	public GameObject button;
	public Text buttonText;

	public GameObject upButton;
	public GameObject downButton;

	public Color openColor;
	public Color closeColor;

	public float blinkDelay = .5f;
	float blinkCountdown = 0;

	bool blinkOn; // If we are on the 'high' part of the blink

	private enum LightState
	{
		blink,
		on,
		off
	}

	LightState redLightState = LightState.blink;
	LightState yellowLightState = LightState.off;
	LightState greenLightState = LightState.on;

	// Start is called before the first frame update
	void Start()
	{
		gasContainer = Provider.GetComponent<GasContainer>();
		initializeTabDisplay();
	}

	// Update is called once per frame
	void Update()
	{
		UpdateTabDisplay();
	}

	void initializeTabDisplay()
	{
		if (!IsServer)
		{
			background.color = gasContainer.tabBackgroundColor;
			stripe.color = gasContainer.tabStripeColor;
		}
	}

	void UpdateTabDisplay()
	{
		if (!IsServer)
		{
			HandleSevenSegDisplays();

			button.GetComponent<Image>().color = !gasContainer.Opened ? openColor : closeColor;
			buttonText.text = gasContainer.Opened ? "CLOSE" : "OPEN";

			// I releaize this isn't the best way to go about this, but I feel that this saved me some code.
			switch (redLightState)
			{
				case LightState.on:
					redLight.color = redOnColor;
					break;
				case LightState.off:
					redLight.color = redOffColor;
					break;
				case LightState.blink:
					break;
			}

			switch (yellowLightState)
			{
				case LightState.on:
					yellowLight.color = yellowOnColor;
					break;
				case LightState.off:
					yellowLight.color = yellowOffColor;
					break;
				case LightState.blink:
					break;
			}

			switch (greenLightState)
			{
				case LightState.on:
					greenLight.color = greenOnColor;
					break;
				case LightState.off:
					greenLight.color = greenOffColor;
					break;
				case LightState.blink:
					break;
			}

			HandleBlink();
		}
	}

	void HandleSevenSegDisplays()
	{
		pressureReading.SetNumber(gasContainer.GasMix.Pressure);
		temperatureReading.SetNumber(gasContainer.GasMix.Temperature);
	}

	void HandleBlink()
	{
		int blinkers = 0;
		if (redLightState == LightState.blink)
			blinkers += 1;

		if (yellowLightState == LightState.blink)
			blinkers += 2;

		if (greenLightState == LightState.blink)
			blinkers += 4;

		if (blinkers != 0)
		{
			if (blinkCountdown <= 0)
			{
				if (blinkOn)
					SetLights(blinkers, false);
				else
					SetLights(blinkers, true);

				blinkOn = !blinkOn;

				blinkCountdown = blinkDelay;
			}
			else
			{
				blinkCountdown -= Time.deltaTime;
			}
		}
	}

	void SetLights(int blinkers, bool state)
	{
		if ((blinkers & 1) != 0)
		{
			redLight.color = state ? redOnColor : redOffColor;
		}

		if ((blinkers & 2) != 0)
		{
			yellowLight.color = state ? yellowOnColor : yellowOffColor;
		}

		if ((blinkers & 4) != 0)
		{
			greenLight.color = state ? greenOnColor : greenOffColor;
		}
	}

	public void OutputPressureUp()
	{
		gasContainer.ReleasePressure += 10;
	}

	public void OutputPressureDown()
	{
		if (gasContainer.ReleasePressure < 10)
		{
			gasContainer.ReleasePressure = 0;
		}
		else
		{
			gasContainer.ReleasePressure -= 10;
		}
	}

	public void ToggleOpen()
	{
		gasContainer.Opened = !gasContainer.Opened;
	}

	public void SubmitOutPutPressure(float press)
	{
		gasContainer.ReleasePressure = press;
	}
}
