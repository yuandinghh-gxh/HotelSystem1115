using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using alias

namespace ConsoleApplication1 {
    public struct CoOrds {
        public int x, y;

        public CoOrds(int p1, int p2) {
            x = p1;
            y = p2;
        }
    }

    class Program {

        static void Main(string[] args) {

            global::System.Console.WriteLine( "Hello World" );
            //    using Col=System.Collections.Generic;
            //var numbers = new Col::List<int> { 1, 2, 3 };

            // Initialize:   
            CoOrds coords1 = new CoOrds();
            CoOrds coords2 = new CoOrds( 10, 10 );

            // Display results:
            Console.Write( "CoOrds 1: " );   //没初始化显示0
            Console.WriteLine( "x = {0}, y = {1}", coords1.x, coords1.y );

            Console.Write( "CoOrds 2: " );
            Console.WriteLine( "x = {0}, y = {1}", coords2.x, coords2.y );

            // Keep the console window open in debug mode.
            p pp = new p();
            Console.WriteLine( pp.chi( 30 ) );

            List<int> ageList = new List<int> { 10, 25, 20, 9, 30, 40 };
            ageList.Sort();
            foreach (int item in ageList) {
                Console.WriteLine( item );
            }
            // Create a new hash table.
            Hashtable openWith = new Hashtable();
            // Add some elements to the hash table. There are no 
            // duplicate keys, but some of the values are duplicates.
            openWith.Add( "txt", "notepad.exe" );
            openWith.Add( "bmp", "paint.exe" );
            openWith.Add( "dib", "paint.exe" );
            openWith.Add( "rtf", "wordpad.exe" );
            // The Add method throws an exception if the new key is 
            // already in the hash table.
            try {
                openWith.Add( "txt", "winword.exe" );
            } catch {
                Console.WriteLine( "An element with Key = \"txt\" already exists." );
            }

            // The Item property is the default property, so you 
            // can omit its name when accessing elements. 
            Console.WriteLine( "For key = \"rtf\", value = {0}.", openWith["rtf"] );

            // The default Item property can be used to change the value
            // associated with a key.
            openWith["rtf"] = "winword.exe";
            Console.WriteLine( "For key = \"rtf\", value = {0}.", openWith["rtf"] );

            // If a key does not exist, setting the default Item property
            // for that key adds a new key/value pair.
            openWith["doc"] = "winword.exe";

            // ContainsKey can be used to test keys before inserting 
            // them.
            if (!openWith.ContainsKey( "ht" )) {
                openWith.Add( "ht", "hypertrm.exe" );
                Console.WriteLine( "Value added for key = \"ht\": {0}", openWith["ht"] );
            }

            // When you use foreach to enumerate hash table elements,
            // the elements are retrieved as KeyValuePair objects.
            Console.WriteLine();
            foreach (DictionaryEntry de in openWith) {
                Console.WriteLine( "Key = {0}, Value = {1}", de.Key, de.Value );
            }

            // To get the values alone, use the Values property.
            ICollection valueColl = openWith.Values;

            // The elements of the ValueCollection are strongly typed
            // with the type that was specified for hash table values.
            Console.WriteLine();
            foreach (string s in valueColl) {
                Console.WriteLine( "Value = {0}", s );
            }

            // To get the keys alone, use the Keys property.
            ICollection keyColl = openWith.Keys;

            // The elements of the KeyCollection are strongly typed
            // with the type that was specified for hash table keys.
            Console.WriteLine();
            foreach (string s in keyColl) {
                Console.WriteLine( "Key = {0}", s );
            }

            // Use the Remove method to remove a key/value pair.
            Console.WriteLine( "\nRemove(\"doc\")" );
            openWith.Remove( "doc" );

            if (!openWith.ContainsKey( "doc" )) {
                Console.WriteLine( "Key \"doc\" is not found." );
            }


  //waiting--------------------------------------------------------------------
            Console.WriteLine( "Press any key to exit." );
            Console.ReadKey();
        }
    }
    class p {
        int i { get; set; }
        public int chi(int ii) {
            ii = ii * 10;

            return ii;
        }

    }
}
