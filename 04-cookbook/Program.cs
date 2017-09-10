using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace _04_cookbook
{
    class Program
    {
        // The shared mutable data
        static char[] data = new char[]{};
        static List<string> words = new List<string>();
        static Dictionary<string, int> word_freqs = new Dictionary<string, int>();
        
        // The procedures
        static void read_file(string path_to_file)
        {
            var txt = File.ReadAllText(path_to_file);
            data = txt.ToArray();
        }

        static void filter_chars_and_normalize()
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (!Char.IsLetterOrDigit(data[i]))
                    data[i] = ' ';
                else
                    data[i] = char.ToLower(data[i]);
            }
        }

        static void scan()
        {
            var data_str = string.Join("", data);
            words = data_str.Split().Where(s=>!string.IsNullOrWhiteSpace(s)).ToList();
        }

        static void remove_stop_words()
        {
            var stop_words = File.ReadAllText("../stop_words.txt").ToLower().Split(',').ToList();
            // add single-letter words
            stop_words.AddRange(Enumerable.Range((int)'a', 26).Select(c=>((char)c).ToString()));
            var indexes = new List<int>();
            
            for (int i = 0; i < words.Count; i++)
                if (stop_words.Contains(words[i]))
                    indexes.Add(i);

            foreach (var i in indexes.OrderByDescending(idx=>idx))
                words.RemoveAt(i);
        }

        static void frequencies()
        {
            var keys = word_freqs.Keys;

            foreach (var w in words)
                if (keys.Contains(w))
                    word_freqs[w] += 1;
                else
                    word_freqs.Add(w, 1);
        }

        static void sort()
        {
            word_freqs = word_freqs.OrderByDescending(w => w.Value)
                                   .ToDictionary(w => w.Key, w => w.Value);
        }

        // The main function
        static void Main(string[] args)
        {
            read_file("../pride-and-prejudice.txt");
            filter_chars_and_normalize();
            scan();
            remove_stop_words();
            frequencies();
            sort();
            
            int i = 1;
            foreach(var tf in word_freqs.Take(25))
                Console.WriteLine($"{i++:D2} {tf.Key} - {tf.Value}");
        }

    }
}
