﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace Whq {
    class Md5 {
        // Hash an input string and return the hash as        // a 32 character hexadecimal string.
     public   static string getMd5Hash(string input) {
            MD5 md5Hasher = MD5.Create(); // Create a new instance of the MD5CryptoServiceProvider object.
                                          // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash( Encoding.Default.GetBytes( input ) );
            // Create a new Stringbuilder to collect the bytes         and create a string.
            StringBuilder sBuilder = new StringBuilder();
            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++) {
                sBuilder.Append( data[i].ToString( "x2" ) );
            }
            return sBuilder.ToString();
        }
        // Verify a hash against a string.
        public static bool verifyMd5Hash(string input, string hash) {
            // Hash the input.
            string hashOfInput = getMd5Hash( input );
            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            if (0 == comparer.Compare( hashOfInput, hash )) {
                return true;
            } else {
                return false;
            }
        }
    }
}
