using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction {
	UP,
	DOWN,
	LEFT,
	RIGHT,
	NONE
}

public class General {

	public static Vector2 Direction2Vector(Direction d) {

		switch(d) {
			case Direction.UP:
			return Vector2.up;
			case Direction.DOWN:
			return Vector2.down;
			case Direction.LEFT:
			return Vector2.left;
			case Direction.RIGHT:
			return Vector2.right;
			case Direction.NONE:
			default:
			return Vector2.zero;
		}

	}


}
