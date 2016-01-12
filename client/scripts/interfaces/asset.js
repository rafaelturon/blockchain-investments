'use strict';
var kmpoint;
(function (kmpoint) {
    var Asset = (function () {
        function Asset(name, type, value, isSubscriber) {
            this.name = name;
            this.type = type;
            this.value = value;
            this.isSubscriber = isSubscriber;
        }
        Asset.prototype.register = function () {
            console.log('Ativo cadastrado com sucesso!');
        };
        return Asset;
    })();
    kmpoint.Asset = Asset;
})(kmpoint || (kmpoint = {}));
//# sourceMappingURL=asset.js.map