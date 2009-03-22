﻿// WorkbenchController.cs - MonoWorks Project
//
//  Copyright (C) 2009 Andy Selvig
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA

using System;
using System.Collections.Generic;

using MonoWorks.Workbench;
using MonoWorks.Framework;
using MonoWorks.GuiWpf;
using MonoWorks.GuiWpf.Framework;

namespace MonoWorks.WorkbenchWpf
{
	/// <summary>
	/// WPF version of the workbench controller.
	/// </summary>
	public class WorkbenchControllerWpf : WorkbenchController
	{

		public WorkbenchControllerWpf(MainWindow window)
		{
			this.window = window;
			window.KeyPressed += OnKeyPress;

			ResourceManager.LoadAssembly("MonoWorks.Resources");

			uiManager = new UiManager(this, window);
			uiManager.LoadStream(Framework.ResourceHelper.GetStream("Workbench.ui", "MonoWorks.Resources"));
			SetUiManager(uiManager);
		}

		private MainWindow window;

		private UiManager uiManager;

		public override void Open()
		{
		}

		public override void Save()
		{
		}

		public override void SaveAs()
		{
		}

	}
}
