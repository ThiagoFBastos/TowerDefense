using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Info : MonoBehaviour
{
	public static Info instance;
	public Text textInfo;
	public Text gameStatus;
	public float time = 0.0f;
	public float money = 15.0f;
	public bool running = false;
	public GameObject screen;
	public GameObject status;
	public GameObject loja;
	bool completed = false;

	void Awake()
	{
		instance = this;
	}

	void Load()
	{
		loja.SetActive(true);
		status.SetActive(true);
		screen.SetActive(false);
		running = true;
	}

	void Clear()
	{
		loja.SetActive(false);
		status.SetActive(false);
		screen.SetActive(true);
		running = false;
	}

    // Start is called before the first frame update
    void Start()
    {
		Clear();
		gameStatus.text = "Tower Defense";
        textInfo.text = time + " s\n" + money + " $";
    }

	public void SetGameOver()
	{
		if(running == false) return;
		Clear();
		completed = true;
		gameStatus.color = Color.red;
		gameStatus.text = "Game Over";
	}

	public void SetGameWin()
	{
		if(running == false) return;
		Clear();
		completed = true;
		gameStatus.color = Color.green;
		gameStatus.text = "You Win";
	}

    // Update is called once per frame
    void Update()
    {
		if(!running) return;
        time -= Time.deltaTime;
		if(time < 0) {
			SetGameWin();
			return;
		}
		textInfo.text = String.Format("{0:0.00}", time) + " s\n$" + money;
    }

	public void NewGameClick()
	{
		Load();
		if(completed) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void QuitClick()
	{	
		UnityEditor.EditorApplication.isPlaying = false;
	}
}
