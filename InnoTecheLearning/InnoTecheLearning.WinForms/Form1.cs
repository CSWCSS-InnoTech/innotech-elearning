﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.JScript;
using Microsoft.JScript.Vsa;
using Convert = Microsoft.JScript.Convert;
using System.IO;

namespace InnoTecheLearning.WinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Input.Text = @"var Ans : double = double(Prev);
function Abs (n) {return Math.abs(n); }
function Acos(n : double) : double { return Math.acos(n); }
function Asin (n : double) : double { return Math.asin(n); }
function Atan (n : double) : double { return Math.atan(n); }
function Atan2 (y : double, x : double) : double{ return Math.atan2(y, x); }
function Ceil(x : double) : double { return Math.ceil(x); }
function Cos(x : double) : double { return Math.cos(x); }
function Exp(x : double) : double { return Math.exp(x); }
function Floor(x : double) : double { return Math.floor(x); }
function Log(x : double) : double { return Math.log(x); }
function Pow(x : double, y : double) : double { return Math.pow(x,y); }
function Random() : double { return Math.random(); }
function Round(x : double) : double { return Math.round(x); }
function Sin(x : double) : double { return Math.sin(x); }
function Sqrt(x : double) : double { return Math.sqrt(x); }
function Tan(x : double) : double { return Math.tan(x); }
function Factorial_(aNumber : int, recursNumber : int ) : double {
   // recursNumber keeps track of the number of iterations so far.
   if (aNumber < 3) {  // If the number is 0, its factorial is 1.
      if (aNumber == 0) return 1.;
      return double(aNumber);
   } else {
      if(recursNumber > 170) {
         return Infinity;
      } else {  // Otherwise, recurse again.
         return (aNumber* Factorial_(aNumber - 1, recursNumber + 1));
      }
}
}

function Factorial(aNumber : int) : double {
   // Use type annotation to only accept numbers coercible to integers.
   // double is used for the return type to allow very large numbers to be returned.
   if(aNumber< 0) {
      throw(""Cannot take the factorial of a negative number."");
   } else {  // Call the recursive function.
      return  Factorial_(aNumber, 0);
   }
}
const π = Math.PI;
const e = Math.E;
const Root2 = Math.SQRT2;
const Root0_5 = Math.SQRT1_2;
const Ln2 = Math.LN2;
const Ln10 = Math.LN10;
const Log2e = Math.LOG2E;
const Log10e = Math.LOG10E;
"""";
";
        }

        private void Evaluate_Click(object sender, EventArgs e)
        {
            Output.Text = JSEvaluate(Input.Text);
        }
        private static string JSEvaluteAns = "";
        public static string JSEvaluate(string Expression, bool TrueFree = false, bool MaxMin = true)
        {
            string Prefix = "var Prev : String = \"" + JSEvaluteAns.Replace(@"\", @"\\").Replace("\"", @"\""") + @""";
