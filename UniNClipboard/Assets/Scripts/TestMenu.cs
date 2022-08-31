using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TestMenu : MonoBehaviour
{
	[SerializeField] private Text _contentLabel;

	private int listenerId = 1;
	private readonly List<Action> registeredListeners = new List<Action>();

	public void OnCopyButtonPressed()
	{
		StartCoroutine(TakeScreenShoot());
	}
	
	IEnumerator TakeScreenShoot()
	{
		yield return new WaitForEndOfFrame();
		int width = UnityEngine.Screen.width;
		int heigh = UnityEngine.Screen.height;
		var screenshotTexture = new Texture2D(width, heigh, TextureFormat.ARGB32, false);
		Rect rect = new Rect(0, 0, width, heigh);
		screenshotTexture.ReadPixels(rect, 0 ,0);
		screenshotTexture.Apply();
		
		var png = screenshotTexture.EncodeToPNG();
		var path = $"{Application.persistentDataPath}/screen.png";
		File.WriteAllBytes(path, png);

		Clipboard.SharedClipboard.CopyImage(screenshotTexture);
		Debug.Log($"Copied to clipboard: {path}");
	}


	public void OnDisplayContentPressed()
	{
		this._contentLabel.text = Clipboard.SharedClipboard.Text;
	}

	public void OnAddListenerButtonPressed()
	{
		var assignedListenerId = this.listenerId;
		this.listenerId += 1;

		Action listenerToAdd = () =>
		{
			var localListenerId = assignedListenerId;
			Debug.Log(string.Format("[{0}]Clipboard changed: {1}", localListenerId, Clipboard.SharedClipboard.Text));
			this._contentLabel.text = string.Format("[{0}]Clipboard changed: {1}", localListenerId,
				Clipboard.SharedClipboard.Text);
		};
		Clipboard.SharedClipboard.OnClipboardChanged += listenerToAdd;
		this.registeredListeners.Add(listenerToAdd);
	}

	public void OnRemoveListenerButtonPressed()
	{
		if (this.registeredListeners.Count == 0)
			return;

		var listenerToRemove = this.registeredListeners[this.registeredListeners.Count - 1];
		Clipboard.SharedClipboard.OnClipboardChanged -= listenerToRemove;
		this.registeredListeners.Remove(listenerToRemove);
	}
}
