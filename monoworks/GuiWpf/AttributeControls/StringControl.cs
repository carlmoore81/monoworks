﻿// StringControl.cs - MonoWorks Project
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

using System.Windows;
using System.Windows.Controls;

using MonoWorks.Model;

namespace MonoWorks.GuiWpf.AttributeControls
{
	/// <summary>
	/// Attribute control for strings.
	/// </summary>
	public class StringControl : AttributeControl
	{
		public StringControl(Entity entity, AttributeMetaData metaData)
			: base(entity, metaData)
		{
			textBox = new TextBox();
			Children.Add(textBox);
			textBox.Text = (string)entity.GetAttribute(metaData.Name);
			textBox.TextChanged += OnTextChanged;
		}

		protected TextBox textBox;


		void OnTextChanged(object sender, TextChangedEventArgs e)
		{
			Entity.SetAttribute(MetaData.Name, textBox.Text);

			RaiseAttributeChanged();
		}

	}
}
