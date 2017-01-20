﻿using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ModuleDependence
{
   public partial class DependencyResult : Form
   {
      public DependencyResult(IList<string> outputList )
      {
         InitializeComponent();
         RtbAssembly.Text = outputList.FirstOrDefault();
         RtbDependent.Text = outputList.LastOrDefault();
      }
   }
}
