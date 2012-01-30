
define(function () {
    return {
        writeFileSync: function (path, content, encoding) { return ioe.writeFileSync(path, content, encoding); }
    };
});
