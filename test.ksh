#!/bin/ksh
touch test.txt
echo ---------------- >> test.txt
date >> test.txt    

echo "llvm GC=Boem" >> test.txt
mono --llvm -O=all --gc=boehm MatchAnalysis.exe >>test.txt

echo "llvm GC=sgn" >> test.txt
mono --llvm -O=all --gc=sgen MatchAnalysis.exe >>test.txt

echo "GC=Boem" >> test.txt
mono  -O=all --gc=boehm MatchAnalysis.exe >>test.txt

echo "GC=sgn" >> test.txt
mono  -O=all --gc=sgen MatchAnalysis.exe >>test.txt

echo "GC=Boem No Opt" >> test.txt
mono --gc=boehm MatchAnalysisNoOpt.exe >>test.txt

echo "GC=sgn No Opt" >> test.txt
mono  --gc=sgen MatchAnalysisNoOpt.exe >>test.txt

