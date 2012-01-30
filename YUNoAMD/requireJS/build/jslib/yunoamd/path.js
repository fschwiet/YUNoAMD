
define(function () {

    return {
        existsSync: function (path) { return YUNoPath.existsSync(path); },
        normalize: function (path) { return YUNoPath.normalize(path); },
        join: function () { return YUNoPath.join.apply(YUNoPath, arguments); }
    };
});
