
define(function () {

    return {
        writeFileSync: function (path, content, encoding) { return YUNoFS.writeFileSync(path, content, encoding); },
        readFileSync: function (path, encoding) { return YUNoFS.readFileSync(path, encoding); },
        mkdirSync: function (path, permissions) { return YUNoFS.mkdirSync(path, permissions); }
    };
});
