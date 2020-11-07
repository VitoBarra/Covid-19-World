// In development, always fetch from the network and do not enable offline support.
// This is because caching would make development more difficult (changes would not
// be reflected on the first load after each change).
self.addEventListener('fetch', () => { });



async function findHistoryByCountry(country = "all") {
    let CountryData = [];

    $.ajax({
        type: 'GET',
        url: 'Home/CovidStatistic?Country=' + country,
        success: function (data) {
            CountryData = JSON.parse(data);
        },
        error: function (error) {
            alert("Error: " + error);
        },
        async: false
    })


    lineChart1["data"]["datasets"][0]["data"] = CountryData["ActiveCase"];
    lineChart1.update();
    lineChart1["data"]["datasets"][1]["data"] = CountryData["TotalDeaths"];
    lineChart1.update();
    lineChart1["data"]["datasets"][2]["data"] = CountryData["TotalRecoverd"];
    lineChart1.update();

    lineChart2["data"]["datasets"][0]["data"] = CountryData["DailyCases"];
    lineChart2.update();
    lineChart2["data"]["datasets"][1]["data"] = CountryData["DiferenceDailyCases"];
    lineChart2.update();

    if (document.getElementById("CountryText").innerHTML == "all")
        document.getElementById("CountryText").innerHTML = "World";
    else
        document.getElementById("CountryText").innerHTML = country;


function WriteMap(pairs) {
    jQuery(document).ready(function () {
        jQuery('#vmap').vectorMap({
            map: 'world_en',
            backgroundColor: 'rgb(0,0,0,0)',
            color: '#ffffff',
            hoverOpacity: 0.7,
            selectedColor: '#666666',
            enableZoom: true,
            showTooltip: true,
            scaleColors: ['#C8EEFF', '#910000'],
            values: pairs,
            normalizeFunction: 'polynomial',
            onRegionClick: function (element, code, region) {
                //alert(code)

                if (code == "cn") { findHistoryByCountry("China"); }
                else if (code == "it") { findHistoryByCountry("Italy"); }
                else if (code == "es") { findHistoryByCountry("Spain"); }
                else if (code == "us") { findHistoryByCountry("USA"); }
                else if (code == "de") { findHistoryByCountry("Germany"); }
                else if (code == "ir") { findHistoryByCountry("Iran"); }
                else if (code == "fr") { findHistoryByCountry("France"); }
                else if (code == "kr") { findHistoryByCountry("S-Korea"); }
                else if (code == "ch") { findHistoryByCountry("Switzerland"); }
                else if (code == "gb") { findHistoryByCountry("UK"); }
                else if (code == "nl") { findHistoryByCountry("Netherlands"); }
                else if (code == "at") { findHistoryByCountry("Austria"); }
                else if (code == "be") { findHistoryByCountry("Belgium"); }
                else if (code == "no") { findHistoryByCountry("Norway"); }
                else if (code == "se") { findHistoryByCountry("Sweden"); }
                else if (code == "ca") { findHistoryByCountry("Canada"); }
                else if (code == "dk") { findHistoryByCountry("Denmark"); }
                else if (code == "pt") { findHistoryByCountry("Portugal"); }
                else if (code == "my") { findHistoryByCountry("Malaysia"); }
                else if (code == "br") { findHistoryByCountry("Brazil"); }
                else if (code == "au") { findHistoryByCountry("Australia"); }
                else if (code == "jp") { findHistoryByCountry("Japan"); }
                else if (code == "cz") { findHistoryByCountry("Czechia"); }
                else if (code == "tr") { findHistoryByCountry("Turkey"); }
                else if (code == "il") { findHistoryByCountry("Israel"); }
                else if (code == "ie") { findHistoryByCountry("Ireland"); }
                else if (code == "pk") { findHistoryByCountry("Pakistan"); }
                else if (code == "cl") { findHistoryByCountry("Chile"); }
                else if (code == "pl") { findHistoryByCountry("Poland"); }
                else if (code == "ec") { findHistoryByCountry("Ecuador"); }
                else if (code == "gr") { findHistoryByCountry("Greece"); }
                else if (code == "fi") { findHistoryByCountry("Finland"); }
                else if (code == "qa") { findHistoryByCountry("Qatar"); }
                else if (code == "is") { findHistoryByCountry("Iceland"); }
                else if (code == "id") { findHistoryByCountry("Indonesia"); }
                else if (code == "th") { findHistoryByCountry("Thailand"); }
                else if (code == "sa") { findHistoryByCountry("Saudi-Arabia"); }
                else if (code == "si") { findHistoryByCountry("Slovenia"); }
                else if (code == "ro") { findHistoryByCountry("Romania"); }
                else if (code == "in") { findHistoryByCountry("India"); }
                else if (code == "pe") { findHistoryByCountry("Peru"); }
                else if (code == "ph") { findHistoryByCountry("Philippines"); }
                else if (code == "ru") { findHistoryByCountry("Russia"); }
                else if (code == "ee") { findHistoryByCountry("Estonia"); }
                else if (code == "eg") { findHistoryByCountry("Egypt"); }
                else if (code == "za") { findHistoryByCountry("South-Africa"); }
                else if (code == "lb") { findHistoryByCountry("Lebanon"); }
                else if (code == "iq") { findHistoryByCountry("Iraq"); }
                else if (code == "hr") { findHistoryByCountry("Croatia"); }
                else if (code == "mx") { findHistoryByCountry("Mexico"); }
                else if (code == "pa") { findHistoryByCountry("Panama"); }
                else if (code == "co") { findHistoryByCountry("Colombia"); }
                else if (code == "sk") { findHistoryByCountry("Slovakia"); }
                else if (code == "kw") { findHistoryByCountry("Kuwait"); }
                else if (code == "rs") { findHistoryByCountry("Serbia"); }
                else if (code == "bg") { findHistoryByCountry("Bulgaria"); }
                else if (code == "am") { findHistoryByCountry("Armenia"); }
                else if (code == "ar") { findHistoryByCountry("Argentina"); }
                else if (code == "tw") { findHistoryByCountry("Taiwan"); }
                else if (code == "ae") { findHistoryByCountry("UAE"); }
                else if (code == "dz") { findHistoryByCountry("Algeria"); }
                else if (code == "lv") { findHistoryByCountry("Latvia"); }
                else if (code == "cr") { findHistoryByCountry("Costa-Rica"); }
                else if (code == "do") { findHistoryByCountry("Dominican-Republic"); }
                else if (code == "uy") { findHistoryByCountry("Uruguay"); }
                else if (code == "hu") { findHistoryByCountry("Hungary"); }
                else if (code == "jo") { findHistoryByCountry("Jordan"); }
                else if (code == "lt") { findHistoryByCountry("Lithuania"); }
                else if (code == "ma") { findHistoryByCountry("Morocco"); }
                else if (code == "vn") { findHistoryByCountry("Vietnam"); }
                else if (code == "ba") { findHistoryByCountry("Bosnia-and-Herzegovina"); }
                else if (code == "mk") { findHistoryByCountry("North-Macedonia"); }
                else if (code == "cy") { findHistoryByCountry("Cyprus"); }
                else if (code == "bn") { findHistoryByCountry("Brunei"); }
                else if (code == "md") { findHistoryByCountry("Moldova"); }
                else if (code == "lk") { findHistoryByCountry("Sri-Lanka"); }
                else if (code == "al") { findHistoryByCountry("Albania"); }
                else if (code == "by") { findHistoryByCountry("Belarus"); }
                else if (code == "mt") { findHistoryByCountry("Malta"); }
                else if (code == "ve") { findHistoryByCountry("Venezuela"); }
                else if (code == "bf") { findHistoryByCountry("Burkina-Faso"); }
                else if (code == "tn") { findHistoryByCountry("Tunisia"); }
                else if (code == "sn") { findHistoryByCountry("Senegal"); }
                else if (code == "kz") { findHistoryByCountry("Kazakhstan"); }
                else if (code == "az") { findHistoryByCountry("Azerbaijan"); }
                else if (code == "kh") { findHistoryByCountry("Cambodia"); }
                else if (code == "nz") { findHistoryByCountry("New-Zealand"); }
                else if (code == "om") { findHistoryByCountry("Oman"); }
                else if (code == "ge") { findHistoryByCountry("Georgia"); }
                else if (code == "tt") { findHistoryByCountry("Trinidad-and-Tobago"); }
                else if (code == "ua") { findHistoryByCountry("Ukraine"); }
                else if (code == "uz") { findHistoryByCountry("Uzbekistan"); }
                else if (code == "cm") { findHistoryByCountry("Cameroon"); }
                else if (code == "bd") { findHistoryByCountry("Bangladesh"); }
                else if (code == "af") { findHistoryByCountry("Afghanistan"); }
                else if (code == "hn") { findHistoryByCountry("Honduras"); }
                else if (code == "cd") { findHistoryByCountry("DRC"); }
                else if (code == "ng") { findHistoryByCountry("Nigeria"); }
                else if (code == "cu") { findHistoryByCountry("Cuba"); }
                else if (code == "gh") { findHistoryByCountry("Ghana"); }
                else if (code == "") { findHistoryByCountry("Puerto-Rico"); }
                else if (code == "jm") { findHistoryByCountry("Jamaica"); }
                else if (code == "bo") { findHistoryByCountry("Bolivia"); }
                else if (code == "gy") { findHistoryByCountry("Guyana"); }
                else if (code == "py") { findHistoryByCountry("Paraguay"); }
                else if (code == "gf") { findHistoryByCountry("French-Guiana"); }
                else if (code == "gt") { findHistoryByCountry("Guatemala"); }
                else if (code == "rw") { findHistoryByCountry("Rwanda"); }
                else if (code == "tg") { findHistoryByCountry("Togo"); }
                else if (code == "pf") { findHistoryByCountry("French-Polynesia"); }
                else if (code == "mu") { findHistoryByCountry("Mauritius"); }
                else if (code == "bb") { findHistoryByCountry("Barbados"); }
                else if (code == "mv") { findHistoryByCountry("Maldives"); }
                else if (code == "mn") { findHistoryByCountry("Mongolia"); }
                else if (code == "et") { findHistoryByCountry("Ethiopia"); }
                else if (code == "ke") { findHistoryByCountry("Kenya"); }
                else if (code == "sc") { findHistoryByCountry("Seychelles"); }
                else if (code == "gq") { findHistoryByCountry("Equatorial-Guinea"); }
                else if (code == "tz") { findHistoryByCountry("Tanzania"); }
                else if (code == "ga") { findHistoryByCountry("Gabon"); }
                else if (code == "sr") { findHistoryByCountry("Suriname"); }
                else if (code == "bs") { findHistoryByCountry("Bahamas"); }
                else if (code == "nc") { findHistoryByCountry("New-Caledonia"); }
                else if (code == "cv") { findHistoryByCountry("Cabo-Verde"); }
                else if (code == "cg") { findHistoryByCountry("Congo"); }
                else if (code == "sv") { findHistoryByCountry("El-Salvador"); }
                else if (code == "lr") { findHistoryByCountry("Liberia"); }
                else if (code == "mg") { findHistoryByCountry("Madagascar"); }
                else if (code == "na") { findHistoryByCountry("Namibia"); }
                else if (code == "") { findHistoryByCountry("St-Barth"); }
                else if (code == "zw") { findHistoryByCountry("Zimbabwe"); }
                else if (code == "sd") { findHistoryByCountry("Sudan"); }
                else if (code == "ao") { findHistoryByCountry("Angola"); }
                else if (code == "bj") { findHistoryByCountry("Benin"); }
                else if (code == "") { findHistoryByCountry("Bermuda"); }
                else if (code == "fj") { findHistoryByCountry("Fiji"); }
                else if (code == "gl") { findHistoryByCountry("Greenland"); }
                else if (code == "gn") { findHistoryByCountry("Guinea"); }
                else if (code == "ht") { findHistoryByCountry("Haiti"); }
                else if (code == "mr") { findHistoryByCountry("Mauritania"); }
                else if (code == "ni") { findHistoryByCountry("Nicaragua"); }
                else if (code == "lc") { findHistoryByCountry("Saint-Lucia"); }
                else if (code == "zm") { findHistoryByCountry("Zambia"); }
                else if (code == "np") { findHistoryByCountry("Nepal"); }
                else if (code == "ag") { findHistoryByCountry("Antigua-and-Barbuda"); }
                else if (code == "td") { findHistoryByCountry("Chad"); }
                else if (code == "dj") { findHistoryByCountry("Djibouti"); }
                else if (code == "er") { findHistoryByCountry("Eritrea"); }
                else if (code == "gm") { findHistoryByCountry("Gambia"); }
                else if (code == "ne") { findHistoryByCountry("Niger"); }
                else if (code == "pg") { findHistoryByCountry("Papua-New-Guinea"); }
                else if (code == "so") { findHistoryByCountry("Somalia"); }
                else if (code == "tl") { findHistoryByCountry("Timor-Leste"); }
                else if (code == "ug") { findHistoryByCountry("Uganda"); }
                else if (code == "mz") { findHistoryByCountry("Mozambique"); }
                else if (code == "sy") { findHistoryByCountry("Syria"); }
                else if (code == "gd") { findHistoryByCountry("Grenada"); }
                else if (code == "dm") { findHistoryByCountry("Dominica"); }
                else if (code == "bz") { findHistoryByCountry("Belize"); }
                else if (code == "mm") { findHistoryByCountry("Myanmar"); }
                else if (code == "ly") { findHistoryByCountry("Libya"); }
                else if (code == "ml") { findHistoryByCountry("Mali"); }
                else if (code == "gw") { findHistoryByCountry("Guinea-Bissau"); }
                else if (code == "kn") { findHistoryByCountry("Saint-Kitts-and-Nevis"); }
                else if (code == "bw") { findHistoryByCountry("Botswana"); }
                else if (code == "sl") { findHistoryByCountry("Sierra-Leone"); }
                else if (code == "bi") { findHistoryByCountry("Burundi"); }
                else if (code == "mw") { findHistoryByCountry("Malawi"); }
                else if (code == "fk") { findHistoryByCountry("Falkland-Islands"); }
                else if (code == "st") { findHistoryByCountry("Sao-Tome-and-Principe"); }
                else if (code == "sd") { findHistoryByCountry("South-Sudan"); }
                else if (code == "ye") { findHistoryByCountry("Yemen"); }
            }

        });
    }                  
