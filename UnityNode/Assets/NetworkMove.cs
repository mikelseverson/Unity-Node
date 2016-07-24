using UnityEngine;
using System.Collections;
using SocketIO;
public class NetworkMove : MonoBehaviour {

	public SocketIOComponent socket;

	public void OnMove (Vector3 position) {
		socket.Emit("move", new JSONObject(VectorToJson(position)));
	}

	string VectorToJson (Vector3 vector) {
		return string.Format (@"{{""x"":""{0}"", ""y"":""{1}""}}", vector.x, vector.y);
	}
}
