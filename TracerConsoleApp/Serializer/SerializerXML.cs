using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using Tracer;
using TracerConsoleApp.Serializer;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.Linq;
using static Tracer.TraceResult.Thread;
using System.Threading;

namespace TracerConsoleApp
{
    namespace Serializer
    {
        public class SerializerXML : ISerializer
        {

            public string Serialize(TraceResult traceResult)
            {
                TraceResult.Thread[] threadsInfo = traceResult.threads;
                XDocument xDocument = new XDocument(new XElement("root"));

                foreach (TraceResult.Thread thread in threadsInfo)
                {
                    XElement threadXElement = GetThreadXElement(thread);
                    xDocument.Root.Add(threadXElement);
                }

                StringWriter stringWriter = new StringWriter();
                using (XmlTextWriter xmlWriter = new XmlTextWriter(stringWriter))
                {
                    xmlWriter.Formatting = System.Xml.Formatting.Indented;
                    xDocument.WriteTo(xmlWriter);
                }
                return stringWriter.ToString();
            }

            private XElement GetMethodXElement(Method methodInfo)
            {
                return new XElement(
                    "method",
                    new XAttribute("name", methodInfo.Name),
                    new XAttribute("class", methodInfo.Class),
                    new XAttribute("time", methodInfo.Time)
                    );
            }

            private XElement GetMethodXElementWithChildMethods(Method methodInfo)
            {
                XElement methodXElement = GetMethodXElement(methodInfo);
                foreach (Method method in methodInfo.Methods)
                {
                    XElement childMethod = GetMethodXElement(method);
                    if (method.Methods.Length > 0)
                    {
                        childMethod = GetMethodXElementWithChildMethods(method);
                    }
                    methodXElement.Add(childMethod);
                }
                return methodXElement;
            }

            private XElement GetThreadXElement(TraceResult.Thread threadInfo)
            {
                XElement threadXElement = new XElement(
                    "thread",
                    new XAttribute("id", threadInfo.Id),
                    new XAttribute("time", threadInfo.Time)
                    );
                foreach (Method method in threadInfo.Methods)
                {
                    XElement methodXElement = GetMethodXElementWithChildMethods(method);
                    threadXElement.Add(methodXElement);
                }
                return threadXElement;
            }

        }
    }
}

