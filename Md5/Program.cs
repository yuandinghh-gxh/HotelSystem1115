
using System;
using System.Security.Cryptography;
using System.Text;


//struct sb {
//    public int ii;
//    sb(int I) {
//        ii = I;
//    }
//}
class Example {
    //    public static object Test { get; private set; }

    public delegate void a( );
    public event a b;
    // Hash an inp    ut string and return the hash as a 32 character hexadecimal string.
    public static void x( ) {
        Console.WriteLine( "yuandg" );
    }
    static string getMd5Hash(string input) {
        MD5 md5Hasher = MD5.Create(); // Create a new instance of the MD5CryptoServiceProvider object.
        // Convert the input string to a byte array and compute the hash.
        byte[] data = md5Hasher.ComputeHash( Encoding.Default.GetBytes( input ) );
        // Create a new Stringbuilder to collect the bytes         and create a string.
        StringBuilder sBuilder = new StringBuilder();
        // Loop through each byte of the hashed data         // and format each one as a hexadecimal string.
        for (int i = 0; i < data.Length; i++) {
            sBuilder.Append( data[i].ToString( "x2" ) );
        }
        return sBuilder.ToString();
    }

    // Verify a hash against a string.
    static bool verifyMd5Hash(string input, string hash) {
        // Hash the input.
        string hashOfInput = getMd5Hash( input );        // Create a StringComparer an compare the hashes.
        StringComparer comparer = StringComparer.OrdinalIgnoreCase;
        if (0 == comparer.Compare( hashOfInput, hash )) {
            return true;
        } else {
            return false;
        }
    }
    //public void Test( ) {
    //    int x = 0;
    //}

    delegate double MathAction(double num);

    class DelegateTest {
        private static object input;

        // Regular method that matches signature:
        static double Double(double input) {
            return input * 2;
        }

        static void Main( ) {
            string source = "admin";
            string hash = getMd5Hash( source );
            Console.WriteLine( "The MD5 hash of " + source + " is: " + hash + "." );
            Console.WriteLine( "Verifying the hash..." );
            if (verifyMd5Hash( source, hash )) {
                Console.WriteLine( "The hashes are the same." );
            } else {
                Console.WriteLine( "The hashes are not same." );
            }

            //      sb s1 = new sb( 10 );
            // Instantiate delegate with named method:

            MathAction ma = Double;

            //   Invoke delegate ma:
            double multByTwo = ma( 4.5 );
            Console.WriteLine( "multByTwo: {0}", multByTwo );

            // Instantiate delegate with anonymous method:
            double ma2(double input) {
                return input * input;
            }

            double square = ma2( 5 );
            Console.WriteLine( "square: {0}", square );

            // Instantiate delegate with lambda expression
            double ma3(double s) => s * s * s;
            double cube = ma3( 4.375 );
            Console.WriteLine( "cube: {0}", cube );
            Console.ReadLine();
        }
    }
}
  

