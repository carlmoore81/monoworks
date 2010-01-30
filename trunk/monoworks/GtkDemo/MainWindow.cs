// MainWindow.cs - MonoWorks Project
//
//  Copyright (C) 2008 Andy Selvig
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA 

using System;

using MonoWorks.Rendering;
using MonoWorks.GtkBackend;
using MonoWorks.GtkBackend.Framework;
using MonoWorks.Controls;
using MonoWorks.Demo;

namespace MonoWorks.GtkDemo
{
	
	/// <summary>
	/// Main window for the Gtk port of the plotting demo.
	/// </summary>
	public class MainWindow : Gtk.Window
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public MainWindow() : base(Gtk.WindowType.Toplevel)
		{
			Title = "MonoWorks Demo";
			
			DeleteEvent += OnDeleteEvent;
			
			// initialize the Gtk Resource Manager
			ResourceManager.Initialize();
			
			// create the viewport adapter
			var adapter = new ViewportAdapter();
			Add(adapter);
			
			// create the scene space
			var sceneSpace = new SceneSpace(adapter.Viewport);
			adapter.Viewport.RootScene = sceneSpace;
			var book = new SceneBook(adapter.Viewport);
			sceneSpace.Root = book;
			
			// create the controls scene
			var controls = new ControlsScene(adapter.Viewport);
			book.Add(controls);
			
			// create the 2D plotting scene
			var plot2d = new Plot2dScene(adapter.Viewport);
			book.Add(plot2d);
			
			// create the 3D plotting scene
			var plot3d = new Plot3dScene(adapter.Viewport);
			book.Add(plot3d);
			
			ShowAll();
		}
				
		protected void OnDeleteEvent(object sender, Gtk.DeleteEventArgs args)
		{
			args.RetVal = true;
			Gtk.Application.Quit();
		}
		
	}
}