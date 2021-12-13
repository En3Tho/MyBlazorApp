using System;
using System.Runtime.InteropServices;
using Foundation;
using MyBlazorApp.BlazorClient.Maui.Services;
using ObjCRuntime;

namespace MyBlazorApp.BlazorClient.Maui.MacCatalyst
{
	[Preserve]
	public class TrayService : ITrayService
	{
		public TrayService() : base()
		{
			_serviceNative = new TrayServiceNative();
			_serviceNative.ClickHandler = () => ClickHandler?.Invoke();
		}

		private readonly TrayServiceNative _serviceNative;

		public void Initialize()
			=> _serviceNative.Initialize();

		public Action ClickHandler { get; set; }
	}

	[Preserve]
	public class TrayServiceNative : NSObject
	{
		[DllImport("/usr/lib/libobjc.dylib", EntryPoint = "objc_msgSend")]
		public static extern IntPtr IntPtr_objc_msgSend_nfloat(IntPtr receiver, IntPtr selector, nfloat arg1);

		[DllImport("/usr/lib/libobjc.dylib", EntryPoint = "objc_msgSend")]
		public static extern IntPtr IntPtr_objc_msgSend_IntPtr(IntPtr receiver, IntPtr selector, IntPtr arg1);

		[DllImport("/usr/lib/libobjc.dylib", EntryPoint = "objc_msgSend")]
		public static extern IntPtr IntPtr_objc_msgSend(IntPtr receiver, IntPtr selector);

		[DllImport("/usr/lib/libobjc.dylib", EntryPoint = "objc_msgSend")]
		public static extern void void_objc_msgSend_IntPtr(IntPtr receiver, IntPtr selector, IntPtr arg1);

		[DllImport("/usr/lib/libobjc.dylib", EntryPoint = "objc_msgSend")]
		public static extern void void_objc_msgSend_bool(IntPtr receiver, IntPtr selector, bool arg1);

		private NSObject _systemStatusBarObj;
		private NSObject _statusBarObj;
		private NSObject _statusBarItem;
		private NSObject _statusBarButton;
		private NSObject _statusBarImage;

		public Action ClickHandler { get; set; }

		public void Initialize()
		{
			_statusBarObj = Runtime.GetNSObject(Class.GetHandle("NSStatusBar"));
			_systemStatusBarObj = _statusBarObj.PerformSelector(new Selector("systemStatusBar"));
			_statusBarItem = Runtime.GetNSObject(IntPtr_objc_msgSend_nfloat(_systemStatusBarObj.Handle, Selector.GetHandle("statusItemWithLength:"), -1));
			_statusBarButton = Runtime.GetNSObject(IntPtr_objc_msgSend(_statusBarItem.Handle, Selector.GetHandle("button")));
			_statusBarImage = Runtime.GetNSObject(IntPtr_objc_msgSend(ObjCRuntime.Class.GetHandle("NSImage"), Selector.GetHandle("alloc")));

			// var imgPath = System.IO.Path.Combine(NSBundle.MainBundle.BundlePath, "Contents", "trayicon.png");
			var imgPath = System.IO.Path.Combine(NSBundle.MainBundle.BundlePath, "Contents", "Resources", "MacCatalyst", "trayicon.png");
			var imageFileStr = NSString.CreateNative(imgPath);
			var nsImagePtr = IntPtr_objc_msgSend_IntPtr(_statusBarImage.Handle, Selector.GetHandle("initWithContentsOfFile:"), imageFileStr);

			void_objc_msgSend_IntPtr(_statusBarButton.Handle, Selector.GetHandle("setImage:"), _statusBarImage.Handle);
			void_objc_msgSend_bool(nsImagePtr, Selector.GetHandle("setTemplate:"), true);

			// Handle click
			void_objc_msgSend_IntPtr(_statusBarButton.Handle, Selector.GetHandle("setTarget:"), this.Handle);
			void_objc_msgSend_IntPtr(_statusBarButton.Handle, Selector.GetHandle("setAction:"), new Selector ("handleButtonClick:").Handle);
		}

		[Export ("handleButtonClick:")]
		private void HandleClick (NSObject senderStatusBarButton)
		{
			var nsapp = Runtime.GetNSObject(Class.GetHandle("NSApplication"));
			var sharedApp = nsapp.PerformSelector(new Selector("sharedApplication"));

			void_objc_msgSend_bool(sharedApp.Handle, Selector.GetHandle("activateIgnoringOtherApps:"), true);

			ClickHandler?.Invoke();
		}
	}
}
