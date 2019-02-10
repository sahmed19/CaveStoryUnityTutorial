using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetBehaviour : MonoBehaviour {

	public float distance = 4;

	private Player player;

	void Start() {
		player = Player.instance;
	}

	void Update() {

		Vector3 localPos = new Vector3(

		player.IsFacingRight()? 1f : -1f,
		General.Direction2Vector(player.GetDirection()).y,
		0f
		);

		transform.localPosition = localPos * distance;

	}

}
