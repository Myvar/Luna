using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IronPython.Hosting;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using System.IO;
using Luna.Core.Internal.Lib;

namespace Luna.Core
{
    public class Engine
    {
        public  Control Host { get; set; }

        private WebClient Wc = new WebClient();

        public string MainCode = "";
        private ScriptEngine pyEngine = null;
        private ScriptRuntime pyRuntime = null;
        private ScriptScope pyScope = null;
     
       

        /// <summary>
        /// This is the main interface to the Core implmntaion
        /// </summary>
        /// <param name="h">The host cotrol  for the web app</param>
        public Engine(Control h)
        {
            Host = h;

            if (pyEngine == null)
            {
                pyEngine = Python.CreateEngine();
                pyScope = pyEngine.CreateScope();
                
                
              }
        }

        /// <summary>
        /// Load an we app
        /// </summary>
        /// <param name="url">The root url for the program</param>
        public void OpenApp(string url)
        {
            //get code form server
            var s = Wc.DownloadString(url + "Main.py");
            MainCode = s;


        }

        private void CompileSourceAndExecute(String code)
        {
            ScriptSource source = pyEngine.CreateScriptSourceFromString
                        (code, SourceCodeKind.Statements);
            CompiledCode compiled = source.Compile();
            // Executes in the scope of Python
            compiled.Execute(pyScope);
        }

      
        public void InvokeMain()
        {
            //add lib
            //varables
            User u = new User();

            //adding
            pyScope.SetVariable("User", u);

            //add main variables
            pyScope.SetVariable("MainForm", Host);

            string baceCode = "import clr\nclr.AddReference('System.Windows.Forms')\nclr.AddReference('System')\n";

            CompileSourceAndExecute(baceCode + MainCode);    
            
        }

    }
}