";
            // Ask user to enter expression.
            JSEvaluator Evaluator = new JSEvaluator();
            try
            {
                return JSEvaluteAns = Evaluator.Eval(TrueFree ? Expression : Prefix + (MaxMin ?
                    System.Text.RegularExpressions.Regex.
                    Replace(Expression, @"(?<=^|[^\w.])M(in|ax)(?=\s*\()", "Math.m$1")
                    : Expression));
            }
            catch (Exception ex)
            {
                return 'ⓧ' + ex.Message; //⮾
            }
        }
        public class JSEvaluator : INeedEngine
        {

            // Methods
            public JSEvaluator() { init(); }
            private void init() { }
            [JSFunction(JSFunctionAttributeEnum.HasStackFrame)]
            public virtual string Eval(string expr)
            {
                string str = default(string);
                JSLocalField[] fields = new JSLocalField[] { new JSLocalField("expr", typeof(string).TypeHandle, 0), new JSLocalField("return value", typeof(string).TypeHandle, 1) };
                StackFrame.PushStackFrameForMethod(this, fields, ((INeedEngine)this).GetEngine());
                try
                {
                    ((StackFrame)GetEngine().ScriptObjectStackTop()).localVars[0] = expr;
                    ((StackFrame)GetEngine().ScriptObjectStackTop()).localVars[1] = str;
                    expr = Convert.ToString(((StackFrame)GetEngine().ScriptObjectStackTop()).localVars[0], true);
                    str = Convert.ToString(((StackFrame)GetEngine().ScriptObjectStackTop()).localVars[1], true);
                    str = Convert.ToString(Microsoft.JScript.Eval.JScriptEvaluate(expr, GetEngine()), true);
                    ((StackFrame)GetEngine().ScriptObjectStackTop()).localVars[0] = expr;
                    ((StackFrame)GetEngine().ScriptObjectStackTop()).localVars[1] = str;
                }
                finally
                {
                    ((INeedEngine)this).GetEngine().PopScriptObject();
                }
                return str;
            }
#pragma warning disable 0618
            public VsaEngine GetEngine()
            {
                if (vsaEngine == null)
                {
                    vsaEngine = VsaEngine.CreateEngineWithType(typeof(JSEvaluator).TypeHandle);
                }
                return vsaEngine;
            }
            public void SetEngine(VsaEngine engine1)
            {
                vsaEngine = engine1;
            }
            private VsaEngine vsaEngine { get; set; }
#pragma warning restore 0618
        }

        public static string[] Login(ushort StudentID = 18999, string PassPhrase = "Y1234567")
        {
            return HttpRequest(Post, "http://cloud.pedosa.org/platform/solutions/cswcss-innotech/test/index.php",
             "STUDENT_ID=s" + StudentID.ToString() + "&STUDENT_PASSPHRASE=" + PassPhrase).Split(',');
        }
        public const string Get = "GET";
        public const string Post = "POST";
        public static string HttpRequest(string Method, string URI, string Parameters)
        //, string ProxyString
        {
            System.Net.WebRequest req = System.Net.WebRequest.Create(URI);
            //req.Proxy = new System.Net.WebProxy(ProxyString, true);
            //Add these, as we're doing a POST
            req.ContentType = "application/x-www-form-urlencoded";
            req.Method = Method;
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(req.GetRequestStream()))
            //We need to count how many bytes we're sending. Post'ed Faked Forms should be name=value&
            {
                //req.ContentLength = sw.Encoding.GetBytes(Parameters).Length;
                sw.Write(Parameters); //Push it out there
                sw.Flush();
            }
            System.Net.WebResponse resp = req.GetResponse();
            if (resp == null) return null;
            using (System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream()))
                return sr.ReadToEnd().Trim();
        }
        public static byte[] Resample(byte[] samples, int fromSampleRate, int toSampleRate, int quality = 10)
        {
            int srcLength = samples.Length;
            var destLength = (long)samples.Length * toSampleRate / fromSampleRate;
            byte[] _samples = new byte[destLength];
            var dx = srcLength / destLength;

            // fmax : nyqist half of destination sampleRate
            // fmax / fsr = 0.5;
            var fmaxDivSR = 0.5;
            var r_g = 2 * fmaxDivSR;

            // Quality is half the window width
            var wndWidth2 = quality;
            var wndWidth = quality * 2;

            var x = 0;
            int i, j;
            double r_y;
            int tau;
            double r_w;
            double r_a;
            double r_snc;
            for (i = 0; i < destLength; ++i)
            {
                r_y = 0.0;
                for (tau = -wndWidth2; tau < wndWidth2; ++tau)
                {
                    // input sample index
                    j = x + tau;

                    // Hann Window. Scale and calculate sinc
                    r_w = 0.5 - 0.5 * Math.Cos(2 * Math.PI * (0.5 + (j - x) / wndWidth));
                    r_a = 2 * Math.PI * (j - x) * fmaxDivSR;
                    r_snc = 1.0;
                    if (r_a != 0)
                        r_snc = Math.Sin(r_a) / r_a;

                    if ((j >= 0) && (j < srcLength))
                    {
                        r_y += r_g * r_w * r_snc * samples[j];
                    }
                }
                _samples[i] = (byte)r_y;
                x += (int)dx;
            }

            return _samples;
        }
        private void Request_Click(object sender, EventArgs e)
        {
            Output.Text = string.Join(",", Login());
        }

        private void Resampler_Click(object sender, EventArgs e)
        {
            using (var S = new FileStream(
                @"C:\Users\user\Source\Repos\InnoTech-eLearning\InnoTecheLearning\InnoTecheLearning\InnoTecheLearning\Sounds\CelloCC.wav"
, FileMode.Open)) { 
            S.Seek(24, SeekOrigin.Begin);
            byte[] val = new byte[4];
            S.Read(val, 0, 4);
            var From = BitConverter.ToInt32(val, 0);
            string s = "";
            foreach (byte b in Resample(new BinaryReader(S).ReadBytes((int)S.Length), From, From))
                s += b.ToString() + ",";
                Input.Text = s;
        }
        }

        private void Hmmm_Click(object sender, EventArgs e)
        {
            Output.Text = System.Runtime.Serialization.FormatterServices.GetUninitializedObject(typeof(void)).ToString();
        }
    }
}
