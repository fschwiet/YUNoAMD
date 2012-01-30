pending question on jurassic: http://jurassic.codeplex.com/discussions/287820#post732435



implement the next module (file) so the read/write code is actually going somewhere that can be tested

        
need to implement module path

YUNoAMD\requireJS\build\jslib\node\file.js:10:define(['fs', 'path'], function (fs, path) {
YUNoAMD\requireJS\build\jslib\node\file.js:13:        if (!path.existsSync(dir)) {
YUNoAMD\requireJS\build\jslib\node\file.js:23:            //First part may be empty string if path starts with a slash.
YUNoAMD\requireJS\build\jslib\node\file.js:40:            return path.existsSync(fileName);
YUNoAMD\requireJS\build\jslib\node\file.js:50:         * Gets the absolute file path as a string, normalized
YUNoAMD\requireJS\build\jslib\node\file.js:51:         * to using front slashes for path separators.
YUNoAMD\requireJS\build\jslib\node\file.js:54:        absPath: function (fileName) {
YUNoAMD\requireJS\build\jslib\node\file.js:55:            return path.normalize(fs.realpathSync(fileName).replace(/\\/g
, '/'));
YUNoAMD\requireJS\build\jslib\node\file.js:59:            return path.normalize(fileName);
YUNoAMD\requireJS\build\jslib\node\file.js:62:        isFile: function (path) {
YUNoAMD\requireJS\build\jslib\node\file.js:63:            return fs.statSync(path).isFile();
YUNoAMD\requireJS\build\jslib\node\file.js:66:        isDirectory: function (path) {
YUNoAMD\requireJS\build\jslib\node\file.js:67:            return fs.statSync(path).isDirectory();
YUNoAMD\requireJS\build\jslib\node\file.js:70:        getFilteredFileList: function (/*String*/startDir, /*RegExp*/regE
xpFilters, /*boolean?*/makeUnixPaths) {
YUNoAMD\requireJS\build\jslib\node\file.js:76:                i, stat, filePath, ok, dirFiles, fileName;
YUNoAMD\requireJS\build\jslib\node\file.js:83:            if (path.existsSync(topDir)) {
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