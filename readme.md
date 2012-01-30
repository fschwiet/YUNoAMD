pending question on jurassic: http://jurassic.codeplex.com/discussions/287820#post732435

statSync.mtime.getTime()
statSync.isFile()
statSync.isDirectory()



implement the next module (file) so the read/write code is actually going somewhere that can be tested


need to implement module fs

    YUNoAMD\requireJS\build\jslib\node\file.js:63:            return fs.statSync(path).isFile();
    YUNoAMD\requireJS\build\jslib\node\file.js:67:            return fs.statSync(path).isDirectory();
    YUNoAMD\requireJS\build\jslib\node\file.js:84:                dirFileArray = fs.readdirSync(topDir);
    YUNoAMD\requireJS\build\jslib\node\file.js:88:                    stat = fs.statSync(filePath);
    YUNoAMD\requireJS\build\jslib\node\file.js:149:                if (pathgexistsSync(destFileName) && fs.statSync(destFil
    eName).mtime.getTime() >= fs.statSync(srcFileName).mtime.getTime()) {
    YUNoAMD\requireJS\build\jslib\node\file.js:160:            fs.writeFileSync(destFileName, fs.readFileSync(srcFileName,
    'binary'), 'binary');
    YUNoAMD\requireJS\build\jslib\node\file.js:175:            return fs.readFileSync(path, encoding);
    YUNoAMD\requireJS\build\jslib\node\file.js:200:            fs.writeFileSync(fileName, fileContents, encoding);
    YUNoAMD\requireJS\build\jslib\node\file.js:207:                stat = fs.statSync(fileName);
    YUNoAMD\requireJS\build\jslib\node\file.js:209:                    files = fs.readdirSync(fileName);
    YUNoAMD\requireJS\build\jslib\node\file.js:213:                    fs.rmdirSync(fileName);
    YUNoAMD\requireJS\build\jslib\node\file.js:215:                    fs.unlinkSync(fileName);
    YUNoAMD\requireJS\build\jslib\node\load.js:12:        var contents = fs.readFileSync(fileName, 'utf8');
    
    
need to implement module path

    YUNoAMD\requireJS\require.js:187:         * a real name that can be mapped to a path.
    YUNoAMD\requireJS\require.js:233:         * name, and path. If parentModuleMap is provided it will
    YUNoAMD\requireJS\require.js:440:         * depName will be fully qualified, no relative . or .. path.
    YUNoAMD\requireJS\require.js:885:            //reference the parentName's path.
    YUNoAMD\requireJS\require.js:1243:             * Converts a module name + .extension into an URL path.
    YUNoAMD\requireJS\require.js:1260:             * Converts a module name to a file path. Supports cases where
    YUNoAMD\requireJS\require.js:1293:                        //A module that needs to be converted to a path.
    YUNoAMD\requireJS\adapt\node.js:65:        if (path.existsSync(url)) {
    YUNoAMD\requireJS\bin\x.js:80:    requireBuildPath = requireBuildPath.replace(/\\/g, '/');
    YUNoAMD\requireJS\bin\x.js:81:    if (requireBuildPath.charAt(requireBuildPath.length - 1) !== "/") {
    YUNoAMD\requireJS\build\jslib\build.js:73:            //a build file path. Otherwise, just all build args.
    YUNoAMD\requireJS\build\jslib\build.js:131:                        if (srcPath.indexOf('/') !== 0 && srcPath.indexOf(':
    ') === -1) {
    YUNoAMD\requireJS\build\jslib\build.js:352:        //then already is an abolute path.
    YUNoAMD\requireJS\build\jslib\build.js:353:        if (path.indexOf('/') !== 0 && path.indexOf(':') === -1) {
    YUNoAMD\requireJS\build\jslib\build.js:355:                   (absFilePath.charAt(absFilePath.length - 1) === '/' ? ''
    : '/') +
    YUNoAMD\requireJS\build\jslib\build.js:382:        if (config.requireBuildPath.charAt(config.requireBuildPath.length -
    1) !== "/") {
    YUNoAMD\requireJS\build\jslib\build.js:475:                        //the appDir, *not* the absFilePath. appDir and dir
    are
    YUNoAMD\requireJS\build\jslib\build.js:482:                        //relative to the absFilePath.
    YUNoAMD\requireJS\build\jslib\build.js:629:                             (config.dir ? module._buildPath.replace(config.
    dir, "") : module._buildPath) +
    YUNoAMD\requireJS\build\jslib\build.js:680:            buildFileContents += path.replace(config.dir, "") + "\n";
    YUNoAMD\requireJS\build\jslib\commonJs.js:33:            commonJsPath = commonJsPath.replace(/\\/g, "/");
    YUNoAMD\requireJS\build\jslib\commonJs.js:34:            savePath = savePath.replace(/\\/g, "/");
    YUNoAMD\requireJS\build\jslib\commonJs.js:35:            if (commonJsPath.charAt(commonJsPath.length - 1) === "/") {
    YUNoAMD\requireJS\build\jslib\commonJs.js:36:                commonJsPath = commonJsPath.substring(0, commonJsPath.leng
    th - 1);
    YUNoAMD\requireJS\build\jslib\commonJs.js:38:            if (savePath.charAt(savePath.length - 1) === "/") {
    YUNoAMD\requireJS\build\jslib\commonJs.js:39:                savePath = savePath.substring(0, savePath.length - 1);
    YUNoAMD\requireJS\build\jslib\optimize.js:72:                //if a relative path, then tack on the filePath.
    YUNoAMD\requireJS\build\jslib\node\file.js:13:        if (!path.existsSync(dir)) {
    YUNoAMD\requireJS\build\jslib\node\file.js:40:            return path.existsSync(fileName);
    YUNoAMD\requireJS\build\jslib\node\file.js:55:            return path.normalize(fs.realpathSync(fileName).replace(/\\/g
    , '/'));
    YUNoAMD\requireJS\build\jslib\node\file.js:59:            return path.normalize(fileName);
    YUNoAMD\requireJS\build\jslib\node\file.js:83:            if (path.existsSync(topDir)) {
    YUNoAMD\requireJS\build\jslib\node\file.js:87:                    filePath = path.join(topDir, fileName);
    YUNoAMD\requireJS\build\jslib\node\file.js:92:                            if (filePath.indexOf("/") === -1) {
    YUNoAMD\requireJS\build\jslib\node\file.js:93:                                filePath = filePath.replace(/\\/g, "/");
    YUNoAMD\requireJS\build\jslib\node\file.js:99:                            ok = filePath.match(regExpInclude);
    YUNoAMD\requireJS\build\jslib\node\file.js:102:                            ok = !filePath.match(regExpExclude);
    YUNoAMD\requireJS\build\jslib\node\file.js:149:                if (path.existsSync(destFileName) && fs.statSync(destFil
    eName).mtime.getTime() >= fs.statSync(srcFileName).mtime.getTime()) {
    YUNoAMD\requireJS\build\jslib\node\file.js:155:            parentDir = path.dirname(destFileName);
    YUNoAMD\requireJS\build\jslib\node\file.js:156:            if (!path.existsSync(parentDir)) {
    YUNoAMD\requireJS\build\jslib\node\file.js:195:            parentDir = path.dirname(fileName);
    YUNoAMD\requireJS\build\jslib\node\file.js:196:            if (!path.existsSync(parentDir)) {
    YUNoAMD\requireJS\build\jslib\node\file.js:206:            if (path.existsSync(fileName)) {
    YUNoAMD\requireJS\build\jslib\node\file.js:211:                        this.deleteFile(path.join(fileName, files[i]));
    YUNoAMD\requireJS\build\jslib\rhino\file.js:78:                            if (filePath.indexOf("/") === -1) {
    YUNoAMD\requireJS\build\jslib\rhino\file.js:79:                                filePath = filePath.replace(/\\/g, "/");
    YUNoAMD\requireJS\build\jslib\rhino\file.js:85:                            ok = filePath.match(regExpInclude);
    YUNoAMD\requireJS\build\jslib\rhino\file.js:88:                            ok = !filePath.match(regExpExclude);
    YUNoAMD\requireJS\dist\dist-site.js:92:                    length = homePath.split("/").length;    
    
    
    
    
    
    
    
    
    
    
    Next steps:
  need to provide more C# methods to javascript... to implement a .\YUNoAMD\requireJS\build\jslib\YUNoAMD\
    *done* print.js -> done-ish
	optimize.js =>  use node's...  we can optimize in post-processing
	load.js -> done-ish
	file.js ->  aggh
	*done* args.js 

Jurassic limitations:
  don't see a way to hook up functions with variable # of arguments
  
  don't see a way to control the context of execution (faking this wrapping in function(){})
  
  
  
some urls:
* requireeJS google group: http://groups.google.com/group/requirejs
* javascript execution for .NET:  http://jurassic.codeplex.com/ 
* java implementation of AMD: https://github.com/Filirom1/concoct