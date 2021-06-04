function CreateEntity() {

    var subeNo = "";
    var faturaNo = "";
    var faturaTarihi = "";
    var irsaliyeNo = "";
    var irsaliyeTarihi = "";
    var tedarikciAdi = "";
    var urunNo = "";
    var urunAdi = "";
    var urunKategori = "";
    var urunAltKategori = "";
    var urunMiktari = 0;
    var urunMiktarBirimi = "";
    var urunBirimFiyati = 0;
    var urunFiyatKurTipi = "";
    var KDVsizToplam = 0;
    var KDVYuzde = 0;
    var KDVTutar = 0;
    var KDVliToplamTutar = 0;
    var kayitTarihi = "";
    var jsonArr = {};
    var jsonArrTotal = [];
    if (CTRL()) {
        $('#table-info tbody tr:visible').each(function () {
            subeNo = $(this).find(".SubeNo").val();
            faturaNo = $(this).find(".FaturaNo").val();
            faturaTarihi = $(this).find(".FaturaTarihi").val();
            irsaliyeNo = $(this).find(".IrsaliyeNo").val();
            irsaliyeTarihi = $(this).find(".IrsaliyeTarihi").val();
            tedarikciAdi = $(this).find(".TedarikciAdi").val();
            urunNo = $(this).find(".UrunNo").val();
            urunAdi = $(this).find(".UrunAdi :selected").val();
            urunKategori = $(this).find(".UrunKategori").val();
            urunAltKategori = $(this).find(".UrunAltKategori").val();
            urunMiktari = $(this).find(".UrunMiktari").val();
            urunMiktarBirimi = $(this).find(".MiktarBirimi").val();
            urunBirimFiyati = $(this).find(".BirimFiyatı").val();
            urunFiyatKurTipi = $(this).find(".FiyatKurTipi").val();
            KDVsizToplam = $(this).find(".KdvsizToplam").val();
            KDVYuzde = $(this).find(".KdvYuzde").val();
            KDVTutar = $(this).find(".KdvTutar").val();
            KDVliToplamTutar = $(this).find(".KdvliToplamTutar").val();
            kayitTarihi = $(this).find(".KayitTarihi").val();

            jsonArr.SubeNo = subeNo;
            jsonArr.FaturaNo = faturaNo;
            jsonArr.FaturaTarihi = faturaTarihi;
            jsonArr.IrsaliyeNo = irsaliyeNo;
            jsonArr.IrsaliyeTarihi = irsaliyeTarihi;
            jsonArr.TedarikciAdi = tedarikciAdi;
            jsonArr.UrunNo = urunNo;
            jsonArr.UrunAdi = urunAdi;
            jsonArr.UrunKategori = urunKategori;
            jsonArr.UrunAltKategori = urunAltKategori;
            jsonArr.UrunMiktari = urunMiktari;
            jsonArr.MiktarBirimi = urunMiktarBirimi;
            jsonArr.BirimFiyatı = urunBirimFiyati;
            jsonArr.FiyatKurTipi = urunFiyatKurTipi;
            jsonArr.KdvsizToplam = KDVsizToplam;
            jsonArr.KdvYuzde = KDVYuzde;
            jsonArr.KdvTutar = KDVTutar;
            jsonArr.KdvliToplamTutar = KDVliToplamTutar;
            jsonArr.KayitTarihi = kayitTarihi;

            jsonArrTotal.push(jsonArr);
            jsonArr = {};

        });
       
        $.ajax({
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            type: "POST",
            url: "/FaturaGiris/Create",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(jsonArrTotal),
            dataType: "text",

        }).done(function (message) {
            if (message) {
                $('#span-error').text(message); // display the error message in the span tag
            } else {
                window.location.href = '/FaturaGiris/FaturaListesi' // redirect to another page
            }
        }).fail(function (message) {
            window.location.href = '/FaturaGiris/FaturaListesi'
        });
    }

}
function clearRelated() {
    $(".UrunNo").val("");
    $(".MiktarBirimi").val("");
    $(".KdvYuzde").val("");
    $(".UrunAltKategori").val("");
    $(".UrunKategori").val("");
}

