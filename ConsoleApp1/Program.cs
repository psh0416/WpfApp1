using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    interface a
    {

    }
    class A
    {

    }
    class B : A
    {

    }
    class Program
    {
        static void Main(string[] args)
        {

            new Program();
            Console.ReadLine();
        }

        public Program()
        {
            Test1();
        }

        public void Test2()
        {

        }

        public void Test1()
        {
            // Our anonymous type sequence
            var anonymousEnumerable = Enumerable
                    .Range(0, 10)
                    .Select(i => new { ID = i, Text = i.ToString() });
            var enumerableCount = anonymousEnumerable.Count();
            var anonymousType = anonymousEnumerable.First().GetType();

            // Option #1 - declare it as dynamic, i.e., anything goes
            dynamic[] asDynamicArray = new dynamic[enumerableCount];
            foreach (var tuple in anonymousEnumerable.Select((item, i) => Tuple.Create(i, item)))
            {
                asDynamicArray[tuple.Item1] = tuple.Item2;
            }

            // Let's go the IEnumerable route
            foreach (var asDynamic in asDynamicArray)
            {
                Console.WriteLine("ID:{0} Text:{1}", asDynamic.ID, asDynamic.Text);
            }

            // Lowest common denominator: *everything* is an object
            object[] asObjectArray = new object[enumerableCount];
            foreach (var tuple in anonymousEnumerable.Select((item, i) => Tuple.Create(i, item)))
            {
                asObjectArray[tuple.Item1] = tuple.Item2;
            }

            // Let's iterate with a for loop - BUT, it's now "untyped", so things get nasty
            var idGetterMethod = anonymousType.GetMethod("get_ID");
            var textGetterMethod = anonymousType.GetMethod("get_Text");
            for (int i = 0; i < asObjectArray.Length; i++)
            {
                var asObject = asObjectArray[i];
                var id = (int)idGetterMethod.Invoke(asObject, null);
                var text = (string)textGetterMethod.Invoke(asObject, null);
                Console.WriteLine("ID:{0} Text:{1}", id, text);
            }

            // This is cheating :)
            var letTheCompilerDecide = anonymousEnumerable.ToArray();
            foreach (var item in letTheCompilerDecide)
            {
                Console.WriteLine("ID:{0} Text:{1}", item.ID, item.Text);
            }

            // Use reflection to "make" an array of the anonymous type
            var anonymousArrayType = anonymousType.MakeArrayType();
            var reflectIt = Activator.CreateInstance(
                      anonymousArrayType,
                      enumerableCount) as Array;
            Array.Copy(anonymousEnumerable.ToArray(), reflectIt, enumerableCount);

            // We're kind of in the same boat as the object array here, since we
            // don't really know what the underlying item type is
            for (int i = 0; i < reflectIt.Length; i++)
            {
                var asObject = reflectIt.GetValue(i);
                var id = (int)idGetterMethod.Invoke(asObject, null);
                var text = (string)textGetterMethod.Invoke(asObject, null);
                Console.WriteLine("ID:{0} Text:{1}", id, text);
            }

            List<dynamic> data = new List<dynamic>();

            //
            //Assume lots of processing to build up all these arguments
            //
            data.Add(new { a = 1, b = "test1" });
            data.Add(new { a = 2, b = "test2" });
            data.Add(new { a = 3, b = "test3" });
            data.Add(new { a = 4, b = "test4" });
            data.Add(new { a = 5, b = "test5" });
            foreach (var datum in data)
            {
                Console.WriteLine(String.Format("{0}: {1}", datum.a, datum.b));
            }

            int N = int.Parse(Console.ReadLine()); // Number of elements which make up the association table.
            int Q = int.Parse(Console.ReadLine()); // Number Q of file names to be analyzed.

            string[] fNames = new string[Q];
            List<dynamic> datas = new List<dynamic>();
            for (int i = 0; i < N; i++)
            {
                string[] inputs = Console.ReadLine().Split(' ');
                datas.Add(new { Ext = inputs[0], Mime = inputs[1].Split('/')[0] });
            }
            for (int i = 0; i < Q; i++)
            {
                fNames[i] = Console.ReadLine(); // One file name per line.
            }

            for (int i = 0; i < Q; i++)
            {
                string[] strArray = fNames[i].Split('.');

                var nameQuery = from file in datas
                                where file.Ext == strArray[1]
                                select file.Mime;
                if (Enumerable.Count(nameQuery) == 0)
                    Console.WriteLine("UNKNOWN");
                else
                {
                    foreach (string str in nameQuery)
                    {
                        Console.WriteLine($"{str}/{strArray[1]}");
                        break;
                    }
                }

            }
        }

        


    }


}
