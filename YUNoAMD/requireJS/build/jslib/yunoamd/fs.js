
define(function () {

    return {
        writeFileSync: function (path, content, encoding) { return YUNoFS.writeFileSync(path, content, encoding); },
        readFileSync: function (path, encoding) { return YUNoFS.readFileSync(path, encoding); },
        mkdirSync: function (path, permissions) { return YUNoFS.mkdirSync(path, permissions); },
        realpathSync: function (path) { return YUNoFS.realpathSync(path); },
        statSync: function (path) { return YUNoFS.statSync(path); },
        readdirSync: function (path) { return eval(YUNoFS.readdirSync(path)); },
        rmdirSync: function (path) { return eval(YUNoFS.rmdirSync(path)); },
        unlinkSync: function (path) { return eval(YUNoFS.unlinkSync(path)); }
    };
});
