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
using System.Xml;
using System.Xml.Linq;
using System.Reflection;


namespace Luna.Core
{
    public class Engine
    {
        public Control Host { get; set; }

        private WebClient Wc = new WebClient();

        public string MainCode = "";
        public string MainHtml = "";

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
            var sh = Wc.DownloadString(url + "Main.html");
            MainCode = s;
            MainHtml = sh;

        }

        private void CompileSourceAndExecute(String code)
        {
            ScriptSource source = pyEngine.CreateScriptSourceFromString
                        (code, SourceCodeKind.Statements);
            CompiledCode compiled = source.Compile();
            // Executes in the scope of Python
            compiled.Execute(pyScope);
        }

        private void LoadHTML()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(MainHtml);
            foreach (XmlNode i in doc.DocumentElement.ChildNodes)
            {
                switch (i.Name)
                {
                    case "Button":                        
                        if (i.Attributes["Name"] != null)
                        {
                            Button b = new Button();
                          //  b.Name = i.Attributes["Name"].Value;
                            foreach(var d in i.Attributes)
                            {
                                var s = (XmlAttribute)d;
                                PropertyInfo prop = b.GetType().GetProperty(s.Name, BindingFlags.Public | BindingFlags.Instance);
                                if (null != prop && prop.CanWrite)
                                {
                                    try
                                    {
                                        prop.SetValue(b, Convert.ChangeType(s.Value, prop.PropertyType), null);
                                    }
                                    catch(Exception e)
                                    {
                                        if(s.Name == "Location")
                                        {
                                            string[] s1 = s.Value.Split(',');
                                            b.Location = new System.Drawing.Point(int.Parse(s1[0]),int.Parse( s1[1]));
                                        }
                                    }
                                }
                            }


                            pyScope.SetVariable(i.Attributes["Name"].Value, b);
                            Host.Controls.Add(b);
                        }
                        break;
                    case "TextBox":
                        if (i.Attributes["Name"] != null)
                        {
                            TextBox b = new TextBox();
                            //  b.Name = i.Attributes["Name"].Value;
                            foreach (var d in i.Attributes)
                            {
                                var s = (XmlAttribute)d;
                                PropertyInfo prop = b.GetType().GetProperty(s.Name, BindingFlags.Public | BindingFlags.Instance);
                                if (null != prop && prop.CanWrite)
                                {
                                    try
                                    {
                                        prop.SetValue(b, Convert.ChangeType(s.Value, prop.PropertyType), null);
                                    }
                                    catch (Exception e)
                                    {
                                        if (s.Name == "Location")
                                        {
                                            string[] s1 = s.Value.Split(',');
                                            b.Location = new System.Drawing.Point(int.Parse(s1[0]), int.Parse(s1[1]));
                                        }
                                    }
                                }
                            }


                            pyScope.SetVariable(i.Attributes["Name"].Value, b);
                            Host.Controls.Add(b);
                        }
                        break;

                }
            }

        }


      
        /// <summary>
        /// start Site
        /// </summary>
        public void InvokeMain()
        {
            LoadHTML();
            //add lib
            //varables
            User u = new User();

            //adding
            pyScope.SetVariable("User", u);

            //add main variables
            pyScope.SetVariable("MainForm", Host);

            string baceCode = "import clr\nclr.AddReference('System.Windows.Forms')\nclr.AddReference('System.Drawing')\nclr.AddReference('System')\n";

            CompileSourceAndExecute(baceCode + MainCode);

        }

    }
}
