// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace RadarIO.Xamarin.iOS.Sample
{
	[Register("ViewController")]
	partial class ViewController
	{
		[Outlet]
		UIKit.UIButton TrackOnce { get; set; }

		void ReleaseDesignerOutlets()
		{
			if (TrackOnce != null)
			{
				TrackOnce.Dispose();
				TrackOnce = null;
			}
		}
	}
}
