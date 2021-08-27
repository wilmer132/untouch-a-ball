using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerScript : MonoBehaviour {
  [SerializeField]
  GameObject Enemy;

  public bool enemySpawned = false;
  public float enemySpeed = 0.00000001f;
  public int LIVES = 3;
  public int HIGHSCORE = 0;
  
  public void IncreaseEnemyDifficulty() {
    enemySpeed *= 1.1f;
    Debug.Log("Enemy Speed is now " + enemySpeed);
  }

  void PlacePrefabRandomlyAcrossScreen() {
    bool spawnAbove = (Random.value < 0.5f);
    bool spawnLeft = (Random.value < 0.5f);
    int XMaxPosition = 3;
    int YMaxPosition = 4;
    int XRandPos = Random.Range(-XMaxPosition, XMaxPosition);
    int YRandPos = Random.Range(-YMaxPosition, YMaxPosition);

    Vector3 enemyPos;
    if (spawnAbove && spawnLeft) {enemyPos = new Vector3(XRandPos, YMaxPosition, 0);} /*Upper side*/
    else if (spawnAbove && !spawnLeft) {enemyPos = new Vector3(XMaxPosition, YRandPos, 0);} /*Right side*/
    else if (!spawnAbove && spawnLeft) {enemyPos = new Vector3(XRandPos, -YMaxPosition, 0);} /*Lower side*/
    else {enemyPos = new Vector3(-XMaxPosition, YRandPos, 0);} /*Left corner*/
    Instantiate(Enemy, enemyPos, Quaternion.identity);
  }
  void Awake() {
    if (!Enemy) {
      Debug.Log("GameObject to load is empty. Quitting...");
      Application.Quit();
    }
  }

  private void OnCollisionEnter2D(Collision2D collision) {
    if (LIVES > 0) {
      LIVES--;
      Debug.Log("Player touched by enemy! Current Lives: " + LIVES);
      Destroy(collision.gameObject);
      enemySpawned = false;
    } else {
      Debug.Log("Game Over. Quitting...");
      Application.Quit();
    }
  }

  IEnumerator WaitAndSpawn(float waitTime) {
    enemySpawned = true;
    yield return new WaitForSeconds(waitTime);
    PlacePrefabRandomlyAcrossScreen();
  }

  void Start() {
    StartCoroutine(WaitAndSpawn(0.0f));
  }

  void Update() {
    if (!enemySpawned) StartCoroutine(WaitAndSpawn(3.0f));
  }
}
