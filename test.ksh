#!/bin/ksh
touch test.txt
date >> test.txt    

echo "BARE" >> test.txt
mono -O=all MatchAnalysis.exe >>test.txt

echo "GC=Boem" >> test.txt
mono -O=all --gc=boehm MatchAnalysis.exe >>test.txt

echo "GC=sgn" >> test.txt
mono -O=all --gc=sgen MatchAnalysis.exe >>test.txt

