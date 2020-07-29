using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlerOrbit : MonoBehaviour {

	void Start () {
		StartCoroutine (GoTo ());
	}

	void Update () { }

	public IEnumerator GoTo () {
		float progress = 0;
		Vector3 startingPos = transform.position;
		Vector3 pos = transform.position;
		Vector3 target = Vector3.one * 5 - Random.insideUnitSphere * 15f;
		while (true) {
			progress += Time.deltaTime / 1;
			transform.transform.position = Vector3.Lerp (pos, target, progress);
			if (progress >= 1) {
				progress = 0;
				pos = transform.position;
				target = Vector3.one * 5 - Random.insideUnitSphere * 15f;
			}
			yield return new WaitForEndOfFrame ();
		}
	}
}