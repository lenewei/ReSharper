using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JetBrains.Metadata.Reader.API;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ProjectModel.Model2.Assemblies.Interfaces;
using JetBrains.ReSharper.Feature.Services.CSharp.Analyses.Bulbs;
using JetBrains.ReSharper.Features.Navigation.Features.FindDependentCode;

namespace AssemblyDependency
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
