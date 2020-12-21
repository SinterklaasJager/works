"use strict"

console.log("script linked");

$(() => {
    console.log("Jquerry Activated");

    let events = [];
    let selectedCategorieen = [];

    //Get Data Function to do Ajax call and load page
    GetData();

    $(".doelgroepKnop").on("click", function () {
        $(this).toggleClass("selected");

        CheckAge(GetAge());
    });


    function CheckAge(leeftijd) {
        //gets used categories from datafile 
        let categories = GetCategories(events);

        //make filter buttons, uses categories and the given age
        MakeFilters(categories, leeftijd);
        CheckFilters(leeftijd);
    }

    function DivideByAge(ageGroup) {
        //selecteert categorieën per leeftijdsgroep
        return GetNumbers(filter(events, "category", ageGroup), "genre-v2");

    }

    function GetAge() {
        let leeftijd = [];
        $(".doelgroepKnop.selected").each(function () {
            leeftijd.push($(this).attr("leeftijd"));
        });

        if (leeftijd.length != 1) {
            leeftijd[0] = "all";
        }
        console.log("leeftijd" + leeftijd);
        return leeftijd[0];
    }

    function GetNumbers(arr, selector) {
        //telt hoeveel categorieën
        let tempArr = [];
        arr.forEach(element => {
            tempArr.push(element[selector]);
        });
        return CountBy(tempArr);
    }

    function CountBy(arr) {
        return _.countBy(arr)
    }



    function Capitalize(word) {
        return _.capitalize(word);
    }

    function LowerCase(word) {
        return _.lowerCase(word);
    }

    function GetCategories(events) {
        let categories = [];
        events.forEach(element => {
            categories.push(Capitalize(element["genre-v2"]));
        });

        return RemoveDuplicates(categories);
    }

    function GetData() {
        $.ajax({
                url: "entries.json",
                method: "get",
                type: "json",
            }).done(function (data) {
                events = data.items;
                // console.log(events);
                console.log(events);
                //start of with age all
                CheckAge("all");
                //loads all events


            }).fail(function () {
                alert("error AJAX");
            })
            .always(function () {
                console.log("complete Ajaks");

            });
    }

    function CheckFilters(age) {
        //clear content container
        $(".content-container").empty();
        console.log(selectedCategorieen);

        if (selectedCategorieen.length != 0) {
            if (age === "all") {

                selectedCategorieen.forEach(element => {

                    MakeCards(filter(events, "genre-v2", LowerCase(element)));

                });
            } else {
                let eventsByAge = filter(events, "category", age);

                selectedCategorieen.forEach(element => {
                    MakeCards((filter(eventsByAge, "genre-v2", LowerCase(element))));

                });
            }

        } else {
            //if no filters are selected, make cards from all events
            MakeCards(events);
        }
    }

    function MakeCards(filteredEvents) {
        //Builds the cards

        console.log(filteredEvents);
        filteredEvents.forEach(element => {
            let categorie = Capitalize(element["genre-v2"]);
            let cardContainer = $("<div>").addClass("card-container").addClass(categorie).addClass(element.category).attr({
                "_id": element._id,
            });

            let filters = $("<div>").addClass("card-categorie").append($("<h2>").text(categorie)).append($("<h2>").text(element.category));
            let video = $("<div>").addClass("card-video").append($("<a>").attr("href", element["link-to-video"].url).append($("<img>").attr({
                "src": element.thumbnail.url,
                "width": "150px",
                "height": "200px"
            })));
            let info = $("<div>").addClass("card-info").append($("<h1>").text(element.name)).append($("<p>").text(element.excerpt)).append($("<p>").text("Locatie: " + element["recorded-at"])).append($("<p>").text("Duur: " + element["video-length"]))

            cardContainer.append(filters).append(video).append(info);

            $(".content-container").append(cardContainer);

        });

    }

    function FilterAgeNumber(age, element) {
        //Returns amount of events by age
        let number;
        if (age === "all") {
            number = GetNumbers(events, "genre-v2")[LowerCase(element)];
        } else {
            number = DivideByAge(age)[LowerCase(element)];
        }

        if (number != null) {
            return number;
        } else {
            return 0;
        }

    }


    function MakeFilters(categorieen, age) {
        //Make Filter Buttons
        $(".genre-knoppen").empty();
        categorieen.forEach(element => {
            let number = FilterAgeNumber(age, element);
            let filter = $("<div>").attr({
                "class": "filterButton",
                "categorie": element
            }).append($("<p>").text(`${element} (${number})`));
            if (selectedCategorieen.includes(element)) {
                filter.addClass("selected");
            }
            filter.appendTo(".genre-knoppen");
        });
        //On Filter Button Click
        $(".filterButton").on("click", function () {
            $(".content-container").empty();
            $(this).toggleClass("selected")
            let categorie = $(this).attr("categorie")


            if ($(this).hasClass("selected")) {
                selectedCategorieen.push(categorie);
            } else {

                _.pull(selectedCategorieen, categorie);
            }
            CheckFilters(age);
        });
    }


});

function RemoveDuplicates(array) {
    return _.uniq(array);
}

function filter(arr, param, x) {
    //array to filter, parameter to filter on, value to filter out
    //Returns all objects that contain param: x
    return _.filter(arr, {
        [param]: x
    });
}

function sum(a, b) {
    return a + b;
}

export {
    sum,
    RemoveDuplicates,
    filter
}