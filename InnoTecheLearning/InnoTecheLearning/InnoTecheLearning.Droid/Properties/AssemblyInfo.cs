﻿using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Android.App;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle(InnoTecheLearning.Utils.AssemblyTitle)]
[assembly: AssemblyDescription(InnoTecheLearning.Utils.AssemblyDescription)]
[assembly: AssemblyConfiguration(InnoTecheLearning.Utils.AssemblyConfiguration)]
[assembly: AssemblyCompany(InnoTecheLearning.Utils.AssemblyCompany)]
[assembly: AssemblyProduct(InnoTecheLearning.Utils.AssemblyProduct)]
[assembly: AssemblyCopyright(InnoTecheLearning.Utils.AssemblyCopyright)]
[assembly: AssemblyTrademark(InnoTecheLearning.Utils.AssemblyTrademark)]
[assembly: AssemblyCulture(InnoTecheLearning.Utils.AssemblyCulture)]
[assembly: ComVisible(InnoTecheLearning.Utils.ComVisible)]
[assembly: Guid(InnoTecheLearning.Utils.ComGuid)]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion(InnoTecheLearning.Utils.VersionAssembly)]
[assembly: AssemblyFileVersion(InnoTecheLearning.Utils.VersionAssemblyFile)]
[assembly: AssemblyInformationalVersion(InnoTecheLearning.Utils.VersionAssemblyInfo)]

// Add some common permissions, these can be removed if not needed
[assembly: UsesPermission(Android.Manifest.Permission.Internet)]
[assembly: UsesPermission(Android.Manifest.Permission.WriteExternalStorage)]