function mustEnterTypes() {
    $("#input-faturaSeriNo").keydown(function (e) {
        if (e.shiftKey || e.ctrlKey || e.altKey) {
            e.preventDefault();
        } else {
            var key = e.keyCode;
            if (!((key == 8) || (key == 32) || (key == 46) || (key >= 35 && key <= 40) || (key >= 65 && key <= 90))) {
                e.preventDefault();
            }
        }
    });
    $("#input-faturaNo").keydown(function (e) {
        var key = e.charCode || e.keyCode || 0;
        // allow backspace, tab, delete, enter, arrows, numbers and keypad numbers ONLY
        // home, end, period, and numpad decimal
        return (
            key == 8 ||
            key == 9 ||
            key == 13 ||
            key == 46 ||
            key == 110 ||
            key == 190 ||
            (key >= 35 && key <= 40) ||
            (key >= 48 && key <= 57) ||
            (key >= 96 && key <= 105));
    });
    $("#IrsaliyeNoEnter").keydown(function (e) {
        var key = e.charCode || e.keyCode || 0;
        // allow backspace, tab, delete, enter, arrows, numbers and keypad numbers ONLY
        // home, end, period, and numpad decimal
        return (
            key == 8 ||
            key == 9 ||
            key == 13 ||
            key == 46 ||
            key == 110 ||
            key == 190 ||
            (key >= 35 && key <= 40) ||
            (key >= 48 && key <= 57) ||
            (key >= 96 && key <= 105));
    });
    $(".UrunMiktari").keydown(function (e) {
        var key = e.charCode || e.keyCode || 0;
        // allow backspace, tab, delete, enter, arrows, numbers and keypad numbers ONLY
        // home, end, period, and numpad decimal
        return (
            key == 8 ||
            key == 9 ||
            key == 13 ||
            key == 46 ||
            key == 110 ||
            key == 190 ||
            (key >= 35 && key <= 40) ||
            (key >= 48 && key <= 57) ||
            (key >= 96 && key <= 105));
    });
    $(".BirimFiyatı").keydown(function (e) {
        var key = e.charCode || e.keyCode || 0;
        // allow backspace, tab, delete, enter, arrows, numbers and keypad numbers ONLY
        // home, end, period, and numpad decimal
        return (
            key == 8 ||
            key == 9 ||
            key == 13 ||
            key == 46 ||
            key == 110 ||
            key == 190 ||
            (key >= 35 && key <= 40) ||
            (key >= 48 && key <= 57) ||
            (key >= 96 && key <= 105));
    });
}
window.onload(mustEnterTypes());


function copyToTable() {
    goToUpperCase();
    var subeNo = $('#SubeNoEnter').val();
    var tedAdi = $('#TedarikciAdiEnter').val();
    var faturaNo = $('#input-faturaSeriNo').val() + $('#input-faturaNo').val();
    var faturaTarihi = $('#FaturaTarihiEnter').val().toString();
    faturaTarihi = faturaTarihi.split('-').reverse().join('-');
    var irsaliyeNo = $('#IrsaliyeNoEnter').val();
    var irsaliyeTarihi = $('#IrsaliyeTarihiEnter').val();
    irsaliyeTarihi = irsaliyeTarihi.split('-').reverse().join('-');
    $('.SubeNo').each(function () { $(this).val(subeNo) }
    );
    $('.FaturaNo').each(function () {
        $(this).val(faturaNo)
    });
    $('.FaturaTarihi').each(function () {
        $(this).val(faturaTarihi)
    });
    $('.IrsaliyeNo').each(function () {
        $(this).val(irsaliyeNo)
    });
    $('.IrsaliyeTarihi').each(function () {
        $(this).val(irsaliyeTarihi)
    });
    $('.TedarikciAdi').each(function () {
        $(this).val(tedAdi)
    });

}


function goToUpperCase() {
    $('#input-faturaSeriNo').val($('#input-faturaSeriNo').val().toUpperCase());
}

function calculateTotal() {
    var toplamKDVTutari = 0;
    var toplamKDVHaricTutar = 0;
    var toplamKDVDahilTutar = 0;

    $('#table-info > tbody > tr:visible').each(function () {
        toplamKDVHaricTutar += parseFloat($(this).find(".KdvsizToplam").val());
        toplamKDVTutari += parseFloat($(this).find(".KdvTutar").val());
        toplamKDVDahilTutar += parseFloat($(this).find(".KdvliToplamTutar").val());
    });

    toplamKDVHaricTutar = parseFloat(toplamKDVHaricTutar).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,');
    toplamKDVTutari = parseFloat(toplamKDVTutari).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,');
    toplamKDVDahilTutar = parseFloat(toplamKDVDahilTutar).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,');
    $("#input-KDVHaricTutar").text(toplamKDVHaricTutar + " TL");
    $("#input-toplamKDVTutar").text(toplamKDVTutari + " TL");
    $("#input-KDVDahilTutar").text(toplamKDVDahilTutar + " TL");

}