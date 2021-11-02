using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Library.Model
{
    [Serializable]
    public class Method
    {
        public Method()
        {
        }

        public Method(string className, string methodName)
        {
            Methods = new List<Method>();
            ClassName = className;
            MethodName = methodName;
            IsCompleted = false;
        }

        [JsonIgnore] [XmlIgnore] public bool IsCompleted { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public long Time { get; set; }
        public List<Method> Methods { get; set; }
    }
}