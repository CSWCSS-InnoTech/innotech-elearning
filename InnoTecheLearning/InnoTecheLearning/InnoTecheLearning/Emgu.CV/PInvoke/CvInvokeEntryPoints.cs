//This file is automatically generated by CMAKE. DO NOT MODIFY.
using System;
using System.Collections.Generic;

namespace Emgu.CV
{
   public partial class CvInvoke
   {
        /// <summary>
        /// The file name of the cvextern library
        /// </summary>
#if UNITY_EDITOR_OSX
      public const string ExternLibrary = "Assets/Emgu.CV/Plugins/emgucv.bundle/Contents/MacOS/libcvextern.dylib";
#elif UNITY_STANDALONE_OSX
      public const string ExternLibrary = "@executable_path/../Plugins/emgucv.bundle/Contents/MacOS/libcvextern.dylib";
#elif (__IOS__ || UNITY_IPHONE) && (!UNITY_EDITOR_WIN)
      public const string ExternLibrary = "__Internal";
#elif (!__IOS__ && !__ANDROID__) && __UNIFIED__
      public const string ExternLibrary = "libcvextern.dylib";
#elif __ANDROID__
        public const string ExternLibrary = "cvextern";
#else
        public const string ExternLibrary = "cvextern";
#endif

        /// <summary>
        /// The file name of the cvextern library
        /// </summary>
        public const string ExternCudaLibrary = ExternLibrary;

      /// <summary>
      /// The file name of the opencv_ffmpeg library
      /// </summary>
      public const string OpencvFFMpegLibrary = "opencv_ffmpeg320_64";

      
	  
      /// <summary>
      /// The List of the opencv modules
      /// </summary>
	  public static List<String> OpenCVModuleList = new List<String>
	  {
#if !(__ANDROID__ || __IOS__ || UNITY_IPHONE || UNITY_ANDROID || NETFX_CORE)
        OpencvFFMpegLibrary,
#endif        
        
        ExternLibrary
      };

	  
   }
}
