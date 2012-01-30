
define(function () {

    return {
        existsSync: function (path) { return YUNoPath.existsSync(path); },
        normalize: function (path) { return YUNoPath.normalize(path); }
    };
});
