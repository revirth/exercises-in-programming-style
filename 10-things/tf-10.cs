using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace _10_things
{
    public abstract class TFExercise
    {
        public string info() => this.ToString();
    }

    public class DataStorageManager : TFExercise
    {
        private string _data { get; }
        public DataStorageManager(string path_to_file)
        {
            _data = File.ReadAllText(path_to_file);

            var pattern = new Regex(@"[\W_]+");
            _data = pattern.Replace(_data, " ").ToString().ToLower();
        }

        public string[] words() => _data.Split();

        public virtual string info() => 
            $"{base.info()}: My major data structure is a {_data.GetType()}";
    }

    public class StopWordManager : TFExercise
    {
        private List<string> _stop_words { get; }

        public StopWordManager()
        {
            _stop_words = File.ReadAllText("../stop_words.txt").ToLower().Split(',').ToList();
            _stop_words.AddRange(Enumerable.Range((int)'a', 26).Select(c=>((char)c).ToString()));
        }

        public bool is_stop_word(string word) => _stop_words.Any(w=>w == word);

        public virtual string info() => 
            $"{base.info()}: My major data structure is a {_stop_words.GetType()}";
    }

    public class WordFrequencyManager : TFExercise
    {
        private Dictionary<string, int> _word_freqs { get; }

        public WordFrequencyManager()
        {
            _word_freqs = new Dictionary<string, int>();
        }

        public void increment_count(string word)
        {
            if(_word_freqs.Any(w=>w.Key == word))
                _word_freqs[word] += 1;
            else
                _word_freqs[word] = 1;
        }

        public IEnumerable<KeyValuePair<string, int>> sorted() 
            => _word_freqs.OrderByDescending(w => w.Value);

        public virtual string info() => 
            $"{base.info()}: My major data structure is a {_word_freqs.GetType()}";
    }

    public class WordFrequencyController : TFExercise
    {
        private DataStorageManager _storage_manager;
        private StopWordManager _stopwrd_manager;
        private WordFrequencyManager _word_freq_manager;
        public WordFrequencyController(string path_to_file)
        {
            _storage_manager = new DataStorageManager(path_to_file);
            _stopwrd_manager = new StopWordManager();
            _word_freq_manager = new WordFrequencyManager(); 
        }

        public void run()
        {
            foreach (var w in _storage_manager.words())
                if(!_stopwrd_manager.is_stop_word(w))
                    _word_freq_manager.increment_count(w);

            var word_freqs = _word_freq_manager.sorted();

            foreach (var w in word_freqs.Take(25))
                Console.WriteLine($"{w.Key}  -  {w.Value}");
        }
    }

    class Program
    {        
        static void Main(string[] args)
        {        
            new WordFrequencyController("../pride-and-prejudice.txt").run();
        }
    }
}
