//
// OsmExplorer: a C# application and class 
// library for exploring OpenStreetMap data
// Copyright (C) 2012, Ryan Conrad
//
// Based on a project by Ciaran Gultnieks: http://projects.ciarang.com/p/csmapcontrol
// Powered by the Volante embedded database engine by Krzysztof Kowalczyk: http://blog.kowalczyk.info/software/volante/database.html
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU Affero General Public License for more details.
//
// You should have received a copy of the GNU Affero General Public License
// along with this program. If not, see <http://www.gnu.org/licenses/>. 

namespace OsmExplorer.Components
{
	partial class MapExplorer
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.Timer1Tick);
            // 
            // MapExplorer
            // 
            this.DoubleBuffered = true;
            this.Name = "MapExplorer";
            this.Size = new System.Drawing.Size(447, 332);
            this.Load += new System.EventHandler(this.MapExplorer_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MapExplorer_MouseDown);
            this.MouseEnter += new System.EventHandler(this.MapExplorer_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.MapExplorer_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MapExplorer_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MapExplorer_MouseUp);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.MapExplorer_MouseWheel);
            this.ResumeLayout(false);

		}
        private System.Windows.Forms.Timer timer1;

	}
}
