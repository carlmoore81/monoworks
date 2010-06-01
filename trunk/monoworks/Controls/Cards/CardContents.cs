// 
//  Card.cs - MonoWorks Project
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

using MonoWorks.Base;
using MonoWorks.Rendering;
using MonoWorks.Controls;

namespace MonoWorks.Controls.Cards
{
	/// <summary>
	/// Stack that contains the contents of a card.
	/// </summary>
	public class CardContents : Stack
	{

		public CardContents() : base(Orientation.Vertical)
		{
		}
		
		public override void ComputeGeometry()
		{
			base.ComputeGeometry();
		}

		
		protected override void Render(RenderContext rc)
		{
			rc.Push();
			rc.Cairo.Color = new Cairo.Color(0, 1, 1);
			rc.Cairo.Rectangle(0, 0, RenderWidth, RenderHeight);
			rc.Cairo.Fill();
			rc.Cairo.Color = new Cairo.Color(0, 0, 0);
			rc.Cairo.LineWidth = 4;
			rc.Cairo.Rectangle(0, 0, RenderWidth, RenderHeight);
			rc.Cairo.Stroke();
			rc.Pop();
			base.Render(rc);
		}
		
	}
}