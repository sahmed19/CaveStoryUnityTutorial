using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactHazard : MonoBehaviour {

	public int damageOnHit;

	void OnCollisionStay2D(Collision2D col) {
		if(col.gameObject.CompareTag("Player")) {
			Player player = col.gameObject.GetComponentInParent<Player>();

			if(player != null) {
				player.ApplyDamage(damageOnHit);
			}

		}
	}


}
