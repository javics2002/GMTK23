using System.Collections;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Button : MonoBehaviour
{
    Material material;
	bool clicked = false;
	public UnityEvent callback;

	private void Start() {
		material = GetComponent<MeshRenderer>().material;
	}

	private void OnMouseEnter() {
		material.SetInt("_Selected", 1);
	}

	private void OnMouseExit() {
		material.SetInt("_Selected", 0);
	}

	private void OnMouseDown() {
		if (clicked)
			return;

		clicked = true;
		StartCoroutine(Click());
	}

	IEnumerator Click() {
		float i = 0;
		float speed = 3;

		float j = 1;
        while (i > -1)
        {
			i -= speed * Time.deltaTime;
			material.SetFloat("_Break", i * i + 2 *i);

			if (i < -.75) {
				j -= speed * Time.deltaTime * 4;
				material.SetFloat("_Alpha", j);
			}

			yield return null;
		}

		callback.Invoke();
	}

	public void Play() {
		SceneManager.LoadScene("Spacee");
	}

	public void LeaderBoard() {

	}

	public void Quit() {
		Application.Quit();
	}
}
