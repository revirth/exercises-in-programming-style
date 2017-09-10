using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _05_pipeline
{
    class Program
    {
        // The functions
        static string read_file(string path_to_file){
            return File.ReadAllText(path_to_file);
        }

        static string filter_chars_and_normalize(string str_data)
        {
            var pattern = new Regex(@"[\W_]+");
            return pattern.Replace(str_data, " ").ToString().ToLower();
        }

        static string[] scan(string str_data)
        {
            return str_data.Split();
        }

        static IEnumerable<string> remove_stop_words(string[] word_list)
        {
            var stop_words = File.ReadAllText("../stop_words.txt").ToLower().Split(',').ToList();
            // add single-letter words
            stop_words.AddRange(Enumerable.Range((int)'a', 26).Select(c=>((char)c).ToString()));
            return word_list.Where(w => !stop_words.Contains(w));
        }

        static Dictionary<string, int> frequencies(IEnumerable<string> word_list)
        {
            var word_freqs = new Dictionary<string, int>();
            foreach(var w in word_list)
                if(word_freqs.ContainsKey(w))
                    word_freqs[w] += 1;
                else
                    word_freqs[w] = 1;

            return word_freqs;
        }

        static IEnumerable<KeyValuePair<string, int>> sort(Dictionary<string, int> word_freq) 
        {
            return word_freq.OrderByDescending(w => w.Value);
        }

        static void print_all(IEnumerable<KeyValuePair<string, int>> word_freqs)
        {
            if(word_freqs.Count() > 0) {
                System.Console.WriteLine($"{word_freqs.First().Key}  -  {word_freqs.First().Value}");
                print_all(word_freqs.Skip(1));
            }
        }

        static void Main(string[] args)
        {
            print_all(sort(frequencies(remove_stop_words(scan(filter_chars_and_normalize(read_file("../pride-and-prejudice.txt")))))).Take(25));
        }
    }
}
