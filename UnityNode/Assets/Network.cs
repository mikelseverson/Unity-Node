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
    Debug.Log("spawned: " + e.data);
    var player = Instantiate(playerPrefab);

    players.Add(e.data["id"].ToString(), player);
    Debug.Log("count: " + players.Count);
  }
  void OnMove (SocketIOEvent e) {
    var id = e.data ["id"].ToString();
    var player = players [id];
    var x = GetFloatFromJson(e.data, "x");
    var y = GetFloatFromJson(e.data, "y");
    var pos = new Vector3 (x, 0, y);
    var navigatePos = player.GetComponent<NavigatePosition> ();

    Debug.Log("player is moving: " + player.name);

    navigatePos.NavigateTo (pos);
  }
  void OnRegistered (SocketIOEvent e) {
    Debug.Log("registered id: " + e.data);
  }
  float GetFloatFromJson (JSONObject data, string key) {
    return float.Parse(data [key].ToString().Replace("\"", ""));
  }

}
