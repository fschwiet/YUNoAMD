
define(function () {

    return {
        writeFileSync: function (path, content, encoding) { return YUNoFS.writeFileSync(path, content, encoding); }
    };
});
