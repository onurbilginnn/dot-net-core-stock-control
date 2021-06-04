
    var urunAdiList = [];
    var priceList = [];

    var say = 0;
            function onLoad() {
                var subeNo = $("#SubeNoEnter").val();
    $('#NihaiUrunAdi').empty();
                $.ajax({
        headers: {
        'Accept': 'application/json',
    'Content-Type': 'application/json'
},
type: "GET",
url: "/BOM/GetUrunList",
contentType: "application/json; charset=utf-8",
dataType: "json",

                }).done(function (data) {
        urunAdiList = JSON.parse(data);
    for (var i = 0; i < urunAdiList.length; i++) {
                        if (urunAdiList[i].SubeNo == subeNo && (urunAdiList[i].UrunSeviyesi == "1 - Bitmiş Ürün" || urunAdiList[i].UrunSeviyesi == "2 - Ara Ürün"))
                            $('#NihaiUrunAdi').append('<option data-subtext="' + urunAdiList[i].UrunSeviyesi + '">' + urunAdiList[i].UrunAdi + '</option>').selectpicker('refresh');
}

});

                $.ajax({
        headers: {
        'Accept': 'application/json',
    'Content-Type': 'application/json'
},
type: "GET",
url: "/BOM/GetPriceList",
contentType: "application/json; charset=utf-8",
dataType: "json",

                }).done(function (data) {
        priceList = JSON.parse(data);


    });

                $('#modal-body-table').DataTable({
        "bLengthChange": false,
    "retrieve": true,
                    "language": {
        "searchPlaceholder": "Malz. Adı",
    "search": "Ara: ",
                        "sInfo": "<b>Toplam: _TOTAL_ adedin _START_ - _END_ aralığı gösterilmektedir.</b>",
    "infoFiltered": "( _MAX_ adet kayıttan bulundu)",
    "infoEmpty": " 0 kayıt bulundu.",
                        "paginate": {
        "previous": "Önceki",
    "next": "Sonraki",
    "last": "Son",
    "first": "İlk",
}
},

"pageLength": 5,
"ordering": false,
});
                $("#NihaiUrunMiktari").keydown(function (e) {
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
window.onload(onLoad());
   




        function FindRelated() {
            $("#span-NihaiUrunAdi").empty();
        var key = $('#NihaiUrunAdi').val();
                for (var i = 0; i < urunAdiList.length; i++) {
                    if (urunAdiList[i].UrunAdi == key) {
            $('#NihaiUrunSeviyesi').val(urunAdiList[i].UrunSeviyesi);
        $('#NihaiUrunNo').val(urunAdiList[i].UrunNo);
        $('#NihaiUrunKategorisi').val(urunAdiList[i].UrunKategori);
        $('#NihaiAltUrunKategorisi').val(urunAdiList[i].UrunAltKategori);

        $('#NihaiUrunMiktarBr').val(urunAdiList[i].MiktarBirimi);
        $('#NihaiUrunMaliyetKur').val("TL");
    }
}

}
            $('#a-receteOlustur').click(function CreateTable() {

                if (CreateTableCTRL() != -1) {
            $("#SubeNoEnter").prop('disabled', true).selectpicker('refresh');

        $("#NihaiUrunAdi").prop('disabled', true).selectpicker('refresh');
        $('#NihaiUrunMiktari').prop('disabled', true);
        $("#table-recete").removeAttr("hidden");
        $("#div-mazlEkle").removeClass('hidden');
        $('#a-receteOlustur').attr("disabled", "disabled");

    }

});

            function CreateTableCTRL() {
                var subeNoEnter = $("#SubeNoEnter").val();
        var nihaiUrunAdi = $("#NihaiUrunAdi").val();
        var nihaiUrunMik = $('#NihaiUrunMiktari').val();

                if (subeNoEnter == "" && nihaiUrunAdi == "" && nihaiUrunMik == "") {
            $("#span-SubeNoEnter").html("Şube No seçiniz...");
        $("#span-NihaiUrunAdi").html("Ürün Adı seçiniz...");
        $("#span-NihaiUrunMiktari").html("Ürün Miktarı giriniz...");
        return -1;
    }
                else if (subeNoEnter == "" && nihaiUrunAdi == "") {
            $("#span-SubeNoEnter").html("Şube No seçiniz...");
        $("#span-NihaiUrunAdi").html("Ürün Adı seçiniz...");
        return -1;
    }
                else if (subeNoEnter == "" && nihaiUrunMik == "") {
            $("#span-SubeNoEnter").html("Şube No seçiniz...");
        $("#span-NihaiUrunMiktari").html("Ürün Miktarı giriniz...");
        return -1;
    }
                else if (nihaiUrunAdi == "" && nihaiUrunMik == "") {
            $("#span-NihaiUrunAdi").html("Ürün Adı seçiniz...");
        $("#span-NihaiUrunMiktari").html("Ürün Miktarı giriniz...");
        return -1;
    }
                else if (subeNoEnter == "") {
            $("#span-SubeNoEnter").html("Şube No seçiniz...");
        return -1;
    }
                else if (nihaiUrunAdi == "") {
            $("#span-NihaiUrunAdi").html("Ürün Adı seçiniz...");
        return -1;
    }

                else if (nihaiUrunMik == "") {
            $("#span-NihaiUrunMiktari").html("Ürün Miktarı giriniz...");
        return -1;
    }
}
            function QuantityFonk() {
            $('#span-NihaiUrunMiktari').empty();
        }

            function showList(x) {
            $('#input-kaydet').removeAttr('disabled');
        if (say == 0) {
                    var sentData = {};
        var ajaxPOST = $('#SubeNoEnter').val();
        sentData.SubeNo = ajaxPOST;
                    $.ajax({
            headers: {
            'Accept': 'application/json',
        'Content-Type': 'application/json'
    },
    type: "POST",
    url: "/BOM/ReceteUrunler",
    contentType: "application/json; charset=utf-8",
    data: JSON.stringify(sentData),
    dataType: "json"
                    }).done(function (data) {
                        var arr = JSON.parse(data);
                        for (var i = 0; i < arr.length; i++) {
                            if (arr[i].UrunNo == $('#NihaiUrunNo').val()) {
            $('#modal-body-table').dataTable().fnAddData([
                arr[i].UrunAdi, arr[i].UrunNo, arr[i].UrunKategori, arr[i].MiktarBirimi, arr[i].UrunSeviyesi,
                "<div class='btn btn-danger' disabled>Eklenemez</div>"
            ]);
        }
                            else {
            $('#modal-body-table').dataTable().fnAddData([
                arr[i].UrunAdi, arr[i].UrunNo, arr[i].UrunKategori, arr[i].MiktarBirimi, arr[i].UrunSeviyesi,
                "<div onclick='addRow(this)' class='btn btn-primary' >Ekle</div>"
            ]);
        }
    }
});
}
say++;
}
var rowNo = 1;

            function addRow(x) {

                var subeNo = $("#SubeNoEnter").val();
        var urunAdi = "";
        var urunNo = "";
        var urunSev = "";
        var mikBr = "";
        var priceArr = [];
        var say = 0;

        var data = $(x).closest('tr').find('td');
        urunAdi = data.eq(0).text();
        urunNo = data.eq(1).text();
        urunSev = data.eq(4).text();
        mikBr = data.eq(3).text();


                for (var i = 0; i < priceList.length; i++) {
                    if (priceList[i].UrunNo == urunNo) {
            priceArr[say] = parseFloat(priceList[i].BirimFiyatı);
        say++;
    }
}
                if (jQuery.isEmptyObject(priceArr)) {
            alert("Bu malzeme için fatura bilgisi yoktur!!!");
        calcType = -1;

    }
                else {
                    var max = Math.max.apply(null, priceArr);
        var min = Math.min.apply(null, priceArr);
        var avg = 0;
                    for (var i = 0; i < priceArr.length; i++) {
            avg += priceArr[i];
        }
        avg /= priceArr.length;
        var calcType = $('input[type=radio][name=radio-cost]:checked').val();
    }
                avg = parseFloat(avg).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,');
                min = parseFloat(min).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,');
                max = parseFloat(max).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,');
                $("#table-recete > tbody").append("<tr>");
                $("#table-recete > tbody").append("<td class='col-xs-2 td-urunAdi' id='td-" + rowNo + "urunAdi'>" + urunAdi + "</td>");
                $("#table-recete > tbody").append("<td class='col-xs-2 td-urunNo' id='td-" + rowNo + "UrunNo'>" + urunNo + "</td>");
                $("#table-recete > tbody").append("<td class='col-xs-2 td-urunSev' id='td-" + rowNo + "urunSev'>" + urunSev + "</td>");
                $("#table-recete > tbody").append("<td class='col-xs-1'> <input class='form-control' onblur='CalcCost()' id='input-" + rowNo + "qty' />"
            );
                $("#input-" + rowNo + "qty").keydown(function (e) {
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
                $("#table-recete > tbody").append("<td class='col-xs-2 td-mikBr' id='td-" + rowNo + "mikBr'>" + mikBr + "</td>");

                switch (calcType) {
                    case "Avg":
                        $("#table-recete > tbody").append("<td class='col-xs-2 td-price' id='td-" + rowNo + "price'>" + avg + "</td>");

                break;
            case "Min":
                        $("#table-recete > tbody").append("<td class='col-xs-2 td-price' id='td-" + rowNo + "price'>" + min + "</td>");

                break;
            case "Max":
                        $("#table-recete > tbody").append("<td class='col-xs-2 td-price' id='td-" + rowNo + "price'>" + max + "</td>");

                break;
            default:
                        $("#table-recete > tbody").append("<td class='col-xs-2 td-price' id='td-" + rowNo + "price'>0</td>");

                break;
        }

                $("#table-recete > tbody").append("<td class='col-xs-1 td-cost' id='td-" + rowNo + "cost'></td>");
                rowNo++;
                $("#table-recete > tbody").append("</tr>");
            $("#modal-closeBtn").trigger("click");
            $(x).text("Eklendi");
            $(x).removeClass("btn btn-primary");
            $(x).addClass("btn btn-danger");
            $(x).attr("disabled", true);
            $(x).removeAttr('onclick');
        }



            $('input[type=radio][name=radio-cost]').change(function () {
                $('#th-maliyet').text("Br.Maliyet (" + this.value + ")");
            var rowNo = 1;
            var costSum = 0;
                $('.td-urunNo').each(function () {
                    var urunNo = $(this).text();
            var fiyatList = [];
            var say = 0;
            var avg = 0;
            var max = 0;
            var min = 0;
            var sum = 0;
                    for (var i = 0; i < priceList.length; i++) {
                        if (priceList[i].UrunNo == urunNo) {
                fiyatList[say] = parseFloat(priceList[i].BirimFiyatı);
            say++;
        }
    }
                    for (var i = 0; i < fiyatList.length; i++) {
                avg += fiyatList[i];
            }
                    if (avg != 0) {
                avg /= fiyatList.length;
            max = Math.max.apply(null, fiyatList);
            min = Math.min.apply(null, fiyatList);
        }
        fiyatList.fill(0);
        var qty = $("#input-" + rowNo + "qty").val();

        var calcType = $('input[type=radio][name=radio-cost]:checked').val();

                    avg = parseFloat(avg).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,');
                    min = parseFloat(min).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,');
                    max = parseFloat(max).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,');

                    switch (calcType) {
                        case "Avg":
                $("#td-" + rowNo + "price").text(avg);
                costSum += qty * avg;
                sum = qty * avg;
                break;
            case "Min":
                $("#td-" + rowNo + "price").text(min);
                costSum += qty * min;
                sum = qty * min;
                break;
            case "Max":
                $("#td-" + rowNo + "price").text(max);
                costSum += qty * max;
                sum = qty * max;
                break;

        };

        $("#td-" + rowNo + "cost").text(sum);
        $('#NihaiUrunMaliyeti').val(costSum);

        rowNo++;
    });
});

            function CalcCost() {

                $('input[type=radio][name=radio-cost]').trigger("change");

            }

     
            function CreateEntity() {
                var nihaiUrunSev = "";
            var subeNo = "";//ok
            var urunSeviye = "";//ok
            var urunNo = "";//ok
            var urunAdi = "";//ok
            var urunMiktari = 0;//ok
            var urunMiktarBirimi = "";//ok
            var urunFiyatKurTipi = "";//ok
            var urunBaglAraUrunNo = "";
            var urunBaglAraUrunAdi = "";
            var urunBaglBitUrunNo = "";
            var urunBaglBitUrunAdi = "";
            var urunBaglRefMik = 0;//ok
            var kayitTarihi = "";//ok
                var jsonArr = {};
            var jsonArrTotal = [];
            var rowNo = 1;
                if (CTRL()) {
                $("#table-recete > tbody").find('input').each(function () {

                    nihaiUrunSev = $('#NihaiUrunSeviyesi').val();
                    subeNo = $('#SubeNoEnter').val();
                    kayitTarihi = $('#input-kayitTarihi').val();

                    urunBaglRefMik = $('#NihaiUrunMiktari').val();
                    if (nihaiUrunSev == "2 - Ara Ürün") {
                        urunBaglAraUrunNo = $('#NihaiUrunNo').val();
                        urunBaglAraUrunAdi = $('#NihaiUrunAdi').val();
                    }
                    else {
                        urunBaglBitUrunNo = $('#NihaiUrunNo').val();
                        urunBaglBitUrunAdi = $('#NihaiUrunAdi').val();

                    }
                    urunMiktari = $(this).val();

                    urunFiyatKurTipi = $('#NihaiUrunMaliyetKur').val();

                    urunAdi = $('#td-' + rowNo + 'urunAdi').text();

                    urunNo = $('#td-' + rowNo + 'urunNo').text();
                    urunSeviye = $('#td-' + rowNo + 'urunSev').text();
                    urunMiktarBirimi = $('#td-' + rowNo + 'mikBr').text();

                    jsonArr.SubeNo = subeNo;
                    jsonArr.UrunNo = urunNo;
                    jsonArr.UrunAdi = urunAdi;
                    jsonArr.UrunMiktari = urunMiktari;
                    jsonArr.UrunSeviyesi = urunSeviye;
                    jsonArr.MiktarBirimi = urunMiktarBirimi;
                    jsonArr.FiyatKurTipi = urunFiyatKurTipi;
                    jsonArr.KayitTarihi = kayitTarihi;
                    jsonArr.BaglAraUrunNo = urunBaglAraUrunNo;
                    jsonArr.BaglAraUrunAdi = urunBaglAraUrunAdi;

                    jsonArr.BaglBitUrunNo = urunBaglBitUrunNo;
                    jsonArr.BaglBitUrunAdi = urunBaglBitUrunAdi;
                    jsonArr.BaglRefMik = urunBaglRefMik;

                    jsonArrTotal.push(jsonArr);

                    jsonArr = {};

                    rowNo++;

                });
            $.ajax({
                headers: {
                'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        type: "POST",
        url: "/BOM/BOMEkle",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(jsonArrTotal),
        dataType: "text",

                    }).done(function (message) {
                        if (message) {
                alert(message); // display the error message in the span tag
            } else {
                window.location.href = '/BOM/BOMListesi' // redirect to another page
            }
            }).fail(function (message) {
                window.location.href = '/BOM/BOMListesi'
            });
        }

    }

            function CTRL() {
                var say = 1;
            var ctrlNo = 0;
                $("#table-recete > tbody").find('input').each(function () {
                    if ($(this).val() == "") {
                alert(say + ".sıra için miktar bilgisi giriniz.");
            ctrlNo = -1;

        }
        else
            ctrlNo = 1;
        say++;
    })

    if (ctrlNo == 1)
        return true;
    else if (ctrlNo == -1)
        return false;

}
