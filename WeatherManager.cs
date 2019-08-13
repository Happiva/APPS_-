using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour {
	public enum Weather { NONE, SUNNY, RAIN, SNOW };
	public Weather currentWeather;

	public ParticleSystem rain;
	public ParticleSystem snow;

	[Header("Time Setting")]
	[Header("Day")]

	public float Weather_time;
	public int day;
	private float Origin_time = 10f;

	[Header("LightSetting")] //날씨따라 햇빛강도 다르게//
	public Light sunLight;
	private float defaultLightIntensity;
	public float sunnylightintensity;
	public float rainlightintensity;
	public float snowlightintensity;

	public Color defaultLightColor;
	public Color sunnyColor;
	public Color rainColor;
	public Color snowColor;

	private void Start()
	{
		currentWeather = Weather.SUNNY;
		this.defaultLightColor = this.sunLight.color;
		this.defaultLightIntensity = this.sunLight.intensity;
		day = 1;
	}

	public void ChangeWeather(Weather weatherType)
	{
		if(weatherType != this.currentWeather)
		{
			switch (weatherType)
			{
			case Weather.SUNNY:
				currentWeather = Weather.SUNNY;
				this.snow.Stop ();
				break;
			case Weather.RAIN:
				currentWeather = Weather.RAIN;
				this.rain.Play ();
				break;
			case Weather.SNOW:
				currentWeather = Weather.SNOW;
				this.rain.Stop ();
				this.snow.Play ();
				break;
			}
		}
	}

	private void LerpLightColor (Light light, Color c)
	{
		light.color = Color.Lerp (light.color, c, 0.2f * Time.deltaTime);
	}

	private void LerpSunIntensity(Light light, float intensity)
	{
		light.intensity = Mathf.Lerp (light.intensity, intensity, 0.2f * Time.deltaTime);
	}

	private void Update()
	{
		this.Weather_time -= Time.deltaTime;

		if (this.currentWeather == Weather.SUNNY) {
			LerpSunIntensity (this.sunLight, sunnylightintensity);
			LerpLightColor (this.sunLight, sunnyColor);
			if (this.Weather_time <= 0f) {
				ChangeWeather (Weather.RAIN);
				day++;
				Weather_time = Origin_time;
			}
		}
		if (this.currentWeather == Weather.RAIN) {
			LerpSunIntensity (this.sunLight, rainlightintensity);
			LerpLightColor (this.sunLight, rainColor);
			if (this.Weather_time <= 0f) {
				ChangeWeather (Weather.SNOW);
				day++;
				Weather_time = Origin_time;
			}
		}

		if (this.currentWeather == Weather.SNOW) {
			LerpSunIntensity (this.sunLight, snowlightintensity);
			LerpLightColor (this.sunLight, snowColor);
			if (this.Weather_time <= 0f) {
				ChangeWeather (Weather.SUNNY);
				day++;
				Weather_time = Origin_time;
			}
		}

	}

}
