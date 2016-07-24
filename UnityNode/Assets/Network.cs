using UnityEngine;
using System.Collections.Generic;
using SocketIO;

public class Network : MonoBehaviour {
  static SocketIOComponent socket;

  public GameObject playerPrefab;

  Dictionary<string, GameObject> players;

  void Start() {
    socket = GetComponent<SocketIOComponent>();
    socket.On("open", OnConnected);
    socket.On("spawn", OnSpawned);
    socket.On("move", OnMove);
    socket.On("registered", OnRegistered);

    players = new Dictionary<string, GameObject> ();
  }
  void OnConnected(SocketIOEvent e) {
    Debug.Log("Connected");
  }
  void OnSpawned (SocketIOEvent e) {
    Debug.Log("spawned");
    var player = Instantiate(playerPrefab);

    players.Add(e.data["id"].ToString(), player);
    Debug.Log("count: " + players.Count);
  }
  void OnMove (SocketIOEvent e) {
    Debug.Log("player is moving " + e.data);

    var id = e.data ["id"];
    Debug.Log (id);
  }
  void OnRegistered (SocketIOEvent e) {
    Debug.Log("registered id: " + e.data);
  }
}
