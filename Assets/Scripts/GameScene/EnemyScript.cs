using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
  GameObject MainPlayer;
  MainPlayerScript GamePlayScript;

  private bool followMainPlayer = false;
  private float enemyDistance;

  void Start() {
    MainPlayer = GameObject.Find("MainPlayer");
    if (!MainPlayer) {
      Debug.Log("Enemy could not identify Main Player. Quitting...");
      Application.Quit();
    }
    GamePlayScript = MainPlayer.GetComponent<MainPlayerScript>();
    enemyDistance = GamePlayScript.enemySpeed * Time.deltaTime;
  }

  void Update() {
    transform.position = Vector3.MoveTowards(transform.position, MainPlayer.transform.position, enemyDistance);
  }
}
