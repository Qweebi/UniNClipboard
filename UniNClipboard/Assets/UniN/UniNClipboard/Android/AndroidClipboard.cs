﻿using System;
using UnityEngine;

namespace UniN.UniNClipboard
{
    public class AndroidClipboard : IClipboard
    {
        private static class ClassNames
        {
            // Unity
            public const string UnityPlayer = "com.unity3d.player.UnityPlayer";

            // API Level 11
            public const string ClipboardManager = "android.content.ClipboardManager";
            public const string ClipData = "android.content.ClipData";
        }

        private static class StringConstants
        {
            // API Level 1
            public const string ClipboardService = "clipboard"; // API Level 1
        }

        private static class MethodNames
        {
            // Unity
            public const string SCurrentActivity = "currentActivity";

            // Android API Level 1
            public const string GetSystemService = "getSystemService";

            //Android API Level 11
            public const string SetPrimaryClip = "setPrimaryClip";
            public const string GetPrimaryClip = "getPrimaryClip";
            public const string GetItemCount = "getItemCount";
            public const string GetItemAt = "getItemAt";
            public const string CoerceToText = "coerceToText";
            public const string AddPrimaryClipChangedListener = "addPrimaryClipChangedListener";
            public const string RemovePrimaryClipChangedListener = "removePrimaryClipChangedListener";
            public const string SNewPlainText = "newPlainText";
        }

        class OnPrimaryClipChangedListener : AndroidJavaProxy
        {
            public Action OnClipboardChanged;

            //Android API Level 11
            public OnPrimaryClipChangedListener() : base("android.content.ClipboardManager$OnPrimaryClipChangedListener")
            {
            }

            void onPrimaryClipChanged()
            {
                if (this.OnClipboardChanged != null)
                    this.OnClipboardChanged.Invoke();
            }
        }

        private AndroidJavaObject _currentActivity;
        private AndroidJavaObject CurrentActivity
        {
            get
            {
                if (this._currentActivity == null)
                {
                    var unityPlayerClass = new AndroidJavaClass(ClassNames.UnityPlayer);
                    this._currentActivity = unityPlayerClass.GetStatic<AndroidJavaObject>(MethodNames.SCurrentActivity);
                }
                return this._currentActivity;
            }
        }

        private AndroidJavaObject _clipboardManager;
        private AndroidJavaObject ClipboardManager
        {
            get
            {
                if (this._clipboardManager == null)
                    this._clipboardManager = this.CurrentActivity.Call<AndroidJavaObject>(MethodNames.GetSystemService, StringConstants.ClipboardService);
                return this._clipboardManager;
            }
        }

        private readonly string _label;
        private OnPrimaryClipChangedListener _nativeListener;

        public bool ClipboardAvailable
        {
            get { return true; }
        }

        public string Text
        {
            get { return this.GetText(); }
            set { this.SetText(value); }
        }

        public event Action OnClipboardChanged
        {
            add
            {
                if (this._nativeListener == null)
                    this.SetupClipboardChangedListener();

                this._nativeListener.OnClipboardChanged += value;
            }
            remove
            {
                this._nativeListener.OnClipboardChanged -= value;

                if (this._nativeListener.OnClipboardChanged == null)
                    this.RemoveClipboardChangedListener();
            }
        }

        public AndroidClipboard(string label)
        {
            this._label = label;
        }

        private void SetText(string text)
        {
            var clipDataClass = new AndroidJavaClass(ClassNames.ClipData);
            var clipDataInstance = clipDataClass.CallStatic<AndroidJavaObject>(MethodNames.SNewPlainText, this._label, text);
            this.ClipboardManager.Call(MethodNames.SetPrimaryClip, clipDataInstance);
        }

        private string GetText()
        {
            var clipDataInstance = this.ClipboardManager.Call<AndroidJavaObject>(MethodNames.GetPrimaryClip);
            if (clipDataInstance == null)
                return null;

            var itemCount = clipDataInstance.Call<int>(MethodNames.GetItemCount);
            if (itemCount == 0)
                return null;

            var clipDataItemInstance = clipDataInstance.Call<AndroidJavaObject>(MethodNames.GetItemAt, 0);
            if (clipDataItemInstance == null)
                return null;

            return clipDataItemInstance.Call<string>(MethodNames.CoerceToText, this.CurrentActivity);
        }

        private void SetupClipboardChangedListener()
        {
            this._nativeListener = new OnPrimaryClipChangedListener();
            this.ClipboardManager.Call(MethodNames.AddPrimaryClipChangedListener, this._nativeListener);
        }

        private void RemoveClipboardChangedListener()
        {
            this.ClipboardManager.Call(MethodNames.RemovePrimaryClipChangedListener, this._nativeListener);
            this._nativeListener = null;
        }
    }
}
