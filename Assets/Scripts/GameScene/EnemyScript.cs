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

  void DetectEnemyUserContact() {
    if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
      Vector3 inputPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
      RaycastHit2D hit = Physics2D.Raycast(inputPos, Vector2.zero);
      if (hit != null && hit.collider != null) {
        GamePlayScript.IncreaseEnemyDifficulty();
        GamePlayScript.enemySpawned = false;
        Destroy(gameObject);
      }
    }
  }

  void Update() {
    DetectEnemyUserContact();
    transform.position = Vector3.MoveTowards(transform.position, MainPlayer.transform.position, enemyDistance);
  }
}
