using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public Image timeBar;
    public Text timeText;
    public float restartDelay = 1f;
    public float dayStartDelay = 2f;
    public static GameManager instance = null;

    public GameObject dayImage;
    public Text dayText;
	private bool isSetDay; //하루가 지났나 안지났나 판정하는 함수

	private int GameDay = 1; //지금이 몇일째
	private float dayTime = 0f; // 하루가 얼마나 경과했는가
	private float day = 10f; //하루는 10초

	void Start () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		isSetDay = true;
		dayPass ();
		timeText.text = dayTime + "/" + day;
	}

	void Update () {
		if (dayTime < day && !isSetDay) //하루가 안 지났을 때
        {
			dayTime += Time.deltaTime;
			timeText.text = dayTime + "/" + day;
            
        } else if (dayTime >= day && !isSetDay)  //하루가 다갔고, 아직 Setday불리언이 false일때
        {
			Debug.Log ("Day is passed!");
			GameDay++;
			isSetDay = true; //true로 바꿈
            dayPass();
            Invoke ("Restart", restartDelay);
		}
	}

	void dayPass(){
		dayText.text = "Day " + GameDay;
		dayImage.SetActive (true);
		Invoke ("HideDayImage", dayStartDelay);
	}

	private void HideDayImage(){
		dayImage.SetActive (false);
		isSetDay = false;
		Time.timeScale = 1f;
	}

    void setTime() {
        timeBar.fillAmount = dayTime / day;
        if (dayTime > day) dayTime = day;
    }
    
	private void Restart(){
        Debug.Log("restart 함수 들어옴");
        Invoke("HideDayImage", dayStartDelay);
        dayTime = 0f;
        Debug.Log ("Restarted!");
	} 
}
