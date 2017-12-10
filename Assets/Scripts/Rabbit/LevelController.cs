using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    public static LevelController current=null;

	public GameObject settingsPrefab;
	public MyButton pauseButton;

	public AudioClip music = null;
	AudioSource musicSource = null;

    void Awake()
    {
        current = this;
    }

	void Start(){
		pauseButton.signalOnClick.AddListener (this.showSettings);

		musicSource = gameObject.AddComponent<AudioSource>();
		musicSource.clip = music;
		musicSource.loop = true;
		musicSource.Play ();
	}

    Vector3 startingPosition;

    public void setStartPosition(Vector3 pos)
    {
        startingPosition = pos;
    }

    public void onRabbitDeath(HeroRabbit rabbit)
    {
        //При смерті кролика повертаємо на початкову позицію
        rabbit.isDead = true;
        StartCoroutine(later(rabbit));
    }


    IEnumerator later(HeroRabbit rabbit) {
        yield return new WaitForSeconds(2);

        rabbit.transform.position = this.startingPosition;
        rabbit.isDead = false;
        rabbit.reset();

    }

	void showSettings() {
		//Знайти батьківський елемент
		GameObject parent = UICamera.first.transform.parent.gameObject;
		//Створити Prefab
		GameObject obj = NGUITools.AddChild (parent, settingsPrefab);
		//Отримати доступ до компоненту (щоб передати параметри)
		SettingsPopUp popup = obj.GetComponent<SettingsPopUp>();


	}
		
	/*void showWinScore() {
		//Знайти батьківський елемент
		GameObject parent = UICamera.first.transform.parent.gameObject;
		//Створити Prefab
		GameObject obj = NGUITools.AddChild (parent, winPrefab);
		//Отримати доступ до компоненту (щоб передати параметри)
		WinPopUp popup = obj.GetComponent<SettingsPopUp>();

	}

	void showLoseScore() {
		//Знайти батьківський елемент
		GameObject parent = UICamera.first.transform.parent.gameObject;
		//Створити Prefab
		GameObject obj = NGUITools.AddChild (parent, losePrefab);
		//Отримати доступ до компоненту (щоб передати параметри)
		LosePopUp popup = obj.GetComponent<SettingsPopUp>();


	}*/

    }
