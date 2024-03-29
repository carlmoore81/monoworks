// ContextLayer.cs - MonoWorks Project
//
//  Copyright (C) 2009 Andy Selvig
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
using System.Collections.Generic;

using MonoWorks.Base;
using MonoWorks.Rendering.Events;
using MonoWorks.Rendering;

namespace MonoWorks.Controls
{
	
	/// <summary>
	/// Exception for invalid contexts.
	/// </summary>
	public class InvalidContextException : Exception
	{
		public InvalidContextException(string context) : 
			base(context + " is not a valid context in this context layer.")
		{
		}
	}

	
	/// <summary>
	/// A control layer that contains anchors on all edges of the scene.
	/// It also contains a collection of toolbars that can be dynamically added
	/// and removed to the anchors by use of string keywords called contexts.
	/// </summary>	
	public class ContextLayer : Overlay
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public ContextLayer() : base()
		{
			// create the anchors
			foreach (AnchorLocation anchorLoc in Enum.GetValues(typeof(AnchorLocation)))
			{
				anchors[anchorLoc] = new AnchorPane(anchorLoc);
			}

			// create the stacks
			foreach (Side loc in Enum.GetValues(typeof(Side)))
			{
				Stack stack = new Stack();
				stack.Orientation = ContextOrientation(loc);
				stack.Padding = 0;
				stacks[loc] = stack;
				anchors[(AnchorLocation)loc].Control = stack;
			}
		}


#region The Anchors and Stacks

		/// <summary>
		/// Gets the orientation for the context in the given position.
		/// </summary>
		/// <param name="loc"></param>
		/// <returns></returns>
		public static Orientation ContextOrientation(Side loc)
		{
			if (loc == Side.N || loc == Side.S)
				return Orientation.Horizontal;
			else
				return Orientation.Vertical;
		}

		Dictionary<AnchorLocation, AnchorPane> anchors = new Dictionary<AnchorLocation, AnchorPane>();

		Dictionary<Side, Stack> stacks = new Dictionary<Side, Stack>();


		/// <summary>
		/// Anchors a control at the given location.
		/// </summary>
		public void AnchorControl(Renderable2D control, AnchorLocation location)
		{
			if (anchors[location].Control != null)
				throw new Exception("There's already something at this anchor.");

			anchors[location].Control = control;
		}
		
#endregion


#region The Contexts

		/// <summary>
		/// Adds the given control to the location.
		/// </summary>
		public void AddContext(Side loc, Renderable2D control)
		{
			//			ToolBar toolbar = GetToolbar(context);
		//			toolbar.Orientation = ContextOrientation(loc);
			stacks[loc].AddChild(control);
			anchors[(AnchorLocation)loc].MakeDirty();			
		}

		/// <summary>
		/// Clears all contexts from the location.
		/// </summary>
		/// <param name="loc"></param>
		public void ClearContexts(Side loc)
		{
			stacks[loc].Clear();
		}

		/// <summary>
		/// Clears all contexts from all locations.
		/// </summary>
		public void ClearAllContexts()
		{
			foreach (Side loc in Enum.GetValues(typeof(Side)))
			{
				stacks[loc].Clear();
			}
		}


#endregion



#region Rendering

		
		public override void OnSceneResized(Scene scene)
		{
			base.OnSceneResized(scene);
			
			foreach (AnchorPane anchor in anchors.Values)
				anchor.OnSceneResized(scene);
		}

		public override void RenderOverlay (Scene scene)
		{
			base.RenderOverlay (scene);
			
			foreach (var anchor in anchors.Values)
			{
				anchor.RenderOverlay(scene);
			}
		}

		
		
#endregion
		
		
#region Interaction		
		
		protected override bool HitTest (Coord pos)
		{
			return false;
		}

		public override void OnButtonPress (MouseButtonEvent evt)
		{
			base.OnButtonPress (evt);
			
			foreach (var anchor in anchors.Values)
				anchor.OnButtonPress(evt);
		}

		public override void OnButtonRelease (MouseButtonEvent evt)
		{
			base.OnButtonRelease (evt);
			
			foreach (var anchor in anchors.Values)
				anchor.OnButtonRelease(evt);
		}

		public override void OnMouseMotion (MouseEvent evt)
		{
			base.OnMouseMotion (evt);
			
			foreach (var anchor in anchors.Values)
				anchor.OnMouseMotion(evt);
		}

		
#endregion
		
		
	}
}
