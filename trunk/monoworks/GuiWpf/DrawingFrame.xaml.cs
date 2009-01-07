﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

//using System.Windows.Forms;
using System.Windows.Forms.Integration;

using MonoWorks.Model;
using MonoWorks.Model.Viewport;
using MonoWorks.Model.Interaction;
using MonoWorks.Rendering;

namespace MonoWorks.GuiWpf
{
	/// <summary>
	/// Interaction logic for DrawingFrame.xaml
	/// </summary>
	public partial class DrawingFrame : UserControl
	{
		public DrawingFrame()
		{
			InitializeComponent();

			Controller = new Controller(Viewport);

		}

		/// <summary>
		/// The viewport.
		/// </summary>
		public IViewport Viewport
		{
			get { return viewportWrapper.Viewport; }
		}

		/// <summary>
		/// The viewport controller.
		/// </summary>
		public Controller Controller { get; private set; }

		protected Drawing drawing = null;
		/// <summary>
		/// The drawing being displayed by the frame.
		/// </summary>
		public Drawing Drawing
		{
			get { return drawing; }
			set
			{
				if (drawing != null)
					Viewport.RenderList.RemoveRenderable(drawing);
				drawing = value;
				Viewport.RenderList.AddRenderable(drawing);

				// add the drawing interactor
				DrawingInteractor interactor = new DrawingInteractor(Viewport, drawing);
				Viewport.PrimaryInteractor = interactor;
			}
		}


	}
}