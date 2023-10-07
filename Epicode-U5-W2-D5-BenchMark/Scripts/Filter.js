$(document).ready(function () {

    function getLista(cf, pensione) {
        $("#tbody").empty()
        $.ajax
            ({
                method: 'POST',
                url: "Prenotazione/PrenotazioniFilter",
                data:
                {
                    CF: cf,
                    Pensione: pensione,
                },
                success: function (spedizioni) {
                    $("#nSpedizioni").text(spedizioni.length)
                    $.each(spedizioni, function (i, v) {
                        let tr = "<tr>" +
                            "<td>" + v.NomeCliente + "</td>" +
                            "<td>" + v.FkCamera + "</td>" +
                            "<td>" + v.DataPrenotazione + "</td>" +
                            "<td>" + v.Dal + "</td>" +
                            "<td>" + v.Al + "</td>" +
                            "<td>" + v.Caparra + "€</td>" +
                            "<td>" + v.Tariffa + "€</td>" +
                            "<td>" + v.MezzaPensione + "</td>" +
                            "<td>" + v.PrimaColazione + "</td>" +
                            "<td>" +
                                "<a href = '/Prenotazione/Edit/" + v.PkPrenotazione + "'> Modifica </a> | " +
                                "<a href = '/Prenotazione/Details/" + v.PkPrenotazione + "'> Dettagli </a> | " +
                                "<a href = '/Prenotazione/Delete/" + v.PkPrenotazione + "'> Elimina </a> | " +
                            "</td>" +
                            "<tr>"
                        $("#tbody").append(tr);
                    })
                }
            })
    }

    getLista("", "")

    $("#Cerca").click(function () {
        let citta = $("#CF").val()
        let cliente = $("#Pensione").is(":checked")
        getLista(citta, cliente)
    })

})