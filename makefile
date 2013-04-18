fsharpcOpts=--optimize+ --tailcalls+ --crossoptimize+ --checked- --nologo --standalone --platform:x64
fsharpcNoOpts= --optimize- --tailcalls- --crossoptimize- --checked+ --nologo --standalone --platform:x64
fscc=/mono/lib/mono/4.0/fsc.exe
monorun=mono --gc=sgen

test.txt : MatchAnalysis.exe test.ksh MatchAnalysisNoOpt.exe
	./test.ksh 


MatchAnalysisNoOpt.exe : MatchAnalysis.fs
	$(monorun) $(fscc)  $(fsharpcNoOpts) --out:MatchAnalysisNoOpt.exe  MatchAnalysis.fs

MatchAnalysis.exe : MatchAnalysis.fs
	$(monorun) $(fscc) $(fsharpcOpts) MatchAnalysis.fs
