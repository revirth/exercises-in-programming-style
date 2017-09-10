#!/usr/bin/env python
import re, string, sys

stops = set(open("../stop_words.txt").read().split(",") + list(string.ascii_lowercase))
words = [x.lower() for x in re.split("[^a-zA-Z]+", open('../pride-and-prejudice.txt').read()) if len(x) > 0 and x.lower() not in stops]
unique_words = list(set(words))
unique_words.sort(key = lambda x: words.count(x))
print ("\n".join(["%s - %s" % (x, words.count(x)) for x in unique_words[:25]]))
