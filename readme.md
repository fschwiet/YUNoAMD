pending question on jurassic: http://jurassic.codeplex.com/discussions/287820#post732435



implement the next module (file) so the read/write code is actually going somewhere that can be tested

        
need to implement module path

YUNoAMD\requireJS\build\jslib\node\file.js:87:                    filePath = path.join(topDir, fileName);
YUNoAMD\requireJS\build\jslib\node\file.js:88:                    stat = fs.statSync(filePath);
YUNoAMD\requireJS\build\jslib\node\file.js:90:                        if (makeUnixPaths) {
YUNoAMD\requireJS\build\jslib\node\file.js:92:                            if (filePath.indexOf("/") === -1) {
YUNoAMD\requireJS\build\jslib\node\file.js:93:                                filePath = filePath.replace(/\\/g, "/");
YUNoAMD\requireJS\build\jslib\node\file.js:99:                            ok = filePath.match(regExpInclude);
YUNoAMD\requireJS\build\jslib\node\file.js:102:                            ok = !filePath.match(regExpExclude);
YUNoAMD\requireJS\build\jslib\node\file.js:106:                            files.push(filePath);
YUNoAMD\requireJS\build\jslib\node\file.js:109:                        dirFiles = this.getFilteredFileList(filePath, re
gExpFilters, makeUnixPaths);
YUNoAMD\requireJS\build\jslib\node\file.js:149:                if (path.existsSync(destFileName) && fs.statSync(destFil
eName).mtime.getTime() >= fs.statSync(srcFileName).mtime.getTime()) {
YUNoAMD\requireJS\build\jslib\node\file.js:155:            parentDir = path.dirname(destFileName);
YUNoAMD\requireJS\build\jslib\node\file.js:156:            if (!path.existsSync(parentDir)) {
YUNoAMD\requireJS\build\jslib\node\file.js:167:        readFile: function (/*String*/path, /*String?*/encoding) {
YUNoAMD\requireJS\build\jslib\node\file.js:175:            return fs.readFileSync(path, encoding);
YUNoAMD\requireJS\build\jslib\node\file.js:195:            parentDir = path.dirname(fileName);
YUNoAMD\requireJS\build\jslib\node\file.js:196:            if (!path.existsSync(parentDir)) {
YUNoAMD\requireJS\build\jslib\node\file.js:206:            if (path.existsSync(fileName)) {
YUNoAMD\requireJS\build\jslib\node\file.js:211:                        this.deleteFile(path.join(fileName, files[i]));

    
    
    
    
    
    
    
    Next steps:
  need to provide more C# methods to javascript... to implement a .\YUNoAMD\requireJS\build\jslib\YUNoAMD\
    *done* print.js -> done-ish
	optimize.js =>  use node's...  we can optimize in post-processing
	load.js -> done-ish
	file.js ->  aggh
	*done* args.js 


Jurassic limitations:

* don't see a way to hook up functions with variable # of arguments
* don't see a way to control the context of execution (faking this wrapping in function(){})
* no support for long (used for filetime)  
  
  
some urls:
* requireeJS google group: http://groups.google.com/group/requirejs
* javascript execution for .NET:  http://jurassic.codeplex.com/ 
* java implementation of AMD: https://github.com/Filirom1/concoct