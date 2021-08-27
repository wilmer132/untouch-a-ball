using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
  private static Singleton _instance;
  public int HIGHSCORE = 0;

  void Awake() {
    if (_instance == null) {
      _instance = this;
      DontDestroyOnLoad(this.gameObject);
    } else {
      Destroy(this);
    }
  }
}
