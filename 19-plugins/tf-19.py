#!/usr/bin/env python
import sys, configparser, imp

def load_plugins():
    config = configparser.ConfigParser()
    config.read("config.ini")
    words_plugin = config.get("Plugins", "words")
    frequencies_plugin = config.get("Plugins", "frequencies")
    global tfwords, tffreqs
    tfwords = imp.load_compiled('tfwords', words_plugin)
    tffreqs = imp.load_compiled('tffreqs', frequencies_plugin)

load_plugins()
word_freqs = tffreqs.top25(tfwords.extract_words('../pride-and-prejudice.txt'))

for (w, c) in word_freqs:
    print (w, ' - ', c)

