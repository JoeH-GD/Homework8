using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;

namespace Homework8
{
    class Program
    {
        static void Main(string[] args)
        {
            Type dateTime = typeof(DateTime);
            PropertyInfo [] properties = dateTime.GetProperties();

            
            for (int i=0; i<properties.Length; i ++)
            {
                Console.WriteLine(properties[i].ToString());
            }
            

            Console.ReadLine();
        }

       
    }
}
