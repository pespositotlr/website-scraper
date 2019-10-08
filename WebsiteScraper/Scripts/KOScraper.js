function ViewModel() {
    var self = this;
    self.Search = function (currentViewModel) {

        $.ajax({
            type: "post",
            contentType: "application/json",
            url: "/Home/GetSearchResults/",
            data: ko.toJSON(currentViewModel),
            error: function (xhr, status, error) {
            },
            success: function (response) {
                self.UpdatePage(response);
            }
        });
    };

    self.UpdatePage = function (data) {
        ko.mapping.fromJS(data, {}, self);
    };
}

$(function () {

    var myViewModel = ko.mapping.fromJSON(jsonScraperResultViewModel, {}, new ViewModel());
    ko.applyBindings(myViewModel);
});
