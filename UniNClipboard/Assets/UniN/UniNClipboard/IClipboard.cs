using System;
using UnityEngine;

namespace UniN.UniNClipboard
{
	public interface IClipboard
	{
		bool ClipboardAvailable { get; }
		string Text { get; set; }
		void CopyImage(Texture2D texture2D);

		event Action OnClipboardChanged;
	}
}
