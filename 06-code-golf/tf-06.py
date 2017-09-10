#!/usr/bin/env python
import heapq, re, sys

words = re.findall("[a-z]{2,}", open('../pride-and-prejudice.txt').read().lower())
for w in heapq.nlargest(25, set(words) - set(open("../stop_words.txt").read().split(",")), words.count):
    print (w, "-", words.count(w))
