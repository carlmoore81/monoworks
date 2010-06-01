// 
//  CardInteractor.cs - MonoWorks Project
//  
//  Author:
//       Andy Selvig <ajselvig@gmail.com>
// 
//  Copyright (c) 2010 Andy Selvig
// 
//  This library is free software; you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as
//  published by the Free Software Foundation; either version 2.1 of the
//  License, or (at your option) any later version.
// 
//  This library is distributed in the hope that it will be useful, but
//  WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
//  Lesser General Public License for more details.
// 
//  You should have received a copy of the GNU Lesser General Public
//  License along with this library; if not, write to the Free Software
//  Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA

using System;

using MonoWorks.Rendering;
using MonoWorks.Rendering.Interaction;
using MonoWorks.Rendering.Events;
using MonoWorks.Base;

namespace MonoWorks.Controls.Cards
{

	/// <summary>
	/// Interactor for cards.
	/// </summary>
	public class CardInteractor : AbstractInteractor
	{

		public CardInteractor(Scene scene) : base(scene)
		{
			_mouseType = InteractionType.None;
		}
		
		
		/// <summary>
		/// The card book that this interactor focuses on.
		/// </summary>
		public CardBook CardBook { get; set; }
		
		/// <summary>
		/// The card whos children we are currently viewing.
		/// </summary>
		public Card CurrentRoot { get; set; }
		
		
		/// <summary>
		/// The factor between screen coordinates and card coordinates.
		/// </summary>
		public double Zoom { get; set; }
		
		private InteractionType _mouseType;
		
		public override void OnButtonPress(MouseButtonEvent evt)
		{
			base.OnButtonPress(evt);
			
			evt.Handle(this);
			
			switch (evt.Button) {
			case 1:
				_mouseType = InteractionType.Pan;
				break;
			}
		}
		
		public override void OnButtonRelease(MouseButtonEvent evt)
		{
			base.OnButtonRelease(evt);
			
			// snap to the nearest grid location
			if (_mouseType == InteractionType.Pan && CardBook != null)
			{
				AnimateToNearest(evt.Scene.Camera);
			}
			
			_mouseType = InteractionType.None;
			
			evt.Handle(this);
		}


		public override void OnMouseMotion(MouseEvent evt)
		{
			
			if (_mouseType == InteractionType.Pan)
			{
				evt.Scene.Camera.Pan(evt.Pos - lastPos);
			}
			
			evt.Handle(this);
			
			base.OnMouseMotion(evt);
		}
		
		public override void OnMouseWheel(MouseWheelEvent evt)
		{
			base.OnMouseWheel(evt);
			
			if (evt.Direction == WheelDirection.Up)
				// zoom in
				AnimateToZoom(evt.Scene.Camera, Zoom * 2);
			else
				// zoom out
				AnimateToZoom(evt.Scene.Camera, Zoom / 2.0);
			evt.Handle(this);
		}


		
		public override void OnSceneResized(Scene scene)
		{
			base.OnSceneResized(scene);
			
			MoveToZoom(scene.Camera, Zoom);
		}



		private bool _isInitialized = false;
		
		public override void RenderOverlay(Scene scene)
		{
			base.RenderOverlay(scene);
			
			if (!_isInitialized) {
				_isInitialized = true;
				InitCamera(scene.Camera);
			}
		}


			
		#region Camera Motion
		
		/// <summary>
		/// Initializes the camera for interacting with cards.
		/// </summary>
		public void InitCamera(Camera camera)
		{
			MoveToZoom(camera, 1);
			MoveToNearest(camera);
			camera.UpVector = new Vector(0, 1, 0);
		}
				
		/// <summary>
		/// Instantly moves the camera to the nearest card.
		/// </summary>
		public void MoveToNearest(Camera camera)
		{
			// get the nearest grid point
			if (CurrentRoot == null)
				CurrentRoot = CardBook;
			var coord = new Coord(camera.Position.X, camera.Position.Y);
			CurrentRoot.RoundToNearestGrid(coord);
			
			// move the camera
			camera.Center.X = coord.X;
			camera.Center.Y = coord.Y;
			camera.Position.X = coord.X;
			camera.Position.Y = coord.Y;
		}

		/// <summary>
		/// Animates the camera to the nearest card.
		/// </summary>
		public void AnimateToNearest(Camera camera)
		{
			// get the nearest grid point
			if (CurrentRoot == null)
				CurrentRoot = CardBook;
			var coord = new Coord(camera.Position.X, camera.Position.Y);
			CurrentRoot.RoundToNearestGrid(coord);
			
			// create the animation
			var center = camera.Center.Copy();
			center.X = coord.X;
			center.Y = coord.Y;
			var position = camera.Position.Copy();
			position.X = coord.X;
			position.Y = coord.Y;
			camera.AnimateTo(center, position, camera.UpVector);
		}
		
		/// <summary>
		/// Instantly moves the camera to the given zoom level.
		/// </summary>
		public void MoveToZoom(Camera camera, double zoom)
		{
			Zoom = zoom;
			
			// determine how far back the camera needs to be for the zoom level
			var offset = camera.ViewportHeight / (camera.FoV / 2.0).Tan() / zoom / 2;
			
			// get the z position of the current level
			if (CurrentRoot == null)
				CurrentRoot = CardBook;
			var z = CurrentRoot.Origin.Z - CardBook.LayerDepth;
			
			// move the camera
			camera.Position.Z = z + offset;
			camera.Center = camera.Position.Copy();
			camera.Center.Z = z;
			
			camera.Configure();
		}

		/// <summary>
		/// Animates the camera to the given zoom level.
		/// </summary>
		public void AnimateToZoom(Camera camera, double zoom)
		{
			if (Zoom == zoom)
				return;
			Zoom = zoom;
			
			// determine how far back the camera needs to be for the zoom level
			var offset = camera.ViewportHeight / (camera.FoV / 2.0).Tan() / zoom / 2;
			
			// get the z position of the current level
			if (CurrentRoot == null)
				CurrentRoot = CardBook;
			var z = CurrentRoot.Origin.Z - CardBook.LayerDepth;
			
			// create the animation
			var position = camera.Position.Copy();
			position.Z = z + offset;
			var center = position.Copy();
			center.Z = z;
			camera.AnimateTo(center, position, camera.UpVector);
		}
		
		#endregion
		
		
	}
}