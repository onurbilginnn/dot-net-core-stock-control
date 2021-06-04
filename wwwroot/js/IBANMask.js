﻿// jQuery Mask Plugin v0.9.0
// github.com/igorescobar/jQuery-Mask-Plugin
(function (k) {
    var p = function (n, f, h) {
        var g = this, a = k(n), m = { byPassKeys: [8, 9, 37, 38, 39, 40], maskChars: { ":": ":", "-": "-", ".": "\\.", "(": "\\(", ")": "\\)", "/": "/", ",": ",", _: "_", " ": "\\s", "+": "\\+" }, translationNumbers: { 0: "\\d", 1: "\\d", 2: "\\d", 3: "\\d", 4: "\\d", 5: "\\d", 6: "\\d", 7: "\\d", 8: "\\d", 9: "\\d" }, translation: { A: "[a-zA-Z0-9]", S: "[a-zA-Z]" } }; g.init = function () {
        g.settings = {}; h = h || {}; m.translation = k.extend({}, m.translation, m.translationNumbers); g.settings = k.extend(!0, {}, m, h); g.settings.specialChars = k.extend({}, g.settings.maskChars,
            g.settings.translation); a.each(function () { f = d.resolveMask(); f = d.fixRangeMask(f); a.attr("maxlength", f.length); a.attr("autocomplete", "off"); d.destroyEvents(); d.setOnKeyUp(); d.setOnPaste() })
        }; var d = {
            onPasteMethod: function () { setTimeout(function () { a.trigger("keyup") }, 100) }, setOnPaste: function () { d.hasOnSupport() ? a.on("paste", d.onPasteMethod) : a.get(0).addEventListener("paste", d.onPasteMethod, !1) }, setOnKeyUp: function () { a.keyup(d.maskBehaviour).trigger("keyup") }, hasOnSupport: function () { return k.isFunction(k().on) },
            destroyEvents: function () { a.unbind("keyup").unbind("onpaste") }, resolveMask: function () { return "function" == typeof f ? f(d.getVal(), void 0, h) : f }, setVal: function (b) { "input" === a.get(0).tagName.toLowerCase() ? a.val(b) : a.html(b); return a }, getVal: function () { return "input" === a.get(0).tagName.toLowerCase() ? a.val() : a.text() }, specialChar: function (b, c) { return g.settings.specialChars[b.charAt(c)] }, maskChar: function (b, c) { return g.settings.maskChars[b.charAt(c)] }, maskBehaviour: function (b) {
                b = b || window.event; if (-1 < k.inArray(b.keyCode ||
                    b.which, g.settings.byPassKeys)) return !0; var c = d.applyMask(f); c !== d.getVal() && d.setVal(c).trigger("change"); return d.seekCallbacks(b, c)
            }, applyMask: function (b) {
                if ("" !== d.getVal()) {
                    var c = function (b, c) { for (; c < b.length;) { if (void 0 !== b[c]) return !0; c++ } return !1 }, l = function (c) { c = "string" === typeof c ? c : c.join(""); c = c.match(RegExp(d.maskToRegex(b))) || []; c.shift(); return c }, e = d.getVal(); b = d.getMask(e, b); for (var e = h.reverse ? d.removeMaskChars(e) : e, a = l(e); a.join("").length < d.removeMaskChars(e).length;)a = a.join("").split(""),
                        e = d.removeMaskChars(a.join("") + e.substring(a.length + 1)), b = d.getMask(e, b), a = l(e); for (e = 0; e < a.length; e++)if (l = d.specialChar(b, e), d.maskChar(b, e) && c(a, e)) a[e] = b.charAt(e); else if (l) if (void 0 !== a[e]) { if (null === a[e].match(RegExp(l))) break } else if (null === "".match(RegExp(l))) { a = a.slice(0, e); break } return a.join("")
                }
            }, getMask: function (b) {
                if (h.reverse) { b = d.removeMaskChars(b); for (var c = 0, a = 0, e = 0, c = f.length, a = c = 1 <= c ? c : c - 1; e < b.length;) { for (; d.maskChar(f, a - 1);)a--; a--; e++ } a = 1 <= f.length ? a : a - 1; b = f.substring(c, a) } else b =
                    f; return b
            }, maskToRegex: function (b) { for (var c, a = 0, e = ""; a < b.length; a++)(c = d.specialChar(b, a)) && (e += "(" + c + ")?"); return e }, fixRangeMask: function (a) { return a.replace(/([A-Z0-9])\{(\d+)?,([(\d+)])\}/g, function () { var a = arguments, b = [], e = g.settings.translationNumbers[a[1]] ? String.fromCharCode(parseInt("6" + a[1], 16)) : a[1].toLowerCase(); b[0] = a[1]; b[1] = Array(a[2] - 1 + 1).join(a[1]); b[2] = Array(a[3] - a[2] + 1).join(e).toLowerCase(); g.settings.specialChars[e] = d.specialChar(a[1]) + "?"; return b.join("") }) }, removeMaskChars: function (a) {
                k.each(g.settings.maskChars,
                    function (c, d) { a = a.replace(RegExp("(" + g.settings.maskChars[c] + ")?", "g"), "") }); return a
            }, seekCallbacks: function (b, c) { if (h.onKeyPress && void 0 === b.isTrigger && "function" == typeof h.onKeyPress) h.onKeyPress(c, b, a, h); if (h.onComplete && void 0 === b.isTrigger && c.length === f.length && "function" == typeof h.onComplete) h.onComplete(c, b, a, h) }
        }; "boolean" === typeof QUNIT && (g.__p = d); g.remove = function () { d.destroyEvents(); d.setVal(d.removeMaskChars(d.getVal())); a.removeAttr("maxlength") }; g.init()
    }; k.fn.mask = function (n, f) {
        return this.each(function () {
            k(this).data("mask",
                new p(this, n, f))
        })
    }
})(window.jQuery || window.Zepto);