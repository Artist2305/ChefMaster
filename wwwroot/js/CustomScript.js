var maxIngridients = 20;
var currentAmountOfIngridients = document.querySelectorAll('#ingridientForm').length;
var maxTags = 10;
var currentAmountOfTags = document.querySelectorAll('#tagsForm').length;

var $window = $(window);
var navbar = document.getElementById("nav-bar");
var backButton = document.getElementById("go-back-button");
var sticky = navbar.offsetTop;

var mobMenu = document.getElementById("mob-menu");

function confirmDelete(uniqueId, isDeleteClicked) {
    var deleteSpan = 'deleteSpan_' + uniqueId;
    var confirmDeleteSpan = 'confirmDeleteSpan_' + uniqueId;

    if (isDeleteClicked) {
        $('#' + deleteSpan).hide();
        $('#' + confirmDeleteSpan).show();
    } else {
        $('#' + deleteSpan).show();
        $('#' + confirmDeleteSpan).hide();
    }

}

function addIngridient() {
    if (currentAmountOfIngridients < maxIngridients) {
        currentAmountOfIngridients++;
        let divToAdd = document.querySelector('#ingridientForm').cloneNode(true);
        document.querySelector('#allIngridients').appendChild(divToAdd);
    }
    else {
        document.getElementById('ingridientsInfo').innerHTML = "Can't add any more ingridients!";
    }
}
function removeIngridient() {
    if (currentAmountOfIngridients > 1) {
        currentAmountOfIngridients--;
        document.querySelectorAll('#ingridientForm').item(currentAmountOfIngridients).remove();
        document.getElementById('ingridientsInfo').innerHTML = "";
    }
}

function addTag() {
    if (currentAmountOfTags < maxTags) {
        currentAmountOfTags++;
        let divToAdd = document.querySelector('#tagsForm').cloneNode(true);
        document.querySelector('#allTags').appendChild(divToAdd);
    }
    else {
        document.getElementById('tagsInfo').innerHTML = "Can't add any more tags!";
    }
}
function removeTag() {
    if (currentAmountOfTags > 1) {
        currentAmountOfTags--;
        document.querySelectorAll('#tagsForm').item(currentAmountOfTags).remove();
        document.getElementById('tagsInfo').innerHTML = "";
    }
}

$(function () {
    $(".RateIt").click(function () {

        var rate = new Object();
        rate.UserId = $(".Model_LoggedUserId").val().toString();
        rate.RecipeId = parseInt($(".Model_RecipeId").val());
        rate.RateValue = parseInt($(this).data('value'));

        var t = $(this);
        var t_parent = t.parent();
        var t_parpar = t.parent().parent();
        var amountOfParents = $(".AmountOfRates", t_parpar);
        var overalRating = t_parpar.find(".OveralRating");
        var stars = $(".RateIt", t_parent);

        $.ajax({
            type: "POST",
            url: "/Home/RateIt",
            data: JSON.stringify(rate),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {

                overalRating.html("Rate the recipe to advise others! Average: " + response.tempOveralRating + " (" + response.tempAmountOfRates + " votes)");

                for (var i = 0; i < response.tempOveralRating; i++) {
                    stars.eq(i).addClass("star-full");
                    stars.eq(i).removeClass("star-empty");
                    console.log(i);
                }

            },
            failure: function (response) {
                console.log("failure");
            },
            error: function (response) {
                console.log("error");
            }
        });
    });
});

$(function () {
    $(".FollowHim").click(function () {

        var t = $(this);
        var t_parent = t.parent();

        var observe = new Object();   
        observe.UserObserverdId = $(".Model_RecipeAuthorId").val().toString();
        observe.UserId = $(".Model_LoggedUserId").val().toString();

        $.ajax({
            type: "POST",
            url: "/Home/FollowHim",
            data: JSON.stringify(observe),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                console.log(response.userId);
                console.log(response.userObserverdId);

                t_parent.html('<div class="center-all FollowHim"><img src ="/images/layout/observe_green.png" class= "details-option-button" /></div ><div class="centerEntertainment text-center"><span class="third-normal-text-style main-palet-color">YOU FOLLOW HIM</span></div>');

            },
            failure: function (response) {
                console.log(response.responseText);
            },
            error: function (response) {
                console.log(response.responseText);
            }
        });
    });
});

$(function () {
    $(".AddToFavourite").click(function () {

        var t = $(this);
        var t_parent = t.parent();

        var favourite = new Object();
        favourite.RecipeObserverdId = parseInt($(".Model_RecipeId").val());
        favourite.UserId = $(".Model_LoggedUserId").val().toString();

        $.ajax({
            type: "POST",
            url: "/Home/AddToFavourite",
            data: JSON.stringify(favourite),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
 
                t_parent.html('<div class="center-all AddToFavourite"><img src = "/images/layout/favourite_green.png" class="details-option-button" /></div><div class="centerEntertainment text-center"><span class="third-normal-text-style main-palet-color">ALREADY GOT IT</span></div>');
                console.log(response.RecipeObserverdId);
            },
            failure: function (response) {
                console.log(response.responseText);
            },
            error: function (response) {
                console.log(response.responseText);
            }
        });
    });
});

$(function () {
    $(".AddComment").click(function () {

        var hiddenCommentForm = $(".NewComment");
        hiddenCommentForm.removeClass("hidden-element");

    });
});
$(function () {
    $(".ReportIt").click(function () {

        var hiddenCommentForm = $(".NewReport");
        hiddenCommentForm.removeClass("hidden-element");

    });
});

$(function () {
    $('[data-toggle="popover"]').popover()
})

$(function () {
    $('[data-toggle="tooltip"]').tooltip()
})
$(document).ready(function () {
    $("a").tooltip();
});

jQuery(function ($) {
    
    $.scrollTo(0);
    
    sticky = navbar.offsetTop;

    $('#social-media').click(function () { $.scrollTo($('#social-media-section').offset().top - 200, 500);});
    $('#about-us').click(function () { $.scrollTo($('#about-us-section').offset().top - 200, 500); });

    $('#social-media-mob').click(function () {
        $.scrollTo($('#social-media-section').offset().top - 100, 500);
 
        mobMenu.style.display = "none";
        mobMenu.classList.add("mob-menu-disabled");
        mobMenu.classList.remove("mob-menu-active");
    });
    $('#about-us-mob').click(function () {
        $.scrollTo($('#about-us-section').offset().top - 100, 500);
        mobMenu.style.display = "none";
        mobMenu.classList.add("mob-menu-disabled");
        mobMenu.classList.remove("mob-menu-active");
    });

    $('#go-up').click(function () { $.scrollTo($('body'), 1000); });
}
);
function myFunction() {
    if (window.pageYOffset - 1 >= sticky) {
        navbar.classList.add("sticky")
        backButton.classList.add("on-button")
    } else {
        navbar.classList.remove("sticky");
        backButton.classList.remove("on-button");
    }
}
/*
$window.on('scroll', function () {
    console.log(window.pageYOffset);
});
*/
window.onscroll = function () {
    myFunction()
};
/*
document.getElementsByTagName("BODY")[0].onresize = function () {
    checkOffSet()
};
*/


function checkOffSet() {
    sticky = navbar.offsetTop;
    console.log(sticky);
    console.log(window.pageYOffset);
}

$(function () {
    $("#mob-burger-button").click(function () {
        console.log("rozwijam menu"); 
        if (mobMenu.style.display === "none") {
            mobMenu.style.display = "block";
            mobMenu.classList.add("mob-menu-active");
            mobMenu.classList.remove("mob-menu-disabled");

        } else {
            mobMenu.style.display = "none";
            mobMenu.classList.add("mob-menu-disabled");
            mobMenu.classList.remove("mob-menu-active");
        }

    });
});

$(function () {
    $("#return-button").click(function () {
        console.log("rozwijam menu");
        if (mobMenu.style.display === "none") {
            mobMenu.style.display = "block";
            mobMenu.classList.add("mob-menu-active");
            mobMenu.classList.remove("mob-menu-disabled");

        } else {
            mobMenu.style.display = "none";
            mobMenu.classList.add("mob-menu-disabled");
            mobMenu.classList.remove("mob-menu-active");
        }

    });
});


/*
function setStartViewPosition() {
    document.scrollTop(100);
};
*/




