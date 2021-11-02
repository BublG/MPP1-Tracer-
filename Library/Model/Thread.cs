using System;
using System.Collections.Generic;

namespace Library.Model
{
    [Serializable]
    public class Thread
    {
        public Thread()
        {
        }

        public Thread(int id)
        {
            Id = id;
            Methods = new List<Method>();
        }

        public int Id { get; set; }
        public long Time { get; set; }
        public List<Method> Methods { get; set; }
    }
}