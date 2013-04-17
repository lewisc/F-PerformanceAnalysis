fsharpcOpts=--optimize+ --tailcalls+ --crossoptimize+ --checked-

test.txt : MatchAnalysis.exe test.ksh
	./test.ksh 



MatchAnalysis.exe : MatchAnalysis.fs
	fsharpc  $(fsharpcOpts) MatchAnalysis.fs
