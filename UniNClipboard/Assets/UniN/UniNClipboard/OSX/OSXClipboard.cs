using System;
using UnityEngine;
using System.Runtime.InteropServices;

namespace UniN.UniNClipboard
{
	public class OSXClipboard : MonoBehaviour, IClipboard
	{
		public bool ClipboardAvailable
		{
			get { return true; }
		}

		public string Text
		{
			get { return OSXUniNClipboardGetText(); }
			set { OSXUniNClipboardSetText(value); }
		}
		
		public void CopyImage(Texture2D texture2D)
		{
			var bytes = texture2D.EncodeToPNG();
			Debug.Log($"Length of byte array (Unity): {bytes.Length}");
			OSXUniNClipboardSetPng(bytes, (ulong) bytes.Length);
		}

		public event Action OnClipboardChanged;

		private string _textOnPaused;

		private void OnApplicationPause(bool isPaused)
		{
			if (isPaused)
			{
				this._textOnPaused = Text;
			}
			else
			{
				if (this.Text != this._textOnPaused && this.OnClipboardChanged != null)
					this.OnClipboardChanged.Invoke();

				this._textOnPaused = null;
			}
		}

		[DllImport("UniNClipboard")]
		private static extern string OSXUniNClipboardGetText();

		[DllImport("UniNClipboard")]
		private static extern void OSXUniNClipboardSetText(string text);
		
		[DllImport("UniNClipboard")]
		private static extern void OSXUniNClipboardSetPng(byte[] content, ulong length);
	}
}

