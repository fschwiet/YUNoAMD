:TROLLFACE:

pending question on jurassic: http://jurassic.codeplex.com/discussions/287820#post732435

Next steps:

* need to provide more C# methods to javascript... to implement a .\YUNoAMD\requireJS\build\jslib\YUNoAMD\
* *done* print.js -> done-ish
* optimize.js =>  use node's...  we can optimize in post-processing
* load.js -> done-ish
* file.js ->  use path,fs implementation
* *done* args.js 


Jurassic limitations:

* don't see a way to hook up functions with variable # of arguments
* don't see a way to control the context of execution (faking this wrapping in function(){})
* no support for long (used for filetime)  
  
  
some urls:

* requireeJS google group: http://groups.google.com/group/requirejs
* javascript execution for .NET:  http://jurassic.codeplex.com/ 
* java implementation of AMD: https://github.com/Filirom1/concoct